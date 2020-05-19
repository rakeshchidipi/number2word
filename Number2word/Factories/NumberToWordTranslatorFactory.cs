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
        IUnit _iUnit;
        private ILogger _logger;

        
        public NumberToWordTranslatorFactory()
        {
            
            _numberUnitImplementation = new Dictionary<int, string>();
            _numberUnitImplementation.Add(1, "ones");
            _numberUnitImplementation.Add(2, "tens");
            _numberUnitImplementation.Add(3, "hundreds");
            _numberUnitImplementation.Add(4, "thousand");
            _numberUnitImplementation.Add(5, "thousand");
            _numberUnitImplementation.Add(6, "thousand");
            _numberUnitImplementation.Add(7, "million");
            _numberUnitImplementation.Add(8, "million");
            _numberUnitImplementation.Add(9, "million");

            //**** Just uncomment below implementation to support billion upto 12 digits
            //** Rest all code will be same .un comment below 3 lines. no other changes required

           // _numberUnitImplementation.Add(10, "billion");
           // _numberUnitImplementation.Add(11, "billion");
           // _numberUnitImplementation.Add(12, "billion");

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
            Boolean isvalid = true;
            try
            {
              
               
                //validations
                // supports 12 chars
                // should be +ve number
                // should not contain decimals
                // can not have strings allowed numbers only.
                // bool beginsZero = false;//tests for 0XX   

                if (string.IsNullOrEmpty(Number))
                {
                   isvalid = false;
                }
                if (Number.Contains("."))
                {
                    isvalid = false;
                }
                double number;
                isvalid= double.TryParse(Number, out number);
                if (isvalid && number<0)
                {
                    isvalid= false;                   
                }

            }
            catch (Exception ex)
            {
                // log error and throw it
                _logger.LogError(ex, "");
            }
            response.result = isvalid;
            return response;
        }


        private string TranslateNumber(String Number)
        {
            //Local variables
            string Translation = string.Empty;
            String DigitPlace = string.Empty;          //digit grouping name:hundres,thousand,etc... 
            bool IsTranslated = true;                  //test if already translated 
            int Position = 0;                          //store digit grouping    
           
            try
            {
                //Handling zeros
                 Number = (Convert.ToDouble(Number).ToString());
                if (Convert.ToDouble(Number) == 0) { return "Zero"; }

               // Loads Number unit implementaions as IUnit interface                
                _iUnit = (IUnit)Assembly.GetExecutingAssembly().CreateInstance("Number2word.Implementation." + _numberUnitImplementation[Number.Length]);
                Translation = _iUnit.Translate(Number);
                IsTranslated = _iUnit.IsDone();
                Position = _iUnit.GetPosition();
                DigitPlace = _iUnit.GetDigitPlace();

                //if transalation is not done, continue...(Recursion comes in now!!)  
                if (!IsTranslated)
                {
                    if (Number.Substring(0, Position) != "0" && Number.Substring(Position) != "0")
                    {
                        Translation = TranslateNumber(Number.Substring(0, Position)) + DigitPlace + TranslateNumber(Number.Substring(Position));
                    }
                    else
                    {
                        Translation = TranslateNumber(Number.Substring(0, Position)) + TranslateNumber(Number.Substring(Position));
                    }
                }
                // if (Translation.Trim().Equals(DigitPlace.Trim())) Translation = "";
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
