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
    public partial class GraphicsForm : System.Web.UI.Page
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
                DataTable dtEvents = new DataTable();

                    try
                    {
                        ClassDBhandler handler = new ClassDBhandler("SELECT AlarmConfigId, AlarmName FROM AlarmConfig");
                        handler.ExecuteSql();
                        dtEvents = handler.DataTable;
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

                        if (langNumber == 0)
                        {
                            ddlEvents.Items.Insert(0, new ListItem("<Selecione o evento>", "0"));
                        }
                        else
                        {
                            ddlEvents.Items.Insert(0, new ListItem("<Select the event>", "0"));
                        }

                        //fill the ChartType dropdown list
                        if (langNumber == 1)
                        {
                            ddlChartType.Items.Add(new ListItem("Area", "Area"));
                            ddlChartType.Items.Add(new ListItem("Bar", "Bar"));
                            ddlChartType.Items.Add(new ListItem("Column", "Column"));
                            ddlChartType.Items.Add(new ListItem("Line", "Line"));
                            
                        }
                        else
                        {
                            ddlChartType.Items.Add(new ListItem("Área", "Area"));
                            ddlChartType.Items.Add(new ListItem("Barra", "Bar"));
                            ddlChartType.Items.Add(new ListItem("Coluna", "Column"));
                            ddlChartType.Items.Add(new ListItem("Linha", "Line"));
                        }

                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message.ToString();
                        lblError.Visible = true;
                        
                    }
                
            }

        }

        private void applyTranslation(short p)
        {
            try
            {

                lblAddEventsR.Text = LanguageConstants.getMessage("Events", p) + ":";
                lblDateFinR.Text = LanguageConstants.getMessage("FinalDate", p) + ":*";
                lblDateIniG.Text = LanguageConstants.getMessage("StartDate", p) + ":*";
                lblChartType.Text = LanguageConstants.getMessage("ChartType", p) + ":";
                
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
                    lblError.Text = lblError.Text = ErrorConstants.getMessage("numberEventsExceded", langNumber);

                }
                else
                {
                    lsbEvents.Items.Add(ddlEvents.SelectedItem);
                    lblError.Visible = false;
                }


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
            // Verify if there are events selected
            if (lsbEvents.Items.Count > 0)
            {
                //proccess submit
                ProcessSubmit();
            }
        }


        #region Functions
        protected void ProcessSubmit()
        {

            string dateini;
            string datefim;
            string hourIni;
            string hourFim;
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


                // Check the agregation 

               // DateTime _stTime, _fnTime;
                //_stTime = Convert.ToDateTime(inpDateIni.Value);
                //_fnTime = Convert.ToDateTime(inpDateFin.Value);

                //char aggregate = 'h';

                //if (_fnTime.Subtract(_stTime) > new TimeSpan(2, 0, 0, 0, 0))
                //{
                //    aggregate = 'd';
                    
                //    if (_fnTime.Subtract(_stTime) > new TimeSpan(60, 0, 0, 0))
                //    {
                //        aggregate = 'm';
                //    }
                //}

                char aggregate = 'h';

                if (_dtFn.Subtract(_dtIni) > new TimeSpan(2, 0, 0, 0, 0))
                {
                    aggregate = 'd';

                    if (_dtFn.Subtract(_dtIni) > new TimeSpan(60, 0, 0, 0))
                    {
                        aggregate = 'm';
                    }
                }

                //Data aquisistion and report generation
                //dstEventReport.dtEventsReportDataTable dt = new dstEventReport.dtEventsReportDataTable();
                ClassChartSql chartHandler = new ClassChartSql(events, dateini, datefim, aggregate, hourIni, hourFim);

                ////process query

                chartHandler.ProcessQuery();
                //Clear chart
                ReportChart.Series.Clear();
                ReportChart.Visible = true;
                try
                {
                    ReportChart.DataBindCrossTable(chartHandler.DataTable.AsEnumerable(), "ID", "TIME", "QTD", "Label=QTD");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message + "\n\n**" + ErrorConstants.getMessage("chartError", langNumber);
                    lblError.Visible = true;
                    ddlEvents.SelectedIndex = 0;
                    return;
                    //throw;
                }
                ReportChart.Titles.Add("Relatório Premier");
                ReportChart.ChartAreas[0].AxisX.Title = "Time";
                ReportChart.ChartAreas[0].AxisY.Title = "QTD";
                ReportChart.Legends.Clear();

               
                Dictionary<int, string> dictionary = new Dictionary<int, string>();



                foreach (ListItem item in lsbEvents.Items)
                {
                    dictionary.Add(Convert.ToInt32(item.Value), item.Text);
                }

                Dictionary<int, string> dict = dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                int j = -1;
                foreach (var series in ReportChart.Series)
                {
                    switch (ddlChartType.SelectedValue)
                    {
                        case "Area":
                            series.ChartType = SeriesChartType.StackedArea;
                            break;
                        case "Bar":
                            series.ChartType = SeriesChartType.Bar;
                            break;
                        case "Column":
                            series.ChartType = SeriesChartType.Column;
                            break;
                        case "Line":
                            series.ChartType = SeriesChartType.Line;
                            series.BorderWidth = 2;
                            break;
                        default:
                            series.ChartType = SeriesChartType.Line;
                            series.BorderWidth = 2;
                            break;
                    }




                    //series.IsVisibleInLegend = true;

                    string _legendName = dict[Convert.ToInt16(lsbEvents.Items[j+1].Value.ToString())];
                    ReportChart.Legends.Add(new Legend(_legendName) { Docking = Docking.Bottom, Name = _legendName, Title = _legendName });

                    //series.Legend = lsbEvents.Items[j + 1].Text.ToString();

                    ReportChart.Series[j + 1].Legend = ReportChart.Legends[j + 1].Name;

                    j++;
                }


               //// one series for each id
               // string id = "-1";
               // Int16 id_count = -1;

               // for (int i = 0; i < chartHandler.DataTable.Rows.Count; i++)
               // {
               //     if (id != chartHandler.DataTable.Rows[i].ItemArray[0].ToString())
               //     {
               //         id = chartHandler.DataTable.Rows[i].ItemArray[0].ToString();
               //         id_count++;

               //         ReportChart.Series.Add(lsbEvents.Items[id_count].Text);
               //         ReportChart.Series[lsbEvents.Items[id_count].Text].Name = lsbEvents.Items[id_count].Text;
               //         ReportChart.Series[lsbEvents.Items[id_count].Text].ChartType = SeriesChartType.Line;
               //         ReportChart.Series[lsbEvents.Items[id_count].Text]["DrawningStyle"] = "Emboss";
               //         ReportChart.Series[lsbEvents.Items[id_count].Text].AxisLabel = "TIME";
               //     }

               //     // adding each cell to chart

               //     object xvalue;

               //     if (chartHandler.DataTable.Rows[i].ItemArray[1].ToString().Length > 2)
               //     {
               //         xvalue = chartHandler.DataTable.Rows[i].ItemArray[1].ToString().Substring(11, 2);
               //     }
               //     else
               //     {
               //         xvalue = chartHandler.DataTable.Rows[i].ItemArray[1].ToString().Trim();
               //     }

               //     //object[] yvalue = new object[]{chartHandler.DataTable.Rows[i].ItemArray[2]};

               //     ReportChart.Series[lsbEvents.Items[id_count].Text].Points.AddXY((double)xvalue, Convert.ToDouble(chartHandler.DataTable.Rows[i].ItemArray[2].ToString()));
                    
                    
               // }
           
              



                    
                //ReportChart.DataSource = chartHandler.DataTable;
                //for (int i = 0; i < lsbEvents.Items.Count; i++)
                //{
                //    ReportChart.Series.Add(lsbEvents.Items[i].Text);
                //    ReportChart.Series[i].Name = lsbEvents.Items[i].Text;
                //    ReportChart.Series[lsbEvents.Items[i].Text].ChartType = SeriesChartType.Line;
                //    ReportChart.Series[lsbEvents.Items[i].Text]["DrawningStyle"] = "Emboss";
                //    ReportChart.Series[lsbEvents.Items[i].Text].XValueMember = "TIME";
                //    ReportChart.Series[lsbEvents.Items[i].Text].YValueMembers = "QTD";
                //}

                //ReportChart.DataBind();


                //reportHandler.ProcessQuery();

                //for (int i = 0; i < reportHandler.DataTable.Rows.Count; i++)
                //{
                //    DateTime stTime = (DateTime)reportHandler.DataTable.Rows[i].ItemArray[0];
                //    int ID = (int)reportHandler.DataTable.Rows[i].ItemArray[1];
                //    string name = (string)reportHandler.DataTable.Rows[i].ItemArray[2];
                //    int pr = (int)reportHandler.DataTable.Rows[i].ItemArray[3];
                //    string recName = (string)reportHandler.DataTable.Rows[i].ItemArray[4];
                //    DateTime endTime = (DateTime)reportHandler.DataTable.Rows[i].ItemArray[5];

                //    dt.AdddtEventsReportRow(stTime, ID, name, pr, recName, endTime);

                //}

                ////fill the report
                //ReportViewerEvents.LocalReport.DataSources.Clear();
                //ReportDataSource rds = new ReportDataSource("dsSource", (DataTable)dt);
                //ReportViewerEvents.LocalReport.DataSources.Add(rds);
                //ReportViewerEvents.LocalReport.Refresh();

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