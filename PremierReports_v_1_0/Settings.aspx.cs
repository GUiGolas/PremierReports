using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Security;

namespace PremierReports_v_1_0
{
    public partial class Settings : System.Web.UI.Page
    {
        /// get ConnectionString
        /// 
        String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;
        private Int16 langNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.DisplayName.ToString() != "Português (Brasil)")
            {
                langNumber = 1;
            }
            else
            {
                langNumber = 0;
            }
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            // Check all fields
            try
            {
                //check if the new password and confirmation password is the same
                if (txtConfirmPassword.Text != txtNewPassword.Text)
                {
                    throw new Exception(ErrorConstants.getErrorMessage("passwordDontMatch",langNumber));
                }
                //check if the lenght of new password is greather than 3
                if (txtConfirmPassword.Text.Length < 4)
                {
                    throw new Exception(ErrorConstants.getErrorMessage("passwordlength", langNumber));
                }
                // check if the lenght of new user name is greather than 2
                if (txtNewUserName.Text.Length < 3)
                {
                    throw new Exception(ErrorConstants.getErrorMessage("userNameMinLength", langNumber));
                }
                //check if the UserName and Password exist and if is correct
                // Verify if the login is valid
                ClassLoginDbHandler loginHandler = new ClassLoginDbHandler();
                if (!loginHandler.CheckUser(txtUserName.Text.Trim(), txtPassword.Text.Trim())) throw new Exception(ErrorConstants.getErrorMessage("loginFailed", langNumber));

                // changing the password
                md5 encript = new md5(txtConfirmPassword.Text);
                encript.Encript();
                string newPass = encript.hash;

                loginHandler.ChangePassword(txtUserName.Text,txtNewUserName.Text, loginHandler.hash, newPass);
                

                //save logged user in application Session
                Session["user"] = txtNewUserName.Text;
                Session["password"] = newPass;
                Session["logged"] = false;

                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(loginHandler.userName, false);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                lblError.Visible = true;
                //throw;
            }
        }

        protected void btnFactory_Click(object sender, EventArgs e)
        {
            txtDbAddress.Text = "LOCALHOST";
            txtDbInstance.Text = "SQLEXPRESS";
            txtDbName.Text = "DvmsData";
            txtPortSeparator.Text = ",";

            //save settings
        }

    }
}