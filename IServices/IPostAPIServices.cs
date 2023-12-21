using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using Lucene.Net.Support;

namespace WFDM.IServices;

public interface IPostAPIServices
{
    //AuthenticateResponse Authenticate(AuthenticateRequest model);
    string InsertAll(string tableName);
    string QueryExecute(string query);

    //List<string> GetById(string id);
    string GetAll(string query);

    DataTable GetQueryAll(string query);
}
public class PostAPIServices(IOptions<AppSettings> appSettings, IConfiguration configuration) : IPostAPIServices
{
    private readonly List<string> _students = [];
    private readonly IConfiguration _configuration = configuration;
    private readonly AppSettings _appSettings = appSettings.Value;

    public string InsertAll(string tableName)
    {

        string query = "INSERT INTO" + tableName + " ";
        var returnData = ReturnDataTable(query);

        List<JObject> dataList = [];
        for (int i = 0; i < returnData.Rows.Count; i++)
        {
            JObject eachRowObj = [];

            for (int j = 0; j < returnData.Columns.Count; j++)
            {
                string key = Convert.ToString(returnData.Columns[j]);
                string value = Convert.ToString(returnData.Rows[i].ItemArray[j]);

                eachRowObj.Add(key, value);

            }
            dataList.Add(eachRowObj);

        }
        string JSONresult = JsonConvert.SerializeObject(returnData);
        return JSONresult;

        //return _students;
    }
    public DataTable ReturnDataTable(string query)
    {
        SqlCommand com = new(query, new SqlConnection(_configuration.GetConnectionString("XProHealth")));
        // SqlCommand com = new SqlCommand(query, new SqlConnection(_configuration.GetConnectionString("")));
        com.Connection.Open();
        SqlDataAdapter da = new(com);
        DataSet ds = new("Test");
        da.Fill(ds, "Test");
        com.Connection.Close();
        DataTable dtValue = ds.Tables["Test"];
        return dtValue;
    }

    public DataTable GetQueryAll(string query)
    {

        return ReturnDataTable(query);

    }
    public string QueryExecute(string query)
    {
        SqlCommand com = new(query, new SqlConnection(_configuration.GetConnectionString("XProHealth")));
        // SqlCommand com = new SqlCommand(query, new SqlConnection(_configuration.GetConnectionString("")));
        com.Connection.Open();
        try
        {
            var rowAffect = com.ExecuteNonQuery();
            if (rowAffect > 0)
            {
                return "Your Query Execute Successfully.";
            }
            else
            {
                return "Your Query Execute Unsuccessfully. Please check your query!";
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        //com.Connection.Close();
        //com.Connection.Dispose();

    }

    public string GetAll(string query)
    {

        string connectionString = _configuration.GetConnectionString("XProHealth");
        using DataTable returnData = ReturnDataTable(query);


        string jsoResult = JsonConvert.SerializeObject(returnData);

        return jsoResult;
    }
}
