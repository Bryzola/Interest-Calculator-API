using Xunit;
using Moq;
using InterestCalculatorAPI.Controllers;
using InterestCalculatorAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InterestCalculatorAPI.Services;

namespace InterestCalculatorAPI.xUnitTest
{
    public class InterestCalculatorControllerUnitTest
    {
        private Mock<IAPICallingService> _mockAPICAllingService = new Mock<IAPICallingService>();
        private CalculationService _calculationService;

        public InterestCalculatorControllerUnitTest()
        {
            _mockAPICAllingService.Setup(x => x.retrieveInterestRateData()).ReturnsAsync("{\"CurrentRate\": 0.01}");
            _calculationService = new CalculationService();
        }

        [Fact]
        public async void PostInterestCalculation_1()
        {
            //Arrange
            decimal initialValue = 100;
            int meses = 5;
            InterestCalculatorController interestCalculatorController = new InterestCalculatorController(_mockAPICAllingService.Object, _calculationService);

            //Act
            ObjectResult result = await interestCalculatorController.PostInterestCalculation(initialValue, meses) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True("105.10".Equals(result.Value));
        }

        [Fact]
        public async void PostInterestCalculation_2()
        {
            //Arrange
            decimal initialValue = 200;
            int meses = 12;
            InterestCalculatorController interestCalculatorController = new InterestCalculatorController(_mockAPICAllingService.Object, _calculationService);

            //Act
            ObjectResult result = await interestCalculatorController.PostInterestCalculation(initialValue, meses) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True("225.36".Equals(result.Value));
        }

        [Fact]
        public async void PostInterestCalculation_3()
        {
            //Arrange
            decimal initialValue = 0;
            int meses = 0;
            InterestCalculatorController interestCalculatorController = new InterestCalculatorController(_mockAPICAllingService.Object, _calculationService);

            //Act
            ObjectResult result = await interestCalculatorController.PostInterestCalculation(initialValue, meses) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True("0.00".Equals(result.Value));
        }

        [Fact]
        public void GetCodeURL()
        {
            //Arrange
            InterestCalculatorController interestCalculatorController = new InterestCalculatorController(_mockAPICAllingService.Object, _calculationService);

            //Act
            ObjectResult result = interestCalculatorController.GetCodeURL() as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True("API 1 (Retorna Taxa de Juros): https://github.com/Bryzola/Interest-Rate-API - API 2 (Calcula Juros): https://github.com/Bryzola/Interest-Calculator-API".Equals(result.Value));
        }
    }
}
