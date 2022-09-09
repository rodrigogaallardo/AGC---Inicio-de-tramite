using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace ServiceLayerExt.Controllers
{
    public class AuthController : ApiController
    {
        // GET api/auth
        public string Get(string id)
        {
            FormsAuthentication.SetAuthCookie(id ?? "FooUser", false);
            return "You are autenthicated now";
        }
        
        // POST api/auth
        public void Post([FromBody]string value)
        {
        }

        // PUT api/auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/auth/5
        public void Delete(int id)
        {
        }
    }
}
