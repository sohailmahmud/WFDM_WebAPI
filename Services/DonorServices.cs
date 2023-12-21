using Dapper;
using WFDM.IServices;
using WFDM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFDM.Common;

namespace WFDM.Services;

public class DonorServices : IDonorServices
{
    List<Donor> donors = [];

    public async Task<bool> Create(Donor entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"INSERT INTO Donor(Name, ProfilePic, PhoneNo, Email, Address, Password, Status) VALUES(@Name, @ProfilePic, @PhoneNo, @Email, @Address, @Password, @Status)";

        try
        {
            await oCon.ExecuteAsync(query, new
            {
                Id = Guid.NewGuid().ToString(),
                entity.Name,
                entity.ProfilePic,
                entity.PhoneNo,
                entity.Email,
                entity.Address,
                entity.Password,
                entity.Status,

            }, commandType: CommandType.Text);
        }
        finally
        {
            oCon.Close();
            oCon.Dispose();
        }

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        const string query = @"Delete Donor where DonorID=@id";

        try
        {
            await con.ExecuteAsync(query, new { id }, commandType: CommandType.Text);
        }
        finally
        {
            con.Close();
            con.Dispose();
        }

        return true;
    }

    public List<Donor> Get(int id)
    {
        donors = [];

        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var oDonor = con.Query<Donor>(@"SELECT * FROM Donor Where PhoneNo =" + id).ToList();
        if (oDonor.Count != 0)
        {
            donors = oDonor;
        }
        con.Close();
        con.Dispose();
        return donors;
    }

    public List<Donor> Gets()
    {
        donors = [];
        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var oDonor = con.Query<Donor>(@"SELECT * FROM Donor").ToList();

        if (oDonor.Count != 0)
        {
            donors = oDonor;
        }
        con.Close();
        con.Dispose();
        return donors;
    }

    public async Task<bool> Update(int id, Donor entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"Update Donor SET Name=@Name, ProfilePic=@ProfilePic, PhoneNo=@PhoneNo, Email=@Email, Address=@Address, Password=@Password, Status=@Status WHERE DonorID=@id";

        try
        {
            await oCon.ExecuteAsync(query, new
            {
                entity.Name,
                entity.ProfilePic,
                entity.PhoneNo,
                entity.Email,
                entity.Address,
                entity.Password,
                entity.Status,
                id

            }, commandType: CommandType.Text);
        }
        finally
        {
            oCon.Close();
            oCon.Dispose();
        }

        return true;
    }
}
