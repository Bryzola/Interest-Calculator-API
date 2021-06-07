using InterestCalculatorAPI.Interfaces;
using InterestCalculatorAPI.Models;
using System;
using System.Threading.Tasks;

namespace InterestCalculatorAPI.Services
{
    public class CalculationService : ICalculationService
    {
        public async Task<string> calculateInterest(InterestFieldsDTO interestFields)
        {
            var task = await Task.Run(() => {
                //Calls calculation function
                decimal calculatedResult = interestCalculation(interestFields.valorInicial, interestFields.meses, interestFields.jurosFixo);

                //Returns truncated value
                return truncateValue(calculatedResult).ToString("f2");
            });

            return task;
        }

        public decimal interestCalculation(decimal initialValue, int months, double fixedRate) 
        {
            decimal result;
            
            //Executes fixed rate and months calculation
            double rateValue = Math.Pow(1 + fixedRate, months);

            //Executes initial value and final rate calculation
            result = initialValue * (decimal)rateValue;

            return result;
        }

        public decimal truncateValue(decimal value)
        {
            //Truncates value
            decimal truncatedValue = Math.Truncate(value * 100) / 100;

            return truncatedValue;
        }
    }
}
