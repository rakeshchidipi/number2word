using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Number2word.Common;
using Number2word.Interface;

namespace Number2word.Implementation
{
    public class ones : IUnit

    {       

        public string GetDigitPlace()
        {
            return string.Empty;
        }

        public int GetPosition()
        {
            return 0;
        }

        public bool IsDone()
        {
            return true;
        }

        public string Translate(string Number)
        {
            return Utility.ones(Number);
        }
    }
}
