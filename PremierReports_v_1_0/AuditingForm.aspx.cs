using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PremierReports_v_1_0
{
    public partial class AuditingForm : System.Web.UI.Page
    {

        /// get ConnectionString
        /// 
        
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

            try
            {
                if (!IsPostBack && ddlEvents.Items.Count < 1)
                {
                    DataTable dtEvents = new DataTable();

                    ClassDBAuditSql auditHandler = new ClassDBAuditSql();
                    auditHandler.AvailableUserNames();

                    for (int i = 0; i < auditHandler.DataTable.Rows.Count; i++)
                    {
                        ddlEvents.Items.Add(auditHandler.DataTable.Rows[i].ItemArray[0].ToString());
                    }

                    if (langNumber == 0)
                    {
                        ddlEvents.Items.Insert(0, new ListItem("<Todos>", "0"));
                    }
                    else
                    {
                        ddlEvents.Items.Insert(0, new ListItem("<All>", "0"));
                    }




                }
                else
                {
                    ReportViewerAudit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.ToString();
                lblError.Visible = true;
                //throw;
            }

            
        }

        protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvents.SelectedIndex > 0)
            {
                
                    lsbEvents.Items.Add(ddlEvents.SelectedItem);
                    lblError.Visible = false;
               
            }
        }

        protected void bntClear_Click(object sender, EventArgs e)
        {
            if (lsbEvents.Items.Count > 0)
            {
                lsbEvents.Items.Clear();
            }
        }

        protected void bntReportSubmit_Click(object sender, EventArgs e)
        {
            //proccess submit
            ProcessSubmit();
        }


        #region functions

        private void ProcessSubmit()
        {
            string dateini;
            string datefim;
            string hourIni;
            string hourFim;
            string names = string.Empty;


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


                if (ddlEvents.SelectedIndex == 0 && lsbEvents.Items.Count == 0)
                {
                    for (int i = 0; i < ddlEvents.Items.Count -1 ; i++)
                    {
                        if (i == 0)
                        {
                            names = "'" + ddlEvents.Items[i + 1].Text.ToString().Trim() + "'" ;
                        }
                        else
                        {
                            names = names + ", " + "'"  +  ddlEvents.Items[i + 1].Text.ToString().Trim() + "'" ;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lsbEvents.Items.Count; i++)
                    {
                        if (i == 0)
                        {
                            names = "'" + lsbEvents.Items[i].Text.ToString().Trim() + "'" ;
                        }
                        else
                        {
                            names = names + ", " + "'" + lsbEvents.Items[i].Text.ToString().Trim() + "'" ;
                        }
                    }
                }


                //Data aquisistion and report generation
                dstAuditReport.dsAuditReportDataTable dt = new dstAuditReport.dsAuditReportDataTable();
                ClassDBAuditSql auditHandler = new ClassDBAuditSql(names,dateini,datefim,hourIni,hourFim);

                //process query
                auditHandler.ProcessQuery();

                for (int i = 0; i < auditHandler.DataTable.Rows.Count; i++)
                {
                    int ID = (int)auditHandler.DataTable.Rows[i].ItemArray[0];
                    DateTime stTime = (DateTime)auditHandler.DataTable.Rows[i].ItemArray[1];
                    string name = (string)auditHandler.DataTable.Rows[i].ItemArray[2];
                    int eventCode = (int)auditHandler.DataTable.Rows[i].ItemArray[3];
                    string eventDesc = (string)auditHandler.DataTable.Rows[i].ItemArray[4];
                    string eventStatus = (string)auditHandler.DataTable.Rows[i].ItemArray[5];
                    string _object = (string)auditHandler.DataTable.Rows[i].ItemArray[6];
                    string recName = (string)auditHandler.DataTable.Rows[i].ItemArray[7];
                    string recAddr = (string)auditHandler.DataTable.Rows[i].ItemArray[8];

                    dt.AdddsAuditReportRow(ID, stTime, name, eventCode, eventDesc, eventStatus, _object, recName, recAddr);

                }

                //fill the report
                ReportViewerAudit.LocalReport.DataSources.Clear();
                ReportViewerAudit.Visible = true;
                ReportDataSource rds = new ReportDataSource("dsAuditSource", (DataTable)dt);
                ReportViewerAudit.LocalReport.DataSources.Add(rds);
                ReportViewerAudit.LocalReport.Refresh();
                

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