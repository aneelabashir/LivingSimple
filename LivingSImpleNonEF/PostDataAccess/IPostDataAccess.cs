using LivingSImpleNonEF.Model;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LivingSimpleNonEF.DataAccess
{
    public interface IPostDataAccess
    {
        List<Post> GetPost();
        int AddPost(Post post);
    }
}