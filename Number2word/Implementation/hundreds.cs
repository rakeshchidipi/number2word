using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Number2word.Interface;

namespace Number2word.Implementation
{
    public class hundreds : IUnit
    {
        private string _number;
              
        public string GetDigitPlace()
        {
            return " Hundred ";
        }

        public int GetPosition()
        {
            return (_number.Length % 3) + 1;
        }

        public bool IsDone()
        {
            return false;
        }

        public string Translate(string Number)
        {
            _number = Number;          
             return "";
        }
    }
}
