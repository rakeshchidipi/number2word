using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Number2word.Controllers;
using Number2word.Interface;
using Number2word.Model;

namespace Number2word.Factories
{
    public class NumberToWordTranslatorFactory : ITranslator
    {
        private Dictionary<int, string> _numberUnitImplementation;       
        private ILogger _logger;

        public NumberToWordTranslatorFactory(ILogger<NumberToWordTranslatorFactory> logger)
        
        {
            _logger = logger;
            _numberUnitImplementation = new Dictionary<int, string>();
            _numberUnitImplementation.Add(1, "ones");
            _numberUnitImplementation.Add(2, "tens");
            

        }

        /// <summary>
        /// This factory method automatically resolves concreate class and expossed as interface
        /// </summary>
        /// <param name="Number">Input valid number</param>
        /// <returns>string</returns>
        public response<string> Translate(string Number)
        {

            response<string> response = new response<string>();
            response<bool> validationresponse = ValidateInput(Number);
            try
            {
                if (validationresponse.result)
                {
                    response.result = TranslateNumber(Number);
                }
                else
                {
                    response.errors = validationresponse.errors;
                }

            }
            catch (Exception ex)
            {

                // log error and throw it
                _logger.LogError(ex,"");
            }

            return response;

        }

        /// <summary>
        /// Validated given number as per business rules
        /// </summary>
        /// <param name="Number">Input valid number</param>
        /// <returns>boolean</returns>
        public response<bool> ValidateInput(String Number)
        {
            response<bool> response = new response<bool>();
            try
            {
                response.result = true;
                //validations
                // supports 12 chars
                // should be +ve number
                // should not contain decimals
                // can not have strings allowed numbers only.
                // bool beginsZero = false;//tests for 0XX   
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                //if (dblAmt > 0)
                //{ }
                //test for zero or digit zero in a nuemric    
                //beginsZero = Number.StartsWith("0");
               


            }
            catch (Exception ex)
            {
                // log error and throw it
                _logger.LogError(ex, "");
            }

            return response;
        }


        private string TranslateNumber(String Number)
        {
            
            string Translation = string.Empty;
           
            return Translation;
        }

    }
}
