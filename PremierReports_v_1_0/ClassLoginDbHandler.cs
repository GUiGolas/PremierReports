using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for ClassLoginDbHandler
/// </summary>
public class ClassLoginDbHandler
{
    public string userName { get; set; }
    public string hash { get; set; }
    public bool root { get; set; }

    
    public ClassLoginDbHandler()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public bool CheckUser(string _userName, string _hash)
    {
        DataTable dt;
        string conection = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=\\servidor\particulares\GUILHERME\Visual Studio 2013\WebSites\InterfaceTest2\App_Data\ReportsDB.mdf;Integrated Security=True";

        using (SqlConnection con = new SqlConnection(conection))
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

    public bool ChangePassword(string _userName, string _oldHash, string _newHash)
    {
        if (!CheckUser(_userName, _oldHash)) return false;
        else
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["userDbConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Table SET Password = @password where username = @uname";
                    cmd.Parameters.Add(new SqlParameter("@uname", _userName));
                    cmd.Parameters.Add(new SqlParameter("@password", _newHash));

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