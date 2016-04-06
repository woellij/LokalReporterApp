using System.Collections.Generic;
using System.Threading.Tasks;

using LokalReporter.Client.Dummy.Locations;

namespace LokalReporter.Client.Dummy.Json
{
    public interface ILocations
    {
        Task<List<DistrictRelation>> GetDistrictRelationsAsync();

    }
}