using System;
using Xunit;
using Number2word.Factories;
using Number2word.Interface;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitNumber2Word
{
    public class UnitTest1
    {
        private ITranslator _ITranslator;
       
        public UnitTest1()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITranslator, NumberToWordTranslatorFactory>();
            var serviceProvider = services.BuildServiceProvider();

            _ITranslator = serviceProvider.GetService<ITranslator>();


           // _ITranslator =(ITranslator) new Mock<ITranslator>();
           // _ITranslator = new NumberToWordTranslatorFactory();
        }
        [Fact]
        public void Test()
        {
            string expectedword = "One Hundred Twenty Three";

            Assert.Equal(expectedword, "One Hundred Twenty Three");
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("abcd", false)]        
        [InlineData("-123", false)]
        [InlineData("-000", false)]
        [InlineData("-0", false)]
        [InlineData("123.32", false)]
        [InlineData("123454543543534534534534534534534534534534534535", false)]
        [InlineData("0123", true)]
        [InlineData("000", true)]
        // negative case
        public void Test_ValidateNumber_ng(string Number,bool Expected)
        {
            bool actual = _ITranslator.ValidateInput(Number).result;

            Assert.Equal(Expected, actual);
        }

        [Theory]
        [InlineData("1", true)]
        [InlineData("0", true)]
        [InlineData("01",true)]
        [InlineData("001", true)]
        [InlineData("9",true)]
        [InlineData("10", true)]
        [InlineData("1000",true)]
        [InlineData("1234", true)]
        [InlineData("9999", true)]
        [InlineData("999999999", true)]
        //[InlineData("999999999999", true)]
        // Positive cases
        public void Test_ValidateNumber_ps(string Number, bool Expected)
        {
            bool actual = _ITranslator.ValidateInput(Number).result;

            Assert.Equal(Expected, actual);
        }


        [Theory]
        [InlineData("1", "One")]
        [InlineData("0", "Zero")]
        [InlineData("01", "One")]
        [InlineData("001", "One")]
        [InlineData("9", "Nine")]
        [InlineData("10", "Ten")]
        [InlineData("1000", "One Thousand")]
        [InlineData("1234", "One Thousand Two Hundred Thirty Four")]
        [InlineData("9999", "Nine Thousand Nine Hundred Ninety Nine")]
        [InlineData("13456", "Thirteen Thousand Four Hundred Fifty Six")]
        [InlineData("999999999", "Nine Hundred Ninety Nine Million Nine Hundred Ninety Nine Thousand Nine Hundred Ninety Nine")]
        //[InlineData("999999999999", "One Thousand Two Hundred Thirty Four")]      
        public void Test_Translate(string Number, string Excpected)
        {
            string actual = _ITranslator.Translate(Number).result;

            Assert.Equal(Excpected, actual, true);
        }
    }
}
