using Domain.Entities.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Data.Infra.Persistence;

public static class MongoDbPersistence
{
    public static void Configure()
    {
        BaseEntityClassMap.Configure();
        EntityClassMap<Account>.Configure();
        EntityClassMap<AccountLoginLog>.Configure();
        EntityClassMap<Organization>.Configure();

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

        // Conventions
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfDefaultConvention(true)
        };
        ConventionRegistry.Register("FinancialCalendar Solution", pack, t => true);
    }
}