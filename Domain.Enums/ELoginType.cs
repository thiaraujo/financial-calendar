namespace Domain.Enums
{
    // Don't use value 0, because mongodb will save empty
    public enum ELoginType
    {
        SelfAccount = 1,
        Google = 2,
        Facebook = 3,
        Apple = 4,
        Github = 5
    }
}