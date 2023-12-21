using System.Collections.Generic;
using System.Threading.Tasks;
using WFDM.Models;

namespace WFDM.IServices;

public interface INgoServices
{
    List<Ngo> Gets();
    List<Ngo> Get(int id);
    Task<bool> Create(Ngo entity);//Insert API 
    Task<bool> Update(int id, Ngo entity);//Update API
    Task<bool> Delete(int id); //Delete API
}
