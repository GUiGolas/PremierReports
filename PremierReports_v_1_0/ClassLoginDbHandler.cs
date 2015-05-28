using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ClassLoginDbHandler
/// </summary>
public class ClassLoginDbHandler
{
    public string userName { get; set; }
    public string hash { get; set; }
    public bool root { get; set; }
    string configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"config\config.xml";
    //string userFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Premier\Relatorio SAM\config\user.xml";
    string userFilePath = AppDomain.CurrentDomain.BaseDirectory + @"config\user.xml";
    private string defaulFilePath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\dbPremierReports.mdf";

    //private string connectionString;
    
    public ClassLoginDbHandler()
    {
        //
        // TODO: Add constructor logic here
        //
         try
        {

             // Check if the file with the user names exists
            CheckUserFile();

           

            // get the configuration strings from config file.

            //getDefaultDB();

            if (!EventLog.SourceExists("SAM"))
            {
                EventLog.CreateEventSource("SAM", "Application");
            }
            EventLog.WriteEntry("SAM", "GET user.xml success",
                 EventLogEntryType.Information);

        }
        catch (Exception ex)
        {
            ex.Data.Add("User Message", "Error while attempt to get the connection string from the configuration file!");
            throw ex;
        }


    }

    private void CheckUserFile()
    {
        try
        {
            if (!File.Exists(userFilePath)) // if File doens't exist, create
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("users");
                xmlDoc.AppendChild(rootNode);

                XmlNode userNode = xmlDoc.CreateElement("user");
                XmlAttribute attribute = xmlDoc.CreateAttribute("password");
                attribute.Value = "21232f297a57a5a743894a0e4a801fc3";
                userNode.Attributes.Append(attribute);
                userNode.InnerText = "admin";
                rootNode.AppendChild(userNode);

                xmlDoc.Save(userFilePath);
            }
        }
        catch (Exception ex)
        {

            if (!EventLog.SourceExists("SAM"))
            {
                EventLog.CreateEventSource("SAM", "Application");
            }
            EventLog.WriteEntry("SAM", "Error:  Problem while creating user.xml file",
                 EventLogEntryType.Information);

            throw;
        }
    }

    public bool login(string _userName, string _hash)
    {
        try
        {
            if (File.Exists(userFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(userFilePath); // loading file
                XmlNodeList xnList = doc.GetElementsByTagName("users");

                foreach (XmlNode node in xnList)
                {
                    if (node.InnerText == _userName)
                    {
                        md5 encript = new md5(_hash.Trim());
                        encript.Encript();
                        this.hash = encript.hash;
                        if (node.ChildNodes[0].Attributes["password"].Value == encript.hash)
                        {
                            return true;
                        }
                    }
                }
                

                return false;

            }
            else
            {
                throw new Exception("ERROR! \n The file user.xml was not found!");
            }
        }
        catch (Exception ex )
        {
            if (!EventLog.SourceExists("SAM"))
            {
                EventLog.CreateEventSource("SAM", "Application");
            }
            EventLog.WriteEntry("SAM", "ERROR: Exception: " + ex.Message,
                 EventLogEntryType.Information);

            throw;
        }
    }

    //public bool CheckUser(string _userName, string _hash)
    //{
    //    DataTable dt;
    //    //string conection = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Visual Studio\Projects\Premier\PremierReports_v_1_0\App_Data\dbPremierReports.mdf;Integrated Security=True";


    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = con.CreateCommand())
    //        {
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = "select Password from Users where username = @uname";
    //            cmd.Parameters.Add(new SqlParameter("@uname", _userName));

    //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //            {
    //                dt = new DataTable();
    //                da.Fill(dt);
    //                if (dt.Rows.Count < 1) return false;
    //            }
    //        }
    //    }

    //    md5 encript = new md5(_hash.Trim());
    //    encript.Encript();

    //    if (encript.hash == dt.Rows[0]["password"].ToString().Trim())
    //    {
    //        userName = _userName;
    //        hash = _hash;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}

    public bool ChangePassword(string _userName, string _newUserName, string _oldHash, string _newHash)
    {
        try
        {
            if (File.Exists(userFilePath))
            {

                if (!login(_userName, _oldHash)) return false;
                else
                {
                    //Open the file
                    // Update name
                    File.WriteAllText(userFilePath, Regex.Replace(File.ReadAllText(userFilePath), _userName, _newUserName));

                    // Update password
                    File.WriteAllText(userFilePath, Regex.Replace(File.ReadAllText(userFilePath), hash, _newHash));


                    return true;

                    //XmlDocument doc = new XmlDocument();
                    //doc.Load(userFilePath); // loading file
                    //XmlNodeList xnList = doc.GetElementsByTagName("users");
                    //if (xnList.Count > 0)
                    //{

                       


                    //    //foreach (XmlNode node in xnList)
                    //    //{
                    //    //    if (node.InnerText == _userName)
                    //    //    {
                                
                    //    //        node.InnerText = _newUserName;
                    //    //        //node.ChildNodes[0].Attributes["password"].Value = _newHash;
                    //    //        XmlAttribute attribute = doc.CreateAttribute("password");
                    //    //        attribute.Value = _newHash;
                    //    //        node.Attributes.Append(attribute);

                    //    //        doc.Save(userFilePath);
                    //    //        return true;
                    //    //    }
                            
                    //    //}
                    //   // return false;
                    //}
                    //else throw new Exception("The user.xml was not found!");
                }
            }
            else throw new Exception("The user.xml was not found!");
        }
        catch (Exception ex)
        {
            
            if (!EventLog.SourceExists("SAM"))
            {
                EventLog.CreateEventSource("SAM", "Application");
            }
            EventLog.WriteEntry("SAM", "ERROR: Exception: " + ex.Message,
                 EventLogEntryType.Information);

            throw;
        }

        
    }

}