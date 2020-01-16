using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PostManager.Controllers
{
    public class PostsController : ApiController
    {
        DL.JsonManager jsonManager { get; set; }
        protected virtual void Initialize()
        {
            jsonManager = new DL.JsonManager();
            jsonManager.LoadComments(string.Format(".\\{0}", jsonManager.commentsFileName));
            jsonManager.LoadPosts(string.Format(".\\{0}", jsonManager.postsFileName));
        }
        // GET: api/Posts
        public string Get()
        {
            return jsonManager.retrieveAllPosts();
            
        }

        // GET: api/Posts/5
        public string Get(int id)
        {
            return jsonManager.retrieveAPostWithComments(id);
        }

        // POST: api/Posts
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Posts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Posts/5
        public void Delete(int id)
        {
        }
    }
}
