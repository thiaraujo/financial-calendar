using Domain.Entities.Shared;

namespace Domain.Entities.ValueObjects;

public class Identification : BaseValueObject
{
    public string Path { get; private set; }
    public string Description { get; private set; }

    public Identification(string path, string description)
    {
        Path = path;
        Description = description;
    }
}