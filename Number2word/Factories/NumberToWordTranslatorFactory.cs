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
       
        private ILogger _logger;

      
        public NumberToWordTranslatorFactory()
        {
           

        }

        /// <summary>
        /// This factory method automatically resolves concreate class and expossed as interface
        /// </summary>
        /// <param name="Number">Input valid number</param>
        /// <returns>string</returns>
        public response<string> Translate(string Number)
        {

            response<string> response = new response<string>();
            

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
            

            return response;
        }


       
}
