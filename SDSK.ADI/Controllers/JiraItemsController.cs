using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using SDSK.API.Model;

namespace SDSK.API.Controllers
{
    public class JiraItemsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //GET api/jiraitems/{id}
        [Route("api/jiraitems/{id}")]
        public JiraItem Get(int id = 1)
        {
            var jiraItem = Data.JiraItems.SingleOrDefault(x => x.JiraItemId == id);
            if (jiraItem != null)
            {
                return jiraItem;
            }
            else
            {
                var message = $"JiraItem with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }


        //GET api/jiraitems/{id:jiraid}
        [Route("api/jiraitems/{id:jiraid}")]
        public IHttpActionResult Get(string id)
        {
            id = id.Substring(5);
            int val;
            var isNumber = Int32.TryParse(id, out val);
            if (isNumber)
            {
                var model = Data.JiraItems.FirstOrDefault(j => j.JiraItemId == val);
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            return NotFound();
        }
    }
}

