using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.Models;

public class Donation
{
    public int DonationID { get; set; }
    public int DonorID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public string Quantity { get; set; }
    public string Area { get; set; }
    public string PickupAddress { get; set; }
    public string DeliveryAddress { get; set; }
    public bool Status { get; set; }
}
