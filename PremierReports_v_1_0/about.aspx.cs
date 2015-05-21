using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;

namespace PremierReports_v_1_0
{
    public partial class about : System.Web.UI.Page
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

                lblAbout.Text = LanguageConstants.getMessage("about", p) ;
                lblContact.Text = LanguageConstants.getMessage("contact", p) ;
                txtAbout.Text = LanguageConstants.getMessage("aboutText", p) ;

               

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}