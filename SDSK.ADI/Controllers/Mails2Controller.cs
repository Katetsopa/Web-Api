using SDSK.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SDSK.API.Constraints;

namespace SDSK.API.Controllers
{
   // [RoutePrefix("api/mails")]
    public class Mails2Controller : ApiController
    {
        //GET /api/mails
        [VersionedRoute("api/mails", 2)]
        public IEnumerable<Mail> Get()
        {
            return Data.Mails;
        }

        //GET /api/mails/{id}
        [VersionedRoute("api/mails/{id}", 2)]
        public Mail Get(int id)
        {
            var mail = Data.Mails.SingleOrDefault(x => x.Id == id);
            if (mail != null)
                return mail;
            else
            {
                var message = $"Mail with id = {id} not found";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }

        //POST /api/mails
        [VersionedRoute("api/Customer", 2)]
        public void Post([FromBody]Mail mail)
        {
            if (mail != null && ModelState.IsValid)
            {
                Data.Mails.Add(mail);
            }
            else
            {
                var message = "invalid input mail";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
        }

        //PUT /api/mails/{id}
        [VersionedRoute("api/Customer", 2)]
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
        [VersionedRoute("api/Customer", 2)]
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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }
    }
}
