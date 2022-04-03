namespace IcoBet.Web.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using IcoBet.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class PullingController : ControllerBase
    {
        private readonly IPullingService pullingService;
        private readonly IParsingService parsingService;
        private readonly ISavingService savingService;

        public PullingController(
            IPullingService pullingService,
            IParsingService parsingService,
            ISavingService savingService)
        {
            this.pullingService = pullingService;
            this.parsingService = parsingService;
            this.savingService = savingService;
        }

        [HttpGet]
        public async Task Get()
        {
            try
            {
                await SaveDataEveryOneMinute(TimeSpan.FromMinutes(1), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task SaveDataEveryOneMinute(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                string result = await this.pullingService.Pull();

                var sports = this.parsingService.ParseXml(result);

                await this.savingService.SaveToDb(sports);

                await Task.Delay(interval, cancellationToken);
            }
        }
    }
}
