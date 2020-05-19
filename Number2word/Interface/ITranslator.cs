using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Number2word.Controllers;
using Number2word.Model;

namespace Number2word.Interface
{
   public interface ITranslator
    {
        response<string> Translate(string Number);
        response<bool> ValidateInput(String Number);
    }
}
