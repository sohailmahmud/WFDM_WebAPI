using System;

namespace WFDM.IServices;

public interface IUnitOfWorks
{
    IDonorServices Donor { get; }
    INgoServices Ngo { get; }
    IVolunteerServices Volunteer { get; }
    IDonationServices Donation { get; }
}
