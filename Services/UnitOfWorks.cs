using WFDM.IServices;

namespace WFDM.Services;

public class UnitOfWorks : IUnitOfWorks
{
    public UnitOfWorks()
    {
        Donor = new DonorServices();
        Ngo = new NgoServices();
        Volunteer = new VolunteerServices();
        Donation = new DonationServices();
    }
    public IDonorServices Donor { get; private set; }
    public INgoServices Ngo { get; private set; }
    public IVolunteerServices Volunteer { get; private set; }
    public IDonationServices Donation { get; private set; }
}
