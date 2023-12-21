using WFDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.IServices;

public interface IDonorServices
{
    List<Donor> Gets();
    List<Donor> Get(int id);
    Task<bool> Create(Donor entity);//Insert API 
    Task<bool> Update(int id, Donor entity);//Update API
    Task<bool> Delete(int id); //Delete API
}
