using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Orders.Infrastructure.MongoDb
{
    public static class MongoConfigurator
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }        
        }

        private static void RegisterConvetionn()
        {
            ConventionRegistry.Register("ItemsConvetions", new ItemsConvetions(), x => true);
        }

        private class ItemsConvetions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention(),
            };
        }
    }
}