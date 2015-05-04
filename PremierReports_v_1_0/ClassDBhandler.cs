using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Class to handler the connection with the DataBase. Only this class can access the DB. A sql command query is required to the contructor
/// Every method to handler the database must be inside this class
/// </summary>
public class ClassDBhandler:IDisposable
{
    #region Private Variables
    //Private Variables
    private string sqlString = "";
    private string connectionString;
    private DataSet ds;
    private DataTable dt;
    bool disposed;
    #endregion

    #region Properties
    /// <summary>
    /// Query to be executed in database
    /// <value>String</value>
    /// </summary>
    public string sqlQuery { get { return sqlString; } set { this.sqlString = value; } }
    /// <summary>
    /// Dataset where the result of the Query is stored
    /// <value>DataSet</value>
    /// </summary>
    public DataSet DataSet { get { return ds; } }
    /// <summary>
    /// DataTable where the result of the query is stored
    /// <value>DataTable</value>
    /// </summary>
    public DataTable DataTable { get { return dt; } }
    #endregion

    #region Constructors
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="sqlQuery">Query to be executed in database</param>
    public ClassDBhandler(string sqlQuery)
    {
        //
        // CONSTRUCTOR: Require a sql command String (Query)
        //
        this.sqlString = sqlQuery;

        try
        {
            //get the connectionString from web.config
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;
        }
        catch (Exception ex)
        {
            ex.Data.Add("User Message", "Error while attempt to get the connection string from the configuration file!");
            throw ex;
        }


    } //end of constructor 
    #endregion

    #region Functions
    /// <summary>
    /// Use this function to Execute the sql. If the sql query is empty, an error will occur. 
    /// <value>empty</value>
    /// </summary>
    public void ExecuteSql()
    {
        //Check if the connection string is not empty
        if (sqlString.Length < 2) throw new Exception("Missing the String sql");
        else
        {
            using (SqlConnection con = new SqlConnection(connectionString)) //Creating a new sql connection
            {
                this.ds = new DataSet();
                this.dt = new DataTable(); //Creating a new DataTable
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlString, con); // Creating a new Sql Adapter
                    adapter.Fill(ds); // Execute the sql and return a DataTable

                    if (ds.Tables.Count > 0) dt = ds.Tables[0];

                }
                catch (Exception ex)
                {
                    ex.Data.Add("User Message", "Error while attempt to execute Sql. Please check the Query and the connection string in the webstie configuration file.");
                    throw;
                }

            }//end using
        }//end else
    }// end ExecuteSQl

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                this.ds.Dispose();
                this.dt.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

}//end of class