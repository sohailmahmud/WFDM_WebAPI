using System.Collections.Generic;
using System.Threading.Tasks;
using WFDM.Models;

namespace WFDM.IServices;

public interface IVolunteerServices
{
    List<Volunteer> Gets();
    List<Volunteer> Get(int id);
    Task<bool> Create(Volunteer entity);//Insert API 
    Task<bool> Update(int id, Volunteer entity);//Update API
    Task<bool> Delete(int id); //Delete API
}
