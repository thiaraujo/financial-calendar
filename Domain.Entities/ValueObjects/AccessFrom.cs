using Domain.Entities.Shared;

namespace Domain.Entities.ValueObjects;

public class AccessFrom : BaseValueObject
{
    public string Device { get; private set; } // Like, Chrome, Mozilla
    public string Ip { get; private set; }
    public string City { get; private set; } // City

    public AccessFrom(string device, string ip, string city)
    {
        Device = device;
        Ip = ip;
        City = city;
    }
}