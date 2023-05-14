/*using System;
using System.Collections;
using System.Collections.Generics;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Azure;
using Microsoft.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["movieConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnklogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtusername.Text.Trim()) && !string.IsNullOrEmpty(txtpwd.Text.Trim()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from User_Login where Uname='" + txtusername.Text.ToLower() + "' and Upwd='" + txtpwd.Text + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["userSession"] = ds.Tables[0].Rows[0]["Uname"].ToString();
                    Session["userIDSession"] = ds.Tables[0].Rows[0]["Uid"].ToString();
                    Response.Redirect("index.aspx");
                }
                else
                {
                    lblerror.Text = "Invalid username/password";
                }
            }
            else
            {
                // lblerror.Text = "Invalid username/password";
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> usernamePassword = new Dictionary<string, string>();
        usernamePassword.Add("john", "qwerty");
        try
        {
            throw new Exception(txtusername.Text.Trim());
            if (usernamePassword.ContainsKey(txtusername.Text.Trim()) && usernamePassword[txtusername.Text.Trim()] == txtpwd.Text.Trim())    //!string.IsNullOrEmpty(txtusername.Text.Trim()) && !string.IsNullOrEmpty(txtpwd.Text.Trim()))
            {
                HttpCookie cookie = new HttpCookie("mscope");
                cookie.Value = Encrypt(txtusername.Text.Trim()); //MD5Hash(txtuname.Text);// GenerateNumber();
                                                                 //  Session["un"] = txtuname.Text;                
                Response.Cookies.Add(cookie);
                Response.Redirect("index.aspx");
            }
            else
            {
                lblerror.Text = "Invalid username/password";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(txtusername.Text.Trim());
        }
}

    public string Encrypt(string originalString)
    {
        byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
        if (String.IsNullOrEmpty(originalString))
        {
            throw new ArgumentNullException
                   ("The string which needs to be encrypted can not be null.");
        }
        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream,
            cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
        StreamWriter writer = new StreamWriter(cryptoStream);
        writer.Write(originalString);
        writer.Flush();
        cryptoStream.FlushFinalBlock();
        writer.Flush();
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
    }
}*/