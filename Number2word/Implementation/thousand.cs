using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Number2word.Interface;

namespace Number2word.Implementation
{
    public class thousand : IUnit
    {
        private string _number;
        public string GetDigitPlace()
        {
            return " Thousand ";
        }

        public int GetPosition()
        {
            return (_number.Length % 4) + 1;
        }

        public bool IsDone()
        {
            return false;
        }

        public string Translate(string Number)
        {
            _number = Number;
            return string.Empty;
        }
    }
}
