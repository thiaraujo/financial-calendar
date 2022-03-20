using Domain.Entities.Shared;
using Domain.Enums;

namespace Domain.Entities.ValueObjects
{
    public class Email : BaseValueObject
    {
        public string Address { get; private set; }
        public ELoginType LoginType { get; private set; }

        public Email(string address, ELoginType loginType)
        {
            Address = address;
            LoginType = loginType;
        }
    }
}
