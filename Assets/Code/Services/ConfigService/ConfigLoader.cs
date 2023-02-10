using System.Threading.Tasks;

namespace Code.Services.ConfigService
{
    public class ConfigLoader : IConfigLoader
    {
        public Task<Config> LoadConfigAsync() => Task.FromResult(new Config());
    }
}