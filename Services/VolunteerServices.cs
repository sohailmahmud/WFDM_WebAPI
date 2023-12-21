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

public class VolunteerServices : IVolunteerServices
{

    List<Volunteer> volunteers = [];

    public async Task<bool> Create(Volunteer entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"INSERT INTO Volunteer(VolunteerID, Name, ProfilePic, PhoneNo, Email, Address, ServiceArea, Password) VALUES(@VolunteerID, @Name, @ProfilePic, @PhoneNo, @Email, @Address, @ServiceArea, @Password)";

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
                entity.ServiceArea,
                entity.Password

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
        const string query = @"Delete Volunteer where VolunteerID=@id";

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

    public List<Volunteer> Get(int id)
    {
        volunteers = [];

        using SqlConnection con = new (Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var ovolunteers = con.Query<Volunteer>(@"SELECT * FROM Volunteer Where PhoneNo =" + id).ToList();
        if (ovolunteers.Count != 0)
        {
            volunteers = ovolunteers;
        }
        con.Close();
        con.Dispose();
        return volunteers;
    }

    public List<Volunteer> Gets()
    {
        volunteers = [];
        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var ovolunteers = con.Query<Volunteer>(@"SELECT * FROM Volunteer").ToList();

        if (ovolunteers.Count != 0)
        {
            volunteers = ovolunteers;
        }
        con.Close();
        con.Dispose();
        return volunteers;
    }

    public async Task<bool> Update(int id, Volunteer entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"Update Volunteer SET Name=@Name, ProfilePic=@ProfilePic, PhoneNo=@PhoneNo, Email=@Email, Address=@Address, ServiceArea=@ServiceArea, Password=@Password WHERE VolunteerID=@id";

        try
        {
            await oCon.ExecuteAsync(query, new
            {
                entity.Name,
                entity.ProfilePic,
                entity.PhoneNo,
                entity.Email,
                entity.Address,
                entity.ServiceArea,
                entity.Password,
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
