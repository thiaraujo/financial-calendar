using Domain.Entities.Shared;

namespace Domain.Entities.ValueObjects
{
    public class Name : BaseValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set;}

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
