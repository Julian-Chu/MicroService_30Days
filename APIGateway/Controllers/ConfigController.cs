using System.Collections.Generic;
using APIGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace APIGateway.Controllers
{
  [Produces("application/json")]
  [Route("api/Config")]
  public class ConfigController : Controller
  {
    private readonly IConfiguration configuration;
    private readonly IOptionsSnapshot<ConfigServerData> _configOptionsSnapshot;
    private readonly IOptions<ConfigServerClientSettings> _configServerClientSettings;

    public ConfigController(IConfiguration configuration, IOptionsSnapshot<ConfigServerData> configOptionsSnapshot, IOptions<ConfigServerClientSettings> configServerClientSettings)
    {
      if (configOptionsSnapshot != null)
        this.configuration = configuration;

      if (configServerClientSettings != null)
        this._configServerClientSettings = configServerClientSettings;
        
      this._configOptionsSnapshot = configOptionsSnapshot;
    }


    // GET: api/Config
    [HttpGet]
    public IEnumerable<string> Get()
    {
      var optionsSnapshot = _configOptionsSnapshot.Value;
      var configServerClientSettings = _configServerClientSettings.Value;
      return new string[] { $"Foo:{optionsSnapshot.Foo}",
        $"Bar:{optionsSnapshot.Bar}",
        $"Env:{configServerClientSettings.Environment}",
        $"Name:{configServerClientSettings.Name}",
        $"Uri:{configServerClientSettings.Uri}",
        $"RawUri:{configServerClientSettings.RawUri}"

      };
    }
    
  }

}
