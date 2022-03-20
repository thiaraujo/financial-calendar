using Domain.Entities.Shared;
using MongoDB.Bson.Serialization;

namespace Data.Infra.Persistence
{
    public class BaseEntityClassMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(p => p.Id);
                cm.SetIsRootClass(true);
                cm.UnmapMember(x => x.Valid);
                cm.UnmapMember(x => x.ValidationResult);
            });
        }
    }
}
