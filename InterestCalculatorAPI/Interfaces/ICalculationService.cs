using InterestCalculatorAPI.Models;
using System.Threading.Tasks;

namespace InterestCalculatorAPI.Interfaces
{
    public interface ICalculationService
    {
        public Task<string> calculateInterest(InterestFieldsDTO interestFields);
    }
}
