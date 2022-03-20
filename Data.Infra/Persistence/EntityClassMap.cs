using MongoDB.Bson.Serialization;

namespace Data.Infra.Persistence
{
    public class EntityClassMap<T> where T : class
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<T>();
        }
    }
}
