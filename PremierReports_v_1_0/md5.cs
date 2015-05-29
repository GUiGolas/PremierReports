using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for md5
/// </summary>
public class md5
{
    private string input = "";
    public string hash { get; set; }

    public md5(string value)
    {
        input = value;


    }

    public void Encript()
    {
        using (MD5 md5Hash = MD5.Create())
        {
            this.hash = GetMd5Hash(md5Hash, input);

        }
    }

    static string GetMd5Hash(MD5 md5Hash, string value)
    {
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }



    // Verify a hash against a string.
    static public bool VerifyMd5Hash(string value, string hash)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            string hashOfInput = GetMd5Hash(md5Hash, value);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        // Hash the input.

    }

}