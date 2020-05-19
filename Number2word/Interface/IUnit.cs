using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Number2word.Interface
{
    interface IUnit
    {
        string Translate(string Number);
        string GetDigitPlace();
        bool IsDone();       
        int GetPosition();       

    }
}
