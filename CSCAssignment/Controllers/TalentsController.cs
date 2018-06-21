using CSCAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSCAssignment.Controllers
{
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        
        [HttpGet]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [HttpGet]
        [Route("api/talents/{id:int}", Name = "getTalentById")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }


        [HttpPost]
        [Route("api/talents")]
        public HttpResponseMessage PostTalent(Talent item)
        {
            if (!(String.IsNullOrWhiteSpace(item.Name) || String.IsNullOrWhiteSpace(item.ShortName) ||
                    String.IsNullOrWhiteSpace(item.Bio) || String.IsNullOrWhiteSpace(item.Reknown)) &&
                    ModelState.IsValid)
            {

                item = repository.Add(item);
                var response = Request.CreateResponse<Talent>(HttpStatusCode.Created, item);

                // Generate a link to the new talent and set the Location header in the response.

                string uri = Url.Link("getTalentById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpPut]
        [Route("api/talents/{id:int}")]
        public HttpResponseMessage PutTalent(int id, Talent talent)
        {
            HttpResponseMessage response = null;
            talent.Id = id;
            if (!repository.Update(talent))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid id. Invalid update request.");
            }
            else
            {
                response = Request.CreateResponse<Talent>(HttpStatusCode.OK, talent);
            }
            return response;
        }


        [HttpDelete]
        [Route("api/talents/{id:int}")]
        public void DeleteTalent(int id)
        {
            repository.Remove(id);
        }


    }
}
