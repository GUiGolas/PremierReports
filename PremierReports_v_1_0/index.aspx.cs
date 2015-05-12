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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentCulture.DisplayName.ToString() != "Português (Brasil)")
            {
                langNumber = 1;
                ddlLanguage.SelectedIndex = 1;
            }
            else
            {
                langNumber = 0;
                ddlLanguage.SelectedIndex = 0;
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
    }
}