using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml;

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
    //string configFilePath = @"E:\Visual Studio\Projects\Premier\PremierReports_v_1_0\config\config.xml";
    private string configFilePath =  Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Premier Reports\config\config.xml";
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

    /// <summary>
    /// The filepath of configuration file
    /// <value>String</value>
    /// </summary>
    public string _configFilePath { get { return configFilePath; }}

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
            //this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;

            // get the configuration strings from config file.

            // - first check the user settings
            if (getUserDb()) ;
            else getDefaultDB();

        }
        catch (Exception ex)
        {
            ex.Data.Add("User Message", "Error while attempt to get the connection string from the configuration file!");
            throw ex;
        }


    }

    private bool getDefaultDB()
    {
        try
        {
            // check if the file exists
            if (File.Exists(configFilePath))
            {
                //Open the file

                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath); // loading file
                XmlNodeList xnList = doc.GetElementsByTagName("default");
                string conString = xnList[0]["connectionstring"].InnerText;
                if (conString == String.Empty || conString.Length < 2)
                {
                    string _address = xnList[0]["address"].InnerText;
                    string _name = xnList[0]["name"].InnerText;
                    string _server = xnList[0]["server"].InnerText;
                    string _port = xnList[0]["port"].InnerText;
                    string _portseparator = xnList[0]["portseparator"].InnerText;
                    string _username = xnList[0]["username"].InnerText;
                    string _password = xnList[0]["password"].InnerText;


                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = _address;
                    builder.InitialCatalog = _name;

                    if (_username == string.Empty || _username.Length < 2)
                    {
                        builder.IntegratedSecurity = true;
                    }
                    else
                    {
                        builder.UserID = _username;
                        builder.Password = _password;
                    }



                    //string commandString;
                    //if (_username == string.Empty || _username.Length < 2)
                    //{
                    //    commandString =  "Integrated Security=true;";
                    //}
                    //else
                    //{
                    //    commandString = "user id=" + _username + "; password =" + _password + ";";
                    //}

                    //commandString = commandString + " server=" + _address +"; database = " + _name +  "; connection timeout=10";

                    using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                    {
                        conn.Open(); // throws if invalid
                    }
                    this.connectionString = builder.ConnectionString;

                    return true;
                }
                else
                {
                    this.connectionString = conString;
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open(); // throws if invalid
                    }
                    return true;
                }
            }
            else
            {
                throw new Exception("Error: missing configuration file, or file not acessible.");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private bool getUserDb()
    {
        try
        {
            // check if the file exists
            if (File.Exists(configFilePath))
            {
                //Open the file

                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath); // loading file
                XmlNodeList xnList = doc.GetElementsByTagName("userdb");
                string conString = xnList[0]["connectionstring"].InnerText;
                if (conString == String.Empty || conString.Length < 2)
                {
                    string _address = xnList[0]["address"].InnerText;
                    string _name = xnList[0]["name"].InnerText;
                    string _server = xnList[0]["server"].InnerText;
                    string _port = xnList[0]["port"].InnerText;
                    string _portseparator = xnList[0]["portseparator"].InnerText;
                    string _username = xnList[0]["username"].InnerText;
                    string _password = xnList[0]["password"].InnerText;


                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = _address;
                    builder.InitialCatalog = _name;

                    if (_username == string.Empty || _username.Length < 2)
                    {
                        builder.IntegratedSecurity = true;
                    }
                    else
                    {
                        builder.UserID = _username;
                        builder.Password = _password;
                    }


                    
                    //string commandString;
                    //if (_username == string.Empty || _username.Length < 2)
                    //{
                    //    commandString =  "Integrated Security=true;";
                    //}
                    //else
                    //{
                    //    commandString = "user id=" + _username + "; password =" + _password + ";";
                    //}

                    //commandString = commandString + " server=" + _address +"; database = " + _name +  "; connection timeout=10";

                    using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                    {
                        conn.Open(); // throws if invalid
                    }
                    this.connectionString = builder.ConnectionString;

                    return true;
                }
                else
                {
                    this.connectionString = conString;
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open(); // throws if invalid
                    }
                    return true;
                }
            }
            else
            {
                throw new Exception("Error: missing configuration file, or file not acessible.");
            }
        }
        catch (Exception ex )
        {
            return false;
            //throw;
        }
        
    } 
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