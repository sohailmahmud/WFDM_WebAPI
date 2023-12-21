using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
//using ProbashiPOS_WebAPI.Helpers;
//using ProbashiPOS_WebAPI.Models;
using WFDM.Models;

//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Lucene.Net.Support;

namespace RunQuery;

public class SQLQuery
{
    
    public static string Empty2Zero(string stringValue)
    {
        if (stringValue == "" || stringValue == "NULL")
        {
            stringValue = "0";
        }
        return stringValue;
    }

    public static DateTime GetBDTime()
    {
        DateTime serverTime = DateTime.Now;
        DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");
        return _localTime;
    }

    public  readonly IConfiguration _configuration;
    public readonly AppSettings _appSettings;
    public static string ReturnString(string query, string connectionString)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(query, new SqlConnection(connectionString));
            cmd.Connection.Open();
            string result = Convert.ToString(cmd.ExecuteScalar());
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(query + ".<br/> ERROR: " + ex.ToString());
            return query + ".<br/><br/> ERROR: " + ex.ToString();
        }
    }

    public static string ExecNonQry(string query, string connectionString)
    {
        try
        {
            SqlCommand cmd7 = new SqlCommand(query, new SqlConnection(connectionString));
            cmd7.Connection.Open();
            int q= cmd7.ExecuteNonQuery();
            cmd7.Connection.Close();
            cmd7.Connection.Dispose();
            return SQLQuery.MakeXDigit(q.ToString(),3)+" rows effected by last query ";
        }catch(Exception ex)
        {
            return ex.ToString();
        }
    }

    //Sql query to dataset
    public static DataSet ReturnDataSet(String Query, string connStr)
    {
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlCommand objCommand = new SqlCommand(Query, conn);
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            da.Fill(ds);
            da.Dispose();
        }
        return ds;
    }

    public static DataTable ReturnDataTable(String Query, string connStr)
    {
        SqlCommand cmd2y = new SqlCommand(Query, new SqlConnection(connStr));
        cmd2y.Connection.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd2y);
        DataSet ds = new DataSet("Board");
        da.Fill(ds, "Board");
        cmd2y.Connection.Close();

        DataTable citydt = ds.Tables["Board"];
        return citydt;
    }

    public static string MakeXDigit(string maxValue, int x)
    {
        while (maxValue.Length < x)
        {
            maxValue += "0" + maxValue;
        }
        return maxValue;
    }
   
}
