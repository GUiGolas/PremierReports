using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Threading;

namespace PremierReports_v_1_0
{
    public partial class ReportForm : System.Web.UI.Page
    {
        /// get ConnectionString
        /// 
        //String conString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;
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

            //if (1 == 1) return;
            if (!IsPostBack && ddlEvents.Items.Count < 1)
            {
                try
                {



                    DataTable dtEvents = new DataTable();
                    ClassDBhandler handler = new ClassDBhandler("SELECT AlarmConfigId, AlarmName FROM AlarmConfig");
                    handler.ExecuteSql();
                    dtEvents = handler.DataTable;
                    //DataTable dtEvents = new DataTable();
                    //using (SqlConnection con = new SqlConnection(conString))
                    //{
                    //    try
                    //    {
                    //        SqlDataAdapter adapter = new SqlDataAdapter("SELECT AlarmConfigId, AlarmName FROM AlarmConfig", con);
                    //        adapter.Fill(dtEvents);
                    //        //filling the dropdownlist
                    //        //ddlEvents.DataSource = dtEvents.Rows;
                    //        //ddlEvents.DataTextField = "AlarmName";
                    //        //ddlEvents.DataValueField = "AlarmId";
                    for (int i = 0; i < dtEvents.Rows.Count; i++)
                    {
                        ListItem eventItem = new ListItem();
                        eventItem.Text = dtEvents.Rows[i].ItemArray[1].ToString();
                        eventItem.Value = dtEvents.Rows[i].ItemArray[0].ToString();
                        ddlEvents.Items.Add(eventItem);
                    }

                    if (langNumber == 0)
                    {
                        ddlEvents.Items.Insert(0, new ListItem("<Selecione o evento>", "0"));
                    }
                    else
                    {
                        ddlEvents.Items.Insert(0, new ListItem("<Select the event>", "0"));
                    }

                    


                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message.ToString();
                    lblError.Visible = true;
                    //throw;
                }
                //}
            }

        }

        private void applyTranslation(short p)
        {
            try
            {

                lblAddEventsR.Text = LanguageConstants.getMessage("Events", p) + ":";
                lblDateFinR.Text = LanguageConstants.getMessage("FinalDate", p) + ":*";
                lblDateIniR.Text = LanguageConstants.getMessage("StartDate", p) + ":*";
                lblOrderbyID.Text = LanguageConstants.getMessage("OrderByID", p) + ":";

                bntClear.Text = LanguageConstants.getMessage("Clear", p);
                bntReportSubmit.Text = LanguageConstants.getMessage("Update", p);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvents.SelectedIndex > 0)
            {
                if (lsbEvents.Items.Count == 3)
                {
                    lblError.Visible = true;
                    lblError.Text = ErrorConstants.getMessage("numberEventsExceded", langNumber);

                }
                else
                {
                    lsbEvents.Items.Add(ddlEvents.SelectedItem);
                    lblError.Visible = false;
                }


            }
        }

        protected void bntReportSubmit_Click(object sender, EventArgs e)
        {
            // Verify if there are events selected
            if (lsbEvents.Items.Count > 0)
            {
                //proccess submit
                ProcessSubmit();
            }
        }

        protected void bntClear_Click(object sender, EventArgs e)
        {
            if (lsbEvents.Items.Count > 0)
            {
                lsbEvents.Items.Clear();
            }
        }


        #region Functions
        protected void ProcessSubmit()
        {

            string dateini;
            string datefim;
            string hourIni;
            string hourFim;
            bool orderById = false;
            string events = string.Empty;


            try
            {

                // put date in the correct format yyyy/dd/mm
                string[] dtini = inpDateIni.Value.ToString().Trim().Split('.');
                string[] dtfim = inpDateFin.Value.ToString().Trim().Split('.');

                dateini = dtini[2] + "/";
                dateini = dateini + dtini[1] + "/";
                dateini = dateini + dtini[0];

                datefim = dtfim[2] + "/";
                datefim = datefim + dtfim[1] + "/";
                datefim = datefim + dtfim[0];
                
                hourIni = ddlHourIni.SelectedValue.ToString().Trim();
                hourFim = ddlHourFin.SelectedValue.ToString().Trim();

                DateTime _dtIni = new DateTime();
                DateTime _dtFn = new DateTime();

                _dtIni = DateTime.Parse(dateini);
                _dtFn = DateTime.Parse(datefim);



                if (DateTime.Compare(_dtIni, _dtFn) > 0)
                {
                    throw new Exception(ErrorConstants.getMessage("dateError", langNumber));
                }

                for (int i = 0; i < lsbEvents.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        events = events + lsbEvents.Items[i].Value.ToString().Trim();
                    }
                    else
                    {
                        events = events + " , " + lsbEvents.Items[i].Value.ToString().Trim();
                    }
                }

                if (chkOrderById.Checked)
                {
                    orderById = true;
                }

                //Data aquisistion and report generation
                dstEventReport.dtEventsReportDataTable dt = new dstEventReport.dtEventsReportDataTable();
                ClassDBReportSql reportHandler = new ClassDBReportSql(events, dateini, datefim, hourIni, hourFim, orderById);

                //process query

                reportHandler.ProcessQuery();

                for (int i = 0; i < reportHandler.DataTable.Rows.Count; i++)
                {
                    DateTime stTime = (DateTime)reportHandler.DataTable.Rows[i].ItemArray[0];
                    int ID = (int)reportHandler.DataTable.Rows[i].ItemArray[1];
                    string name = (string)reportHandler.DataTable.Rows[i].ItemArray[2];
                    int pr = (int)reportHandler.DataTable.Rows[i].ItemArray[3];
                    string recName = (string)reportHandler.DataTable.Rows[i].ItemArray[4];
                    DateTime endTime = (DateTime)reportHandler.DataTable.Rows[i].ItemArray[5];

                    dt.AdddtEventsReportRow(stTime, ID, name, pr, recName, endTime); 
                 
                }

                //fill the report
                ReportViewerEvents.LocalReport.DataSources.Clear();
                ReportViewerEvents.Visible = true;
                ReportDataSource rds = new ReportDataSource("dsSource", (DataTable)dt);
                ReportViewerEvents.LocalReport.DataSources.Add(rds);
                ReportViewerEvents.LocalReport.Refresh();

                lblError.Visible = false;
                ddlEvents.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                lblError.Visible = true;
                //throw;
            }


        }

        #endregion


    }


}