using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Number2word.Interface;
using Number2word.Model;

namespace Number2word.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private ILogger _logger;
        private ITranslator _ITranslator;
        public TranslatorController(ITranslator ITranslate, ILogger<TranslatorController> logger)
        {
            _ITranslator = ITranslate;
            _logger = logger;
        }

        [HttpGet]
        [Route("Translate/{number}")]
        public async Task<response<string>> Translate(string Number)
        {
            response<string> response = new response<string>();
            try
            {
                response = _ITranslator.Translate(Number);
            }
            catch (Exception ex)
            {
                response.errors = new List<string>();
                response.errors.Add(ex.Message);
                _logger.LogError(ex, "");
            }
            return response;
        }

    }
}