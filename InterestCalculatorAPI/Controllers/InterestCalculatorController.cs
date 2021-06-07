using InterestCalculatorAPI.Interfaces;
using InterestCalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace InterestCalculatorAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class InterestCalculatorController : ControllerBase
    {

        private readonly IAPICallingService _apiCallingService;
        private readonly ICalculationService _calculationService;

        public InterestCalculatorController(
            IAPICallingService apiCallingService,
            ICalculationService calculationService
        )
        {
            _apiCallingService = apiCallingService;
            _calculationService = calculationService;
        }

        [HttpPost("calculaJuros")]
        public async Task<IActionResult> PostInterestCalculation(decimal valorInicial, int meses)
        {
            try
            {
                InterestRateDTO interestRate;

                //Retrieves current interest rate from service
                string interestRateData = await _apiCallingService.retrieveInterestRateData();
                
                try { 
                    interestRate = JsonConvert.DeserializeObject<InterestRateDTO>(interestRateData);
                } catch
                {
                    return BadRequest("Please start the Interest Rate API on port 9000.");
                }

                //Define Interest Fields for calculation
                InterestFieldsDTO interestFields = new InterestFieldsDTO
                {
                    valorInicial = valorInicial,
                    meses = meses,
                    jurosFixo = interestRate.CurrentRate
                };

                //Calculates interest
                string finalResult = await _calculationService.calculateInterest(interestFields);
                
                //Returns calculated result
                return Ok(finalResult);
            }
            catch (Exception ex)
            {
                //Returns Internal Server Error with exception
                return StatusCode(500, ex);
            }
        }

        [HttpGet("showMeTheCode")]
        public IActionResult GetCodeURL()
        {
            return Ok("API 1 (Retorna Taxa de Juros): https://github.com/Bryzola/Interest-Rate-API - API 2 (Calcula Juros): https://github.com/Bryzola/Interest-Calculator-API");
        }
    }
}
