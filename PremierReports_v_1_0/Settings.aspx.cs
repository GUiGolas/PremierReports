using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Security;
using System.IO;
using System.Xml;
using System.Threading;





namespace PremierReports_v_1_0
{
    public partial class Settings : System.Web.UI.Page
    {
        /// get ConnectionString
        /// 
       // String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;
        private Int16 langNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
                Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
                base.InitializeCulture();

            }
            if (CultureInfo.CurrentCulture.DisplayName.ToString() != "Português (Brasil)")
            {
                langNumber = 1;
                applyTranslation(1);
            }
            else
            {
                langNumber = 0;
                applyTranslation(0);
            }

            if (!IsPostBack)
            {
                try
                {
                    // fill the dbSettings 

                    ClassDBhandler handler = new ClassDBhandler("");
                    string filePath = handler._configFilePath;
                    // check if the file exists
                    if (File.Exists(filePath))
                    {
                        //Open the file

                        XmlDocument doc = new XmlDocument();
                        doc.Load(filePath); // loading file
                        XmlNodeList xnList = doc.GetElementsByTagName("userdb");
                        if (xnList.Count > 0)
                        {
                            txtDbAddress.Text = xnList[0]["address"].InnerText;
                            txtDbName.Text = xnList[0]["name"].InnerText;
                            txtDbInstance.Text = xnList[0]["server"].InnerText;
                            txtDbPort.Text = xnList[0]["port"].InnerText;
                            txtPortSeparator.Text = xnList[0]["portseparator"].InnerText;

                        }
                        else
                        {
                            throw new Exception("Error to get the xml nodes");
                        }
                    }
                    else
                    {
                        throw new Exception("Configuration file not found.");
                    }
                }
                catch (Exception ex)
                {
                    lblErrorDb.Text = ex.Message.ToString();
                    lblErrorDb.Visible = true;
                    //throw;
                }
            }

        }

        private void applyTranslation(short p)
        {
            try
            {

                lblConfirmPassword.Text = LanguageConstants.getMessage("ConfirmPassword", p) + ":";
                lblDbAddress.Text = LanguageConstants.getMessage("DbAddress", p) + ":";
                lblDbInstance.Text = LanguageConstants.getMessage("DbInstance", p) + ":";
                lblDbName.Text = LanguageConstants.getMessage("DbName", p) + ":";
                lbldbPassword.Text = LanguageConstants.getMessage("Password", p) + ":";
                lbldbUserName.Text = LanguageConstants.getMessage("UserName", p) + ":";
                lblNewPassword.Text = LanguageConstants.getMessage("NewPassword", p) + ":";
                lblPort.Text = LanguageConstants.getMessage("DbPort", p) +":";
                lblPortSeparator.Text = LanguageConstants.getMessage("DbPortSeparator", p) + ":";
                lblUserName.Text = LanguageConstants.getMessage("UserName", p) + ":" ;
                lblPassword.Text = LanguageConstants.getMessage("Password", p) + ":";
                lblChangePassword.Text = LanguageConstants.getMessage("ChangePassword", p) + ":";
                lblDBConfig.Text = LanguageConstants.getMessage("DbConfig", p) + ":";

                lnkButtonSettings.Text = LanguageConstants.getMessage("Settings", p) + ":";

                btnFactoryReset.Text = LanguageConstants.getMessage("Factory", p);
                btnSaveDbSettings.Text = LanguageConstants.getMessage("Save", p);
                btnSavePassword.Text = LanguageConstants.getMessage("Save", p);


            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        protected void Logoff_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        private void LogOut()
        {


            // clear the data from session
            if (Session["user"] != null)
            {
                if (Session["user"].ToString().Length > 0)
                {
                    Session["user"] = string.Empty;
                    Session["password"] = string.Empty;
                    Session["logged"] = false;
                }
            }    
           

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            // Check all fields
            try
            {
                //check if the new password and confirmation password is the same
                if (txtConfirmPassword.Text != txtNewPassword.Text)
                {
                    throw new Exception(ErrorConstants.getMessage("passwordDontMatch", langNumber));
                }
                //check if the lenght of new password is greather than 3
                if (txtConfirmPassword.Text.Length < 4)
                {
                    throw new Exception(ErrorConstants.getMessage("passwordlength", langNumber));
                }
                // check if the lenght of new user name is greather than 2
                if (txtNewUserName.Text.Length < 3)
                {
                    throw new Exception(ErrorConstants.getMessage("userNameMinLength", langNumber));
                }
                //check if the UserName and Password exist and if is correct
                // Verify if the login is valid
                ClassLoginDbHandler loginHandler = new ClassLoginDbHandler();
                //if (!loginHandler.CheckUser(txtUserName.Text.Trim(), txtPassword.Text.Trim())) throw new Exception(ErrorConstants.getMessage("loginFailed", langNumber));
               
                // changing the password
                md5 encript = new md5(txtConfirmPassword.Text);
                encript.Encript();
                string newPass = encript.hash;

                loginHandler.ChangePassword(txtUserName.Text, txtNewUserName.Text, txtPassword.Text, newPass);


                //save logged user in application Session
                Session["user"] = txtNewUserName.Text;
                Session["password"] = newPass;
                Session["logged"] = false;

                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(loginHandler.userName, false);

                if (langNumber == 0)
                {
                    Response.Write("<script language=javascript>alert('Senha alterada com sucesso!');</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Password saved!');</script>");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                lblError.Visible = true;
                //throw;
            }
        }

        //protected void chkConnectionString_Check(object sender, EventArgs e)
        //{
        //    if (chkConnectionString.Checked)
        //    {
        //        txtConnectionString.Enabled = true;
        //        txtConnectionString.Visible = true;
        //    }
        //    else
        //    {
        //        txtConnectionString.Enabled = false;
        //        txtConnectionString.Visible = false;
        //    }
        //}

        protected void btnFactory_Click(object sender, EventArgs e)
        {
            txtDbAddress.Text = "LOCALHOST";
            txtDbInstance.Text = "SQLEXPRESS";
            txtDbName.Text = "DvmsData";
            txtPortSeparator.Text = ",";

            //save settings
        }

        protected void btnSaveDbSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDbAddress.Text.Length < 1 || txtDbInstance.Text.Length < 1 || txtDbName.Text.Length < 1)
                {
                    throw new Exception(ErrorConstants.getMessage("missingFields", langNumber));
                }

                  
                proccessXML(txtDbName.Text.Trim(),
                    txtDbAddress.Text.Trim(),
                    txtDbInstance.Text.Trim(),
                    txtDbPort.Text.Trim(),
                    txtPortSeparator.Text.Trim(),
                    txtdbUserName.Text.Trim(),
                    txtdbPassword.Text.Trim(),
                    txtConString.Text.Trim());

                if(langNumber == 0)
                {
                    Response.Write("<script language=javascript>alert('Configurações salvas com sucesso!');</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Configurations saved!');</script>");
                }
                
            }


            catch (Exception ex)
            {
                lblErrorDb.Text = ex.Message.ToString();
                lblErrorDb.Visible = true;
                //throw;
            }
        }

        private void proccessXML(string _dbName, string _dbAdrress, string _server, string _port, string _portseparator, string _username, string _password, string _connecrtionstring)
        {

            try
            {
                string filePath = getFilePath();
                // check if the file exists
                if (File.Exists(filePath))
                {
                    //Open the file

                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath); // loading file
                    XmlNodeList xnList = doc.GetElementsByTagName("userdb");
                    if (xnList.Count > 0)
                    {
                        // update xml nodes
                        xnList[0]["address"].InnerText = _dbAdrress;
                        xnList[0]["name"].InnerText = _dbName;
                        xnList[0]["server"].InnerText = _server;
                        xnList[0]["port"].InnerText = _port;
                        xnList[0]["portseparator"].InnerText = _portseparator;
                        xnList[0]["username"].InnerText = _username;
                        xnList[0]["password"].InnerText = _password;
                        xnList[0]["connectionstring"].InnerText = _connecrtionstring;

                        //save xml
                        doc.Save(filePath);
                    }
                    else
                    {
                        //erro
                        throw new Exception("error while getting nodes from config XML");
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private string getFilePath()
        {
            //check if the config file exists
            try
            {
                ClassDBhandler dbhandler = new ClassDBhandler("");
                return dbhandler._configFilePath;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

    }
}