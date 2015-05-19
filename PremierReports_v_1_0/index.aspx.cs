using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Web.Security;

namespace PremierReports_v_1_0
{
    public partial class index : System.Web.UI.Page
    {
        private Int16 langNumber = 0;

        public const string PostBackEventTarget = "__EVENTTARGET";

        public const string LanguageDropDownID = "ctl00$cphHeader$Header1$ddlLanguage";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.DisplayName.ToString() != "Português (Brasil)")
            {
                langNumber = 1;
                ddlLanguage.SelectedIndex = 1;
                applyTranslation(1);
            }
            else
            {
                langNumber = 0;
                ddlLanguage.SelectedIndex = 0;
                applyTranslation(0);
            }
        }

        private void applyTranslation(short p)
        {
            try
            {

                lblLanguage.Text = LanguageConstants.getErrorMessage("Language", p) + ":";
                lblPassword.Text = LanguageConstants.getErrorMessage("Password", p) + ":";
                lblUserName.Text = LanguageConstants.getErrorMessage("UserName", p) + ":";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //   *** Validate fields *** 
            try
            {
                // Validade userName field
                if (txtUserName.Text.Trim().Length < 3)
                {
                    throw new Exception(ErrorConstants.getErrorMessage("userNameMinLength", langNumber));
                }
                // Validade the password field
                if ( txtPassword.Text.Trim().Length < 4)
                {
                    throw new Exception(ErrorConstants.getErrorMessage("passwordMinLength", langNumber));
                }

                // Verify if the login is valid
                ClassLoginDbHandler loginHandler = new ClassLoginDbHandler();
                if (!loginHandler.CheckUser(txtUserName.Text.Trim(), txtPassword.Text.Trim())) throw new Exception(ErrorConstants.getErrorMessage("loginFailed",langNumber));

                //save logged user in application Session
                Session["user"] = loginHandler.userName;
                Session["password"] = loginHandler.hash;
                Session["logged"] = true;

                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(loginHandler.userName, false);

                //Login successful lets put him to requested page
                //string returnUrl = Request.QueryString["ReturnUrl"] as string;

                string returnUrl = "main.aspx";

                if (returnUrl != null)
                {
                    Response.Redirect(returnUrl);
                }
                else
                {
                    //no return URL specified so lets kick him to home page
                    Response.Redirect("index.aspx");
                }

            }
            catch (Exception ex)
            {
                lblLoginError.Text = ex.Message;
                lblLoginError.Visible = true;
                //throw;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCulture();
        }



        protected override void InitializeCulture()
        {
            //change culture
            if (Request[PostBackEventTarget] != null)
            {
                string controlID = Request[PostBackEventTarget];

                if (controlID.Equals("ddlLanguage"))
                {
                    string selectedValue =
                           Request.Form[Request[PostBackEventTarget]].ToString();

                    switch (selectedValue)
                    {
                        case "pt-BR": SetCulture("pt-BR", "pt-BR");
                            break;
                        case "en-US": SetCulture("en-US", "en-US");
                            break;
                        default:
                            SetCulture("pt-BR", "pt-BR");

                            break;
                    }
                }
            }
           
            if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
                Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
            }

            //call the method to apply the culture change
            base.InitializeCulture();
            
        }

        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            ///<span class="code-SummaryComment"><remarks></span>
            ///Saving the current thread's culture set by the User in the Session
            ///so that it can be used across the pages in the current application.
            ///<span class="code-SummaryComment"></remarks></span>
            Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
            Page.Culture = name;
        }
    }
}