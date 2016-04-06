using System.Collections.Generic;
using System.Linq;
using LokalReporter.Responses;

namespace LokalReporter.Client.Dummy {


    public static class Entities {
        private static readonly IDictionary<string, IdEntity> entities = new Dictionary<string, IdEntity>();

        private static readonly IDictionary<string, string> Resorts = new Dictionary<string, string> {
            {"wetter", "Wetter"},
            {"sport", "Sport"},
            {"verkehr", "Verkehr"},
            {"polizei", "Polizei"},
            {"ratgeber", "Ratgeber"},
            {"kultur", "Kultur"},
            {"religion", "Religion"}
        };

        private static readonly IDictionary<string, string> districts = new Dictionary<string, string> {
            {"unterfranken", "Unterfranken"},
            {"oberfranken", "Oberfranken"},
            {"mittelfranken", "Mittelfranken"},
            {"oberpfalz", "Oberpfalz"},
            {"schwaben", "Schwaben"},
            {"oberbayern", "Oberbayern"},
            {"niederbayern", "Niederbayern"},
        };

        public static List<District> Districts => districts.Select(pair => From<District>(pair.Key, pair.Value)).ToList();

        public static List<Category> Categories => Resorts.Select(pair => From<Category>(pair.Key, pair.Value)).ToList();

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