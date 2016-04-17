using System.Collections.Generic;
using System.Threading.Tasks;

namespace LokalReporter.Common
{
    public interface ICollectionAsyncSetting<TItem> : IAsyncSetting<IReadOnlyCollection<TItem>>
    {

        Task AddItemAndSave(TItem item);

        Task RemoveAndSave(TItem item);

    }
}