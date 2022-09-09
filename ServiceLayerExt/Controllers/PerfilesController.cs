using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceLayerExt.Controllers
{
    [Authorize]
    public class PerfilesController : ApiController
    {
        // GET api/perfiles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/perfiles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/perfiles
        public void Post([FromBody]string value)
        {
        }

        // PUT api/perfiles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/perfiles/5
        public void Delete(int id)
        {
        }
    }
}
