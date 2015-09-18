using People_Search.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace People_Search.Controllers
{
    public class PeopleController : ApiController
    {
        //static readonly PeopleRepository repository = new PeopleRepository();
        readonly PeopleRepository repository;
        
        public PeopleController()
        {
            this.repository = new PeopleRepository();
        }

        public PeopleController(PeopleRepository repository)
        {
            this.repository = repository;
        }





        private List<UserDetail> testProducts;
        public Func<object, UserDetail> r;

        public PeopleController(List<UserDetail> testProducts)
        {
            this.testProducts = testProducts;
        }

        [Route("api/getpeople/search/{key?}")]
        public HttpResponseMessage Get(string key)
        {
            PeopleRepository p = new PeopleRepository();
            var result = p.SearchPeople(key);



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        

        [Route("api/register")]
        public HttpResponseMessage Put([FromBody] UserDetail u)
        {



            var q = PeopleRepository.InsertUser(u);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, u.address);
            return response;
        }

        
          
       


   
    }
}

