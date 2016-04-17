using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LokalReporter.Common;

namespace LokalReporter.Client.Dummy.Settings
{
    internal class CollectionAsyncSetting<TItem> : SettingsAsyncSetting<List<TItem>>,
        ICollectionAsyncSetting<TItem>
    {

        public CollectionAsyncSetting(string key, List<TItem> defaultValue) : base(key, defaultValue)
        {
        }


        public async Task AddItemAndSave(TItem item)
        {
            IReadOnlyCollection<TItem> filters = await this.GetValueAsync();
            var current = filters as List<TItem> ?? filters.ToList();
            current.Add(item);
            await this.SetValueAsync(current);
        }

        public async Task RemoveAndSave(TItem item)
        {
            IReadOnlyCollection<TItem> filters = await this.GetValueAsync();
            var current = filters as List<TItem> ?? filters.ToList();
            current.Remove(item);
            await this.SetValueAsync(current);
        }

        public async Task<IReadOnlyCollection<TItem>> GetValueAsync()
        {
            return await base.GetValueAsync();
        }

        public async Task SetValueAsync(IReadOnlyCollection<TItem> value)
        {
            await base.SetValueAsync(value as List<TItem> ?? value.ToList());
        }

    }
}