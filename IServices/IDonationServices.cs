using WFDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.IServices;

public interface IDonationServices
{
    List<Donation> Gets();
    List<Donation> Get(int id);
    Task<bool> Create(Donation entity);//Insert API 
    Task<bool> Update(int id, Donation entity);//Update API
    Task<bool> Delete(int id); //Delete API
}
