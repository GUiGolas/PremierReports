using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace PremierReports_v_1_0
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logoff_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        private void LogOut()
        {

            
            // clear the data from session
                if (Session["user"].ToString().Length > 0)
                {
                    Session["user"] = string.Empty;
                    Session["password"] = string.Empty;
                    Session["logged"] = false;
                }

                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
        }
    }
}