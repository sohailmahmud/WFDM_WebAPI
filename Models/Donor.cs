using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.Models;

public class Donor
{
    public int DonorID { get; set; }
    public string Name { get; set; }
    public string ProfilePic { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}
