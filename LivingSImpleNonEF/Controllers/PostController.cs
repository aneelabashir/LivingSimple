using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivingSimpleNonEF.DataAccess;

using LivingSImpleNonEF.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LivingSImpleNonEF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private IPostDataAccess _ipostDataAccess;

        public PostController(IPostDataAccess ipostDataAccess)
        {
            _ipostDataAccess = ipostDataAccess;
        }

        //[EnableCors("AllowOrigin")]
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            List<Post> posts = new List<Post>();
            posts =  _ipostDataAccess.GetPost();
            return posts;
        }
        [HttpPost]
        public int Post(Post post)
        {
            int id;
            id = _ipostDataAccess.AddPost(post);
            return id;
           
        }
        //[EnableCors("AllowOrigin")]
        //[HttpGet]
        //[Route("{id}")]
        //public Post Get(int id)
        //{
        //    return _postDataAccess.Posts.Where(x => x.id == id).FirstOrDefault();
        //}
        //[EnableCors("AllowOrigin")]
        //[HttpPost]
        //public bool Post(Post p)
        //{
        //    Post updatePost = _dbContext.Posts.Where(x => x.id == p.id).FirstOrDefault();

        //    updatePost.postContent = p.postContent;

        //    _dbContext.SaveChanges();

        //    return true;
        //}
    }
}
