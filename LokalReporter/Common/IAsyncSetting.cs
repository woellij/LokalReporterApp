using System.Threading.Tasks;

namespace LokalReporter.Common
{
    public interface IAsyncSetting<T>
    {

        Task<T> GetValueAsync();

        Task SetValueAsync(T value);

    }
}