using System.Collections.Generic;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy {

    public static class Entities {
        private static readonly IDictionary<string, IdEntity> entities = new Dictionary<string, IdEntity>();

        public static T From<T>(string id, string name) where T : IdEntity, new()
        {
            lock (entities) {
                IdEntity entity;
                if (entities.TryGetValue(id, out entity)) {
                    if (entity is T) {
                        return (T) entity;
                    }
                }

                entity = new T {Id = id, Name = name};

                entities[id] = entity;

                return (T) entity;
            }
        }
    }

}