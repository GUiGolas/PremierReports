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
/// Summary description for ClassLoginDbHandler
/// </summary>
public class ClassLoginDbHandler
{
    public string userName { get; set; }
    public string hash { get; set; }
    public bool root { get; set; }
    string configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Premier Reports\config\config.xml";
    private string connectionString;
    
    public ClassLoginDbHandler()
    {
        //
        // TODO: Add constructor logic here
        //
         try
        {
            //get the connectionString from web.config
            //this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MirasysDB"].ConnectionString;

            // get the configuration strings from config file.

            getDefaultDB();

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
                XmlNodeList xnList = doc.GetElementsByTagName("logindb");
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

    public bool CheckUser(string _userName, string _hash)
    {
        DataTable dt;
        //string conection = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Visual Studio\Projects\Premier\PremierReports_v_1_0\App_Data\dbPremierReports.mdf;Integrated Security=True";


        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Password from Users where username = @uname";
                cmd.Parameters.Add(new SqlParameter("@uname", _userName));

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count < 1) return false;
                }
            }
        }

        md5 encript = new md5(_hash.Trim());
        encript.Encript();

        if (encript.hash == dt.Rows[0]["password"].ToString().Trim())
        {
            userName = _userName;
            hash = _hash;
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool ChangePassword(string _userName, string _newUserName, string _oldHash, string _newHash)
    {
        if (!CheckUser(_userName, _oldHash)) return false;
        else
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Users SET Password = @password, username = @newusername where username = @uname";
                    cmd.Parameters.Add(new SqlParameter("@uname", _userName));
                    cmd.Parameters.Add(new SqlParameter("@password", _newHash));
                    cmd.Parameters.Add(new SqlParameter("@newusername", _newUserName));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        if (da.Fill(dt) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }




        }


    }
}