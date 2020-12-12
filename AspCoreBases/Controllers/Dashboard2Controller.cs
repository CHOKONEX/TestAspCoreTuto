using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TestAspCoreTuto.Controllers
{
    public interface IConfigurationReader
    {
        string ReadDashboardHeaderSettings();
    }

    public class ConfigurationReader : IConfigurationReader
    {
        private DashboardHeaderConfiguration dashboardHeaderConfig;

        ////exception in the developer exception page, which clearly says " A scoped service can not be consumed from a singleton service"
        //public ConfigurationReader(IOptionsSnapshot<DashboardHeaderConfiguration> optionsSnapshot)
        //{
        //    dashboardHeaderConfig = optionsSnapshot.Value;
        //}

        public ConfigurationReader(IOptionsMonitor<DashboardHeaderConfiguration> optionsMonitor)
        {
            this.dashboardHeaderConfig = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(config =>
            {
                this.dashboardHeaderConfig = config;
            });
        }

        public string ReadDashboardHeaderSettings()
        {
            return JsonConvert.SerializeObject(dashboardHeaderConfig);
        }
    }

    public class DashboardHeaderConfiguration
    {
        public bool IsSearchBoxEnabled { get; set; }
        public string BannerTitle { get; set; }
        public bool IsBannerSliderEnabled { get; set; }
    }

    public class Dashboard2Controller : Controller
    {
        private readonly DashboardHeaderConfiguration dashboardHeaderConfig;

        //public Dashboard2Controller(IOptions<DashboardHeaderConfiguration> options)
        //{
        //    dashboardHeaderConfig = options.Value;
        //}

        public Dashboard2Controller(IOptionsSnapshot<DashboardHeaderConfiguration> optionsSnapshot)
        {
            dashboardHeaderConfig = optionsSnapshot.Value;
        }

        public IActionResult Index()
        {
            return Content(JsonConvert.SerializeObject(dashboardHeaderConfig));
        }
    }

    //[ApiController]
    //[Route("test/dashboard3")]
    //[AllowAnonymous]
    //public class Dashboard3Controller : Controller
    //{
    //    private readonly IConfigurationReader configurationReader;

    //    public Dashboard3Controller(IConfigurationReader configurationReader)
    //    {
    //        this.configurationReader = configurationReader;
    //    }

    //    public IActionResult Index()
    //    {
    //        return Content(configurationReader?.ReadDashboardHeaderSettings());
    //    }
    //}
}
