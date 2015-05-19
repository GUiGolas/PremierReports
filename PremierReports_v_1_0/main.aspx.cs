using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Threading;
using System.Globalization;

namespace PremierReports_v_1_0
{
    public partial class main : System.Web.UI.Page
    {
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
        }

        private void applyTranslation(short p)
        {
            try
            {
                lblAuditTab.Text = LanguageConstants.getErrorMessage("Audit", p) + ":";
                lblChartTab.Text = LanguageConstants.getErrorMessage("Chart", p) + ":";
                lblEventTab.Text = LanguageConstants.getErrorMessage("Events", p) + ":";

                lnkButtonSettings.Text = LanguageConstants.getErrorMessage("Settings", p) + ":";

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