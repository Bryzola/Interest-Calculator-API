using System.Threading.Tasks;

namespace InterestCalculatorAPI.Interfaces
{
    public interface IAPICallingService
    {
        public Task<string> retrieveInterestRateData();
    }
}
