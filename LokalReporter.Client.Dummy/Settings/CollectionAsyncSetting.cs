using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LokalReporter.Common;

namespace LokalReporter.Client.Dummy.Settings
{
    internal class CollectionAsyncSetting<TItem> : SettingsAsyncSetting<IReadOnlyCollection<TItem>>,
        ICollectionAsyncSetting<TItem>
    {

        public CollectionAsyncSetting(string key, IReadOnlyCollection<TItem> defaultValue) : base(key, defaultValue)
        {
        }


        public async Task AddItemAndSave(TItem item)
        {
            IReadOnlyCollection<TItem> filters = await this.GetValueAsync();
            var current = filters as List<TItem> ?? filters.ToList();
            current.Add(item);
            await this.SetValueAsync(current);
        }

    }
}