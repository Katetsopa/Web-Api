using SDSK.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using SDSK.API.Constraints;

namespace SDSK.API.Controllers
{
    [RoutePrefix("api/mails")]
    [VersionedRoute("api/mails", 1)]
    public class MailsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //GET /api/mails
        public IEnumerable<Mail> Get()
        {
            return Data.Mails;
        }

        //GET /api/mails/{id}
        [VersionedRoute("{id}", 1)]
        public Mail Get(int id)
        {
            var mail = Data.Mails.SingleOrDefault(x => x.Id == id);
            if (mail != null)
                return mail;
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }

        //POST /api/mails
        public void Post([FromBody]Mail mail)
        {
            if (mail != null && ModelState.IsValid)
            {
                Data.Mails.Add(mail);
            }
            else
            {
                var message = "invalid input mail";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
        }

        //PUT /api/mails/{id}
        [VersionedRoute("{id}", 1)]
        public void Put(int id, [FromBody]Mail mail)
        {
            if (mail != null && ModelState.IsValid)
            {
                var mailToUpdate = Data.Mails.SingleOrDefault(x => x.Id == id);
                if (mailToUpdate != null)
                {
                    Data.Mails.Remove(mailToUpdate);
                    Data.Mails.Add(mail);
                }
                else
                {
                    var message = $"Mail with id = {id} not found";
                    Log.Error(message);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
            }
            else
            {
                var message = "Invalid input mail";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
        }

        //DELETER /api/mails/{id}
        [VersionedRoute("{id}", 1)]
        public void Delete(int id)
        {
            var mail = Data.Mails.SingleOrDefault(x => x.Id == id);
            if (mail != null)
            {
                Data.Mails.Remove(mail);
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }
    }
}