using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Number2word.Model
{
    public class response<T>
    {
        public List<string> errors { get; set; }
        public T result { get; set; }
        
      
    }
}
