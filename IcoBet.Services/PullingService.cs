namespace IcoBet.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    public class PullingService : IPullingService
    {
        private readonly IConfiguration configuration;

        public PullingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> Pull()
        {
            var pullingUrl = this.configuration.GetSection("PullingUrl").Value;
            string result = null;
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pullingUrl);
                request.Method = "GET";
                response = await request.GetResponseAsync();
                reader = new StreamReader(response.GetResponseStream());
                result = await reader.ReadToEndAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }
        }
    }
}
