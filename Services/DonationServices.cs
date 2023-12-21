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

public class DonationServices : IDonationServices
{
    List<Donation> items = [];

    public async Task<bool> Create(Donation entity)
    {
        using IDbConnection oCon = new SqlConnection(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"INSERT INTO Donation(DonationID, DonorID, Name, Description, Unit, Quantity, Area, PickupAddress, DeliveryAddress, Status) VALUES(@DonationID, @DonorID, @Name, @Description, @Unit, @Quantity, @Area, @PickupAddress, @DeliveryAddress, @Status)";

        try
        {
            await oCon.ExecuteAsync(query, new
            {
                Id = Guid.NewGuid().ToString(),
                entity.DonationID,
                entity.DonorID,
                entity.Name,
                entity.Description,
                entity.Unit,
                entity.Quantity,
                entity.Area,
                entity.PickupAddress,
                entity.DeliveryAddress,
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
        const string query = @"Delete Item where DonationID=@id";

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

    public List<Donation> Get(int id)
    {
        items = [];

        using IDbConnection con = new SqlConnection(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var oItems = con.Query<Donation>(@"SELECT * FROM Donation Where DonationID ='" + id + "'").ToList();
        if (oItems.Count != 0)
        {
            items = oItems;
        }
        con.Close();
        con.Dispose();
        return items;
    }

    public List<Donation> Gets()
    {
        items = [];
        using SqlConnection con = new(Global.ConnectionsString);
        if (con.State == ConnectionState.Closed) con.Open();
        var oItems = con.Query<Donation>(@"SELECT * FROM Donation").ToList();

        if (oItems.Count != 0)
        {
            items = oItems;
        }
        con.Close();
        con.Dispose();
        return items;
    }

    public async Task<bool> Update(int id, Donation entity)
    {
        using SqlConnection oCon = new(Global.ConnectionsString);
        if (oCon.State == ConnectionState.Closed) oCon.Open();
        const string query = @"Update Donation SET DonorID=@DonorID, Name=@Name, Description=@Description, Unit=@Unit, Quantity=@Quantity, Area=@Area, PickupAddress=@PickupAddress, DeliveryAddress=@DeliveryAddress WHERE DonationID=@id";

        try
        {
            await oCon.ExecuteAsync(query, new
            {
                entity.DonorID,
                entity.Name,
                entity.Description,
                entity.Unit,
                entity.Quantity,
                entity.Area,
                entity.PickupAddress,
                entity.DeliveryAddress,
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
