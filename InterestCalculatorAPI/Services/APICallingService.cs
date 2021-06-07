using InterestCalculatorAPI.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;

namespace InterestCalculatorAPI.Services
{
    public class APICallingService : IAPICallingService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> retrieveInterestRateData()
        {
            try { 
                string response = await client.GetStringAsync("http://localhost:9000/taxaJuros");

                return response;
            }
            catch
            {
                return null;
            }   
        }
    }
}
