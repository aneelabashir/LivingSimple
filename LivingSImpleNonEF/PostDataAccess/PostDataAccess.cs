using LivingSimpleNonEF.BaseDataAccess;

using LivingSImpleNonEF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LivingSimpleNonEF.DataAccess
{
    public class PostDataAccess : IPostDataAccess
    {
        private IBaseDataAccess _baseDataAccess;
        public PostDataAccess(IBaseDataAccess baseDataAccess)
        {
            _baseDataAccess = baseDataAccess;
        }

        public List<Post> GetPost()
        {
            //change this function to Get(string ProcedureName)
            //add another PostDataAccess file, change current file to BaseDataAccess
            //call BaseDataAccess from PostDataAccess and write a test case for PostDataAccess.GetPosts()

            List<Post> posts = new List<Post>();
            DataTable dtposts = new DataTable();
          
            try
            {
                using (SqlCommand command = new SqlCommand("GetPosts"))
                {
                    dtposts = _baseDataAccess.Get(command);

                    if (dtposts != null && dtposts.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtposts.Rows)
                        {
                            Post post = new Post();
                            post.id = int.Parse(row["id"].ToString());
                            post.imgURL = Convert.ToString(row["imgURL"]);
                            post.numberOfComments = int.Parse(row["numberOfComments"].ToString());
                            post.postContent = Convert.ToString(row["postContent"]);
                            post.postPreviewContent = Convert.ToString(row["postPreviewContent"]);
                            post.title = Convert.ToString(row["title"]);
                            post.date = Convert.ToDateTime(row["date"]);

                            posts.Add(post);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return posts;

        }



        public int AddPost(Post post)
        {
            int id ;
            try
            {

                using (SqlCommand command = new SqlCommand("AddPosts"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@date", post.date);
                    command.Parameters.AddWithValue("@title", post.title);
                    command.Parameters.AddWithValue("@postPreviewContent", post.postPreviewContent);
                    command.Parameters.AddWithValue("@postContent", post.postContent);
                    command.Parameters.AddWithValue("@imgURL", post.imgURL);
                    command.Parameters.AddWithValue("@numberOfComments", Convert.ToInt32(post.numberOfComments));

                    id = _baseDataAccess.Add(command);
                }


                //DataTable dataTable = new DataTable();
                //PropertyInfo[] Props = typeof(Post).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //foreach (PropertyInfo prop in Props)
                //{
                //    dataTable.Columns.Add(prop.Name);
                //}

                //var values = new object[Props.Length];
                //for (int i = 0; i < Props.Length; i++)
                //{
                //    values[i] = Props[i].GetValue(post, null);
                //}
                //dataTable.Rows.Add(values);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;



        }
    }
}
