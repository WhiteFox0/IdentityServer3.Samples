using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JsApp.WebApi.Controllers
{
    [Route("values")]
    public class ValuesController:ApiController
    {
        public IEnumerable<string> Get()
        {
            var random = new Random();
            return new[]
            {
                random.Next(0, 10).ToString(),
                random.Next(0, 10).ToString()
            };
        }
        
    }
}
