using CSCAssignment.Models;
using CSCAssignment.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSCAssignment.Controllers
{
    public class ProductsV2Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();
        [HttpGet]
        [Route("api/v2/products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();

        }
        [HttpGet]
        [Route("api/v2/products/{id:int}", Name = "getProductById")]
        public HttpResponseMessage GetProduct(int id) //change from Product to HttpResponseMessage to include error messages
        {
            Product item = repository.Get(id);

            HttpResponseMessage response = null;
            if (item == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid id. Invalid get request.");
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.OK, item);
            }
            return response;

        }
        [HttpGet]
        [Route("api/v2/products")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));

        }
        [ValidateModel]
        [HttpPost]
        [Route("api/v2/products")]
        public HttpResponseMessage PostProduct(Product item)
        {

            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
                string uri = Url.Link("getProductById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [ValidateModel]
        [HttpPut]
        [Route("api/v2/products/{id:int}")]
        public HttpResponseMessage PutProduct(int id, Product product)
        {
            product.Id = id;
            HttpResponseMessage response = null;
            if (!repository.Update(product))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Invalid id. Invalid update request.");
                // throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                response = Request.CreateResponse<Product>(HttpStatusCode.OK, product);
            }
            return response;
        }
        [HttpDelete]
        [Route("api/v2/products/{id:int}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            HttpResponseMessage response = null;
            if (item == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, item.Id + " is not found. Invalid delete request.");
            }
            else
            {
                repository.Remove(id);

                response = Request.CreateResponse<Product>(HttpStatusCode.Accepted, item);
            }
            return response;
        }
    }
}
