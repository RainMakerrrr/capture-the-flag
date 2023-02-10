using System.Threading.Tasks;

namespace Code.Services.ConfigService
{
    public interface IConfigLoader
    {
        Task<Config> LoadConfigAsync();
    }
}