using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Threading;

namespace PremierReports_v_1_0
{
    public partial class AuditForm : System.Web.UI.Page
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

            if (!IsPostBack && ddlEvents.Items.Count < 1)
            {
                try
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
                catch (Exception ex)
                {

                    lblError.Text = ex.Message.ToString();
                    lblError.Visible = true;
                    //throw;
                }
                
            }
            

        }

        private void applyTranslation(short p)
        {
            try
            {

                lblAddEventsR.Text = LanguageConstants.getMessage("UserName", p) + ":*";
                lblDateFinR.Text = LanguageConstants.getMessage("FinalDate", p) + ":*";
                lblDateIniR.Text = LanguageConstants.getMessage("StartDate", p) + ":";
                

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

                lsbEvents.Items.Add(ddlEvents.SelectedItem);
                lblError.Visible = false;

            }
        }

        protected void bntReportSubmit_Click(object sender, EventArgs e)
        {
            //Check Required Fields
            if (inpDateFin.Value.ToString().Length < 1 || inpDateIni.Value.ToString().Length < 1)
            {
                lblError.Text = ErrorConstants.getMessage("missingFields", langNumber);
                lblError.Visible = true;
            }
            else
            {
                //proccess submit
                ProcessSubmit();
            }
            
        }

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

                DateTime _dtIni = new DateTime();
                DateTime _dtFn = new DateTime();
                
                _dtIni = DateTime.Parse(dateini);
                _dtFn = DateTime.Parse(datefim);



                if(DateTime.Compare(_dtIni,_dtFn) > 0 ){
                    throw new Exception(ErrorConstants.getMessage("dateError", langNumber));
                }

                // if selected the first item in the dorpodown means bring all users
                if (ddlEvents.SelectedIndex == 0 && lsbEvents.Items.Count == 0)
                {
                    for (int i = 0; i < ddlEvents.Items.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            names = "'" + ddlEvents.Items[i + 1].Text.ToString().Trim() + "'";
                        }
                        else
                        {
                            names = names + ", " + "'" + ddlEvents.Items[i + 1].Text.ToString().Trim() + "'";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lsbEvents.Items.Count; i++)
                    {
                        if (i == 0)
                        {
                            names = "'" + lsbEvents.Items[i].Text.ToString().Trim() + "'";
                        }
                        else
                        {
                            names = names + ", " + "'" + lsbEvents.Items[i].Text.ToString().Trim() + "'";
                        }
                    }
                }

                //Data aquisistion and report generation
                dstAuditReport.dsAuditReportDataTable dt = new dstAuditReport.dsAuditReportDataTable();
                ClassDBAuditSql auditHandler = new ClassDBAuditSql(names, dateini, datefim, hourIni, hourFim);

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

                rpwAudit.LocalReport.DataSources.Clear();
                rpwAudit.Visible = true;
                ReportDataSource rds = new ReportDataSource("dsAuditionSource", (DataTable)dt);
                rpwAudit.LocalReport.DataSources.Add(rds);
                rpwAudit.LocalReport.Refresh();

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

        protected void bntClear_Click(object sender, EventArgs e)
        {
            if (lsbEvents.Items.Count > 0)
            {
                lsbEvents.Items.Clear();
                rpwAudit.Visible = false;
            }
        }
    }
}