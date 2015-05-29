using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Class Where is stored the Queries for reports
/// </summary>
public class ClassDBReportSql
{
    #region Variables
    private string id = "";
    private string InitialDate;
    private string FinalDate = "00:00";
    private string InitialHour;
    private string FinalHour = "00:00";
    private bool OrderById = false;
    private DataSet _ds;
    private DataTable _dt;   

    #endregion

    #region Properties
    /// <summary>
    /// DataSet where the result of Query is stored
    /// </summary>
    public DataSet DataSet { get { return _ds;}}

    /// <summary>
    /// DataTable where the result of Query is stored
    /// </summary>
    public DataTable DataTable { get {return _dt ;}}

    #endregion

    #region Constructors
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="_ids"></param> Required: The ids of desired events separated by comma. <value>String</value>
    /// <param name="_InitialDate"></param> Required: Initial date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value> 
    /// <param name="_FinalDate"></param> Required: Final date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value>
    /// <param name="_InitialHour"></param> Optional: Initial hour to be filtered. The format must be '00:00'. Default: '00:00' <value>String</value>
    /// <param name="_FinalHour"></param> Optional: Final hour to be filtered. The format must be '00:00'. Default: '00:00' <value>String</value>
    /// <param name="_OrderById"></param> Optional: Set true if you want to order the query by AlarmId
    public ClassDBReportSql(string _ids, string _InitialDate, string _FinalDate, string _InitialHour = "00:00", string _FinalHour = "00:00", bool _OrderById = false)
    {
        this.id = _ids;
        this.InitialDate = _InitialDate;
        this.FinalDate = _FinalDate;
        this.InitialHour = _InitialHour;
        this.FinalHour = _FinalHour;
        this.OrderById = _OrderById;
    }

    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="_ids"></param> Required: The ids of desired events separated by comma. <value>String</value>
    /// <param name="_InitialDate"></param> Required: Initial date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value> 
    /// <param name="_FinalDate"></param> Required: Final date to  be filtered in query. The format must be aaaa/mm/dd <value>String</value>
    /// <param name="_OrderById"></param> Optional: Set true if you want to order the query by AlarmId
    public ClassDBReportSql(string _ids, string _InitialDate, string _FinalDate, bool _OrderById = false)
    {
        this.id = _ids;
        this.InitialDate = _InitialDate;
        this.FinalDate = _FinalDate;
        this.InitialHour = "00:00";
        this.FinalHour = "00:00";
        this.OrderById = _OrderById;
    } 

    #endregion

    #region Functions

    public void ProcessQuery()
    {
        //Build Query
        StringBuilder Query = new StringBuilder();
        Query.AppendLine("SELECT AlarmEvent.LocalStartTime,AlarmConfig.AlarmConfigId,AlarmConfig.AlarmName,AlarmConfig.AlarmPriority,AlarmConfig.RecorderName,AlarmEvent.LocalStopTime ");
        Query.AppendLine("FROM AlarmEvent INNER JOIN AlarmConfig ON AlarmConfig.AlarmConfigId = AlarmEvent.AlarmConfigId ");
        Query.Append("WHERE AlarmConfig.AlarmConfigId IN ( ");
        Query.Append(id);
        Query.Append(" ) AND LocalStartTime BETWEEN ");
        Query.Append("'" + InitialDate + " ");
        Query.Append(InitialHour + "' ");
        Query.Append("AND ");
        Query.Append("'" + FinalDate + " ");
        Query.Append(FinalHour + "' ");
        //Check OrderById
        if (OrderById) Query.AppendLine("ORDER BY 2,1");
        else Query.AppendLine("ORDER BY 1,2");
       
        //Execute Query
        ExecuteQuery(Query.ToString());      

    }

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