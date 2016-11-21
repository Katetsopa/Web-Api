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
    [VersionedRoute("api/mails", 2)]
    public class Mails2Controller : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //GET /api/mails
        public IEnumerable<Mail> Get()
        {
            return Data.Mails;
        }

        //GET /api/mails/{id}
        [VersionedRoute("{id}", 2)]
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
        [VersionedRoute("{id}", 2)]
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
        [VersionedRoute("{id}", 2)]
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


        //GET api/mails/{id}/attachements
        [Route("{id}/attachements")]
        [VersionedRoute("{id}/attachments", 2)]
        public IEnumerable<Attachement> GetAttachements(int id)
        {
            if (Data.Mails.Exists(x => x.Id == id))
            {
                if (Data.AttList.Where(x => x.MailId == id).Any())
                {
                    return Data.AttList.Where(x => x.MailId == id);
                }
                else
                {
                    var message = $"Mail with id = {id} don't have any attachments";
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }


        //GET api/mails/{id}/attachements/{attId}
        [VersionedRoute("{id}/attachments/{attId}", 2)]
        public IEnumerable<Attachement> Get(int id, int attId)
        {

            if (Data.Mails.Exists(x => x.Id == id))
            {
                if (Data.AttList.Where(x => x.MailId == id).Where(c => c.Id == attId).Any())
                {
                    return Data.AttList.Where(x => x.MailId == id).Where(c => c.Id == attId);
                }
                else
                {
                    var message = $"Mail with id = {id} don't have any attachments with id = {attId}";
                    Log.Error(message);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }


        //GET api/mails/{id}/attachements/extention = { ext }? status = { status }
        [VersionedRoute("{id}/attachments", 2)]
        public IEnumerable<Attachement> Get(int id, string ext = null, int status = 0)
        {
            if (Data.Mails.Exists(x => x.Id == id))
            {
                IEnumerable<Attachement> result = Data.AttList;
                if (ext.Equals(null))
                    result = result.Where(x => x.FileExtention == ext);
                if (status != 0)
                    result = result.Where(x => x.StatusId == status);
                return result;
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }



        //PUT api/mails/{id}/attachements/{attId}
        [VersionedRoute("{id}/attachements/{attId}", 2)]
        public void Put(int id, int attId, [FromBody]Attachement attach)
        {
            if (Data.Mails.Exists(x => x.Id == id))
            {
                var attachToUpdate = Data.AttList.Where(x => x.MailId == id).SingleOrDefault(c => c.Id == attId);
                if (attachToUpdate.Equals(null))
                {
                    var message = $"Attachment with id = {attId} don't exist";
                    Log.Error(message);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                else
                {
                    Data.AttList.Remove(attachToUpdate);
                    Data.AttList.Add(attach);
                }
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }



        //POST api/mails/{id}/attachements
        [VersionedRoute("{id}/attachements", 2)]
        public void Post(int id, [FromBody]Attachement attachement)
        {
            if (Data.Mails.Exists(x => x.Id == id))
            {
                if (attachement != null && ModelState.IsValid)
                {
                    Data.AttList.Add(attachement);
                }
                else
                {
                    var message = "invalid input attachment";
                    Log.Error(message);
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                Log.Error(message);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }



        //DELETE api/mails/{id}/attachements/{attId}
        [VersionedRoute("{id}/attachements/{attId}", 2)]
        public void Delete(int id, int attId)
        {
            if (Data.Mails.Exists(x => x.Id == id))
            {
                var attach = Data.AttList.Where(x => x.MailId == id).SingleOrDefault(c => c.Id == attId);
                if (attach != null && ModelState.IsValid)
                {
                    Data.AttList.Remove(attach);
                }
                else
                {
                    var message = $"Attachment with id = {attId} don't exist";
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
            }
            else
            {
                var message = $"Mail with id = {id} not found";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
        }
    }
}
