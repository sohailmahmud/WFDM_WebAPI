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

public class NgoServices : INgoServices
{
    List<Ngo> ngos = [];
    public async Task<bool> Create(Ngo entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"INSERT INTO Ngo(NgoID, Name, ProfilePic, PhoneNo, Email, Address, Password, Status) VALUES(@NgoID, @Name, @ProfilePic, @PhoneNo, @Email, @Address, @Password, @Status)";

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
                entity.Status

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
        const string query = @"Delete Ngo where NgoID=@id";

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

    public List<Ngo> Get(int id)
    {
        ngos = [];

        using SqlConnection con = new (Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var ongos = con.Query<Ngo>(@"SELECT * FROM Ngo Where PhoneNo =" + id).ToList();
        if (ongos.Count != 0)
        {
            ngos = ongos;
        }
        con.Close();
        con.Dispose();
        return ngos;
    }

    public List<Ngo> Gets()
    {
        ngos = [];
        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var ongos = con.Query<Ngo>(@"SELECT * FROM Ngo").ToList();

        if (ongos.Count != 0)
        {
            ngos = ongos;
        }
        con.Close();
        con.Dispose();
        return ngos;
    }

    public async Task<bool> Update(int id, Ngo entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"Update Ngo SET Name=@Name, ProfilePic=@ProfilePic, PhoneNo=@PhoneNo, Email=@Email, Address=@Address, Password=@Password, Status=@Status WHERE NgoID=@id";

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
