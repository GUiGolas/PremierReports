using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Contains all SQL Queries to generate carts
/// </summary>
public class ClassChartSql
{
   
    #region Variables
    private string id = "";
    private string InitialDate;
    private string FinalDate = "00:00";
    private string InitialHour;
    private string FinalHour = "00:00";
    private char AggregateBy = 'h';
    private DataSet  _ds;
    private DataTable _dt;
    #endregion
    
    #region Properties
    /// <summary>
    /// Ids to be filtered by query
    /// </summary>
    public String IDs { get { return id.ToString(); } set { id = value; } }
    /// <summary>
    /// DataSet where the result of Query is stored
    /// </summary>
    public DataSet Dataset { get {return _ds ;}}
    
    /// <summary>
    /// DataTable where the result of Query is stored
    /// </summary>
    public DataTable DataTable { get {return _dt ;}}
    #endregion
    
    #region Construtors
    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="_ids"></param> Required: List of IDs to be filtered in query separeted by comma. <value>String</value>
    /// <param name="_InitialDate"></param> Required: Initial date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value>
    /// <param name="_FinalDate"></param> Required: Final date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value>
    /// <param name="_AggregateBy"></param> Optional: The kind of Aggregation to be done.Inform 'h' to aggregate by hour, 'd' to aggregate by day and 'm' to aggregate by month. DEFAULT: 'h'<value>Char</value>
    /// <param name="_InitialHour"></param> Optional: Initial hour to be filtered. The format must be '00:00'. Default: '00:00' <value>String</value>
    /// <param name="_FinalHour"></param> Optional: Final hour to be filtered. The format must be '00:00'. Default: '00:00' <value>String</value>
    public ClassChartSql(string _ids, string _InitialDate, string _FinalDate, char _AggregateBy = 'h', string _InitialHour = "00:00", string _FinalHour = "00:00")
    {
        this.id = _ids;
        this.InitialDate = _InitialDate;
        this.FinalDate = _FinalDate;
        this.AggregateBy = _AggregateBy;
        this.InitialHour = _InitialHour;
        this.FinalHour = _FinalHour;
    } 
    #endregion

    #region Functions
    /// <summary>
    /// Function to  mount and after execute the Query
    /// </summary>
    public void ProcessQuery()
    {
        StringBuilder Query = new StringBuilder();
        switch (AggregateBy)
        {
            case 'd': //Aggregate per day
                //Build the sql 
                Query.Clear();
                Query.AppendLine("SELECT AlarmConfigId As 'ID', DAY(LocalStartTime)As 'TIME', count(Day(LocalStartTime))As 'QTD' ");
                Query.AppendLine("FROM [DvmsData].[dbo].[AlarmEvent] ");
                Query.Append("WHERE AlarmConfigId IN (");
                Query.Append(id);
                Query.Append(") AND LocalStartTime BETWEEN ");
                Query.Append("'"+ InitialDate + " ");
                Query.Append(InitialHour + "' ");
                Query.Append("AND ");
                Query.Append("'" + FinalDate + " ");
                Query.Append(FinalHour + "' ");
                Query.Append("GROUP BY AlarmConfigId, DAY(LocalStartTime) ");
                Query.Append("ORDER BY 1,2");

                break;
            case 'm': //Aggregate per month
                //Build the sql 
                Query.Clear();
                Query.AppendLine("SELECT AlarmConfigId As 'ID' , MONTH(LocalStartTime)As 'TIME', count(MONTH(LocalStartTime))As 'QTD' ");
                Query.AppendLine("FROM [DvmsData].[dbo].[AlarmEvent] ");
                Query.Append("WHERE AlarmConfigId IN (");
                Query.Append(id);
                Query.Append(") AND LocalStartTime BETWEEN ");
                Query.Append("'"+ InitialDate + " ");
                Query.Append(InitialHour + "' ");
                Query.Append("AND ");
                Query.Append("'" + FinalDate + " ");
                Query.Append(FinalHour + "' ");
                Query.Append("GROUP BY AlarmConfigId, MONTH(LocalStartTime) ");
                Query.Append("ORDER BY 1,2");

                break;
            case 'h': //Aggregate per hour
                //Build the sql 
                Query.Clear();
                Query.AppendLine("SELECT AlarmConfigId As 'ID' ,CONVERT(VARCHAR(13),LocalStartTime,120)+':00:00.000' AS TIME,COUNT(*)AS QTD ");
                Query.AppendLine("FROM AlarmEvent ");
                Query.Append("WHERE AlarmConfigId IN( ");
                Query.Append(id);
                Query.Append(") AND LocalStartTime BETWEEN ");
                Query.Append("'"+ InitialDate + " ");
                Query.Append(InitialHour + "' ");
                Query.Append("AND ");
                Query.Append("'" + FinalDate + " ");
                Query.Append(FinalHour + "' ");
                Query.AppendLine("GROUP BY AlarmConfigID, CONVERT(VARCHAR(13),LocalStartTime,120)+':00:00.000'");
                Query.AppendLine(" ORDER BY CONVERT(VARCHAR(13),LocalStartTime,120)+':00:00.000'");
                
                break;
            default:
                throw new Exception("Option of aggregation is not valid");
                
        }

        //Execute Query
        ExecuteQuery(Query.ToString());

    }//end Process Query


    private void ExecuteQuery(string Query)
    {
        ClassDBhandler DBexec = new ClassDBhandler(Query);

        try
        {
            DBexec.ExecuteSql();
            this._ds = DBexec.DataSet;
            this._dt = DBexec.DataTable;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
        finally
        {
            DBexec.Dispose();
        }
    }//end Execute Query

    #endregion
}