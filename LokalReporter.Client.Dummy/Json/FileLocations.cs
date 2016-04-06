using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using LokalReporter.Client.Dummy.Data.Reading;
using LokalReporter.Client.Dummy.Locations;

namespace LokalReporter.Client.Dummy.Json
{
    internal class FileLocations : ILocations
    {

        private List<DistrictRelation> districtRelations;

        public async Task<List<DistrictRelation>> GetDistrictRelationsAsync()
        {
            return this.districtRelations ??
                   (this.districtRelations = await DataReader.ReadFromEmbeddedResourceAsync<List<DistrictRelation>>(this.GetType().GetTypeInfo().Assembly, "LokalReporter.Client.Dummy", "Data", "district_articles.json"));
        }

    }
}