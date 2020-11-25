using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingSImpleNonEF.Model
{
    public class Post
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string postPreviewContent { get; set; }
        public string postContent { get; set; }
        public int numberOfComments { get; set; }
        public string imgURL { get; set; }

        public bool Equals(Post post)
        {
            return id.Equals(post.id) && date.Equals(post.date) && title.Equals(post.title) && postPreviewContent.Equals(post.postPreviewContent) && postContent.Equals(post.postContent)
                && numberOfComments.Equals(post.numberOfComments) && imgURL.Equals(post.imgURL);
        }
    }

    //public static class PostExtensions
    //{
    //    public static bool Equals(this Post current, Post post)
    //    {
    //        return current.id.Equals(post.id) && current.date.Equals(post.date) && current.title.Equals(post.title) && current.postPreviewContent.Equals(post.postPreviewContent) && current.postContent.Equals(post.postContent)
    //            && current.numberOfComments.Equals(post.numberOfComments) && current.imgURL.Equals(post.imgURL);
    //    }
    //}
}
