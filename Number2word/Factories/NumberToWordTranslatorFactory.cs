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
        IUnit _iUnit;

        public NumberToWordTranslatorFactory(ILogger<NumberToWordTranslatorFactory> logger)
        { 
            _logger = logger;
            _numberUnitImplementation = new Dictionary<int, string>();
            _numberUnitImplementation.Add(1, "ones");
            _numberUnitImplementation.Add(2, "tens");
           

        }

       
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
                // bool beginsZero = false;//tests for                
                //test for zero or digit zero in a nuemric    
                            


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
           
            try
            {


                // Loads Number unit implementaions as IUnit interface    
                String DigitPlace = string.Empty;        
                int Position = 0;
                bool IsTranslated = true;
                _iUnit = (IUnit)Assembly.GetExecutingAssembly().CreateInstance("Number2word.Implementation." + _numberUnitImplementation[Number.Length]);
                Translation = _iUnit.Translate(Number);
                IsTranslated = _iUnit.IsDone();
                Position = _iUnit.GetPosition();
                DigitPlace = _iUnit.GetDigitPlace();

                //loop 
                if (!IsTranslated)
                {
                   
                        Translation = TranslateNumber(Number.Substring(0, Position)) + TranslateNumber(Number.Substring(Position));
                  
                }
               
            }
            catch(Exception ex)
            {
                // log error and throw it
                _logger.LogError(ex, "");
            }
            return Translation;
        }

    }
}
