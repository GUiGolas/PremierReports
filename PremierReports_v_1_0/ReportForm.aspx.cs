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

namespace PremierReports_v_1_0
{
    public partial class ReportForm : System.Web.UI.Page
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

            //if (1 == 1) return;
            if (!IsPostBack && ddlEvents.Items.Count < 1)
            {
                DataTable dtEvents = new DataTable();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT AlarmConfigId, AlarmName FROM AlarmConfig", con);
                        adapter.Fill(dtEvents);
                        //filling the dropdownlist
                        //ddlEvents.DataSource = dtEvents.Rows;
                        //ddlEvents.DataTextField = "AlarmName";
                        //ddlEvents.DataValueField = "AlarmId";
                        for (int i = 0; i < dtEvents.Rows.Count; i++)
                        {
                            ListItem eventItem = new ListItem();
                            eventItem.Text = dtEvents.Rows[i].ItemArray[1].ToString();
                            eventItem.Value = dtEvents.Rows[i].ItemArray[0].ToString();
                            ddlEvents.Items.Add(eventItem);
                        }

                        ddlEvents.Items.Insert(0, new ListItem("<Selecione o evento>", "0"));


                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message.ToString();
                        lblError.Visible = true;
                        //throw;
                    }
                }
            }

        }

        protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvents.SelectedIndex > 0)
            {
                if (lsbEvents.Items.Count == 3)
                {
                    lblError.Visible = true;
                    lblError.Text = ErrorConstants.getErrorMessage("numberEventsExceded", langNumber);

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
                ReportDataSource rds = new ReportDataSource("dsSource", (DataTable)dt);
                ReportViewerEvents.LocalReport.DataSources.Add(rds);
                ReportViewerEvents.LocalReport.Refresh();
                
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