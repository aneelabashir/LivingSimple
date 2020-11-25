using Microsoft.AspNetCore.Mvc;
using LivingSImpleNonEF.Controllers;
using LivingSImpleNonEF.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using LivingSimpleNonEF.DataAccess;
using LivingSImpleNonEF.BaseDataAccess;
using LivingSimpleNonEF.BaseDataAccess;

namespace XUnitTestSample
{
    public class PostUnitTest
    {
        public PostUnitTest()
        {
            
        }

        [Fact]
        public void BaseDataAccessGet_True()
        {
            List<Post> posts = new List<Post>()
            {
                new Post() {
                    id = 1,
                    date = DateTime.Now,
                    imgURL = "",
                    numberOfComments = 1,
                    postContent = "test content 1",
                    postPreviewContent = "preview 1",
                    title = "Post 1"
                },
                new Post() {
                    id = 2,
                    date = DateTime.Now,
                    imgURL = "/img1",
                    numberOfComments = 1,
                    postContent = "test content 2",
                    postPreviewContent = "preview here",
                    title = "Post TWO"
                }
            };

            var sqlConnectionMock = new Mock<IDbConnection>();
            var sqlCommandMock = new Mock<IDbCommand>();
            var sqlDataReaderMock = new Mock<IDataReader>();

            sqlDataReaderMock.Setup(c => c.FieldCount).Returns(2);

            sqlDataReaderMock.Setup(c => c.GetFieldType(0)).Returns(typeof(int));
            sqlDataReaderMock.Setup(c => c.GetFieldType(1)).Returns(typeof(string));

            // Columns
            sqlDataReaderMock.Setup(c => c.GetName(0)).Returns("Id");
            sqlDataReaderMock.Setup(c => c.GetName(1)).Returns("Name");

            sqlDataReaderMock.Setup(m => m.GetOrdinal("Id")).Returns(0);

            // Rows
            // NOTE: Unfortunately this only works for a single row.
            sqlDataReaderMock.Setup(c => c.GetValues(It.IsAny<object[]>())).Callback<object[]>(
                (values) =>
                {
                    values[0] = 1;
                    values[1] = "Luggar";
                }
            ).Returns(2);

            // Read one row
            sqlDataReaderMock.SetupSequence(c => c.Read()).Returns(true).Returns(false);

            DataTable table1 = new DataTable();
            table1.Columns.Add("Id", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Rows.Add("1", "Luggar");

            BaseDataAccess _baseDataAccess = new BaseDataAccess(sqlConnectionMock.Object);

            sqlConnectionMock.Setup(conn => conn.CreateCommand()).Returns(sqlCommandMock.Object);

            sqlCommandMock.Setup(command => command.ExecuteReader()).Returns(sqlDataReaderMock.Object);

            sqlConnectionMock.Setup(conn => conn.CreateCommand()).Returns(sqlCommandMock.Object);

            DataTable returnDt = _baseDataAccess.Get(sqlCommandMock.Object);

            sqlConnectionMock.Verify((m => m.Open()), Times.Once());
            sqlCommandMock.Verify((m => m.ExecuteReader()), Times.Once());
            sqlDataReaderMock.Verify((m => m.Close()), Times.Once());
            sqlConnectionMock.Verify((m => m.Close()), Times.Once());



            Assert.Equal(table1.Columns.Count, returnDt.Columns.Count);
            Assert.Equal(table1.Rows[0].Field<int>("Id"), returnDt.Rows[0].Field<int>("Id"));
            Assert.Equal(table1.Rows[0].Field<string>("Name"), returnDt.Rows[0].Field<string>("Name"));
        }

        [Fact]
        public void PostDataAccessGet_True()
        {
            List<Post> posts = new List<Post>()
            {
                new Post() {
                    id = 1,
                    date = new DateTime(2020,11,20),
                    imgURL = "",
                    numberOfComments = 1,
                    postContent = "test content 1",
                    postPreviewContent = "preview 1",
                    title = "Post 1"
                },
                new Post() {
                    id = 2,
                    date = new DateTime(2020,11,20),
                    imgURL = "/img1",
                    numberOfComments = 1,
                    postContent = "test content 2",
                    postPreviewContent = "preview 2",
                    title = "Post 2"
                }
            };

            DataTable table1 = new DataTable();
            table1.Columns.Add("Id", typeof(int));
            table1.Columns.Add("date", typeof(DateTime));
            table1.Columns.Add("imgURL", typeof(string));
            table1.Columns.Add("numberOfComments", typeof(int));
            table1.Columns.Add("postContent", typeof(string));
            table1.Columns.Add("postPreviewContent", typeof(string));
            table1.Columns.Add("title", typeof(string));
            table1.Rows.Add("1", new DateTime(2020, 11, 20), "", "1", "test content 1", "preview 1", "Post 1");
            table1.Rows.Add("2", new DateTime(2020, 11, 20), "/img1", "1", "test content 2", "preview 2", "Post 2");


            Mock<IBaseDataAccess> mock = new Mock<IBaseDataAccess>();


            mock.Setup(x => x.Get(It.IsAny<IDbCommand>())).Returns(table1);

            PostDataAccess postDataAccess = new PostDataAccess(mock.Object);
            List<Post> p = new List<Post>();

            p = postDataAccess.GetPost();

            mock.Verify((m => m.Get(It.IsAny<IDbCommand>())), Times.Once());

            Assert.Equal(2, posts.Count);
            Assert.Equal(posts.Count, p.Count);
            Assert.True(posts[0].Equals(p[0]));
            Assert.True(posts[1].Equals(p[1]));
        }


        [Fact]
        public void PostControllerGet_True()
        {
            List<Post> postlist = new List<Post>()
            {
                new Post() {
                    id = 1,
                    date = DateTime.Now,
                    imgURL = "",
                    numberOfComments = 1,
                    postContent = "test content 1",
                    postPreviewContent = "preview 1",
                    title = "Post 1"
                },
                new Post() {
                    id = 2,
                    date = DateTime.Now,
                    imgURL = "/img1",
                    numberOfComments = 1,
                    postContent = "test content 2",
                    postPreviewContent = "preview 2",
                    title = "Post 2"
                }
            };


            Mock<IPostDataAccess> mock = new Mock<IPostDataAccess>();

            mock.Setup(x => x.GetPost()).Returns(postlist);

            PostController postController = new PostController(mock.Object);
            List<Post> p = new List<Post>();

            p = postController.Get().ToList();

            mock.Verify((m => m.GetPost()), Times.Once());

            Assert.Equal(postlist.Count, p.Count);
            Assert.Equal(postlist, p);

        }


        [Fact]
        public void BaseDataAccessAdd_True()
        {
            var sqlConnectionMock = new Mock<IDbConnection>();
            var sqlCommandMock = new Mock<IDbCommand>();
           

            BaseDataAccess _baseDataAccess = new BaseDataAccess(sqlConnectionMock.Object);
                        
            sqlCommandMock.Setup(command => command.ExecuteScalar()).Returns(1);
            sqlCommandMock.SetupProperty(command => command.Connection);
                               
            int id = _baseDataAccess.Add(sqlCommandMock.Object);

            sqlConnectionMock.Verify((m => m.Open()), Times.Once());
            sqlCommandMock.Verify((m => m.ExecuteScalar()), Times.Once());
            sqlConnectionMock.Verify((m => m.Close()), Times.Once());
            

            Assert.Equal(sqlConnectionMock.Object, sqlCommandMock.Object.Connection);
            Assert.Equal(1, id);
        }

        [Fact]
        public void PostDataAccessAdd_True()
        {
            Post post = new Post() {
                id = 1,
                date = DateTime.Now,
                imgURL = "",
                numberOfComments = 1,
                postContent = "test content 1",
                postPreviewContent = "preview 1",
                title = "Post 1"
            };

            var baseDataAccessMock = new Mock<IBaseDataAccess>();
            PostDataAccess postDataAccess = new PostDataAccess(baseDataAccessMock.Object);
            
            baseDataAccessMock.Setup(m => m.Add(It.Is<SqlCommand>(c =>
                c.CommandType.Equals(CommandType.StoredProcedure)
                && c.Parameters["@date"].Value.Equals(post.date)
                && c.Parameters["@title"].Value.Equals(post.title)
                && c.Parameters["@postPreviewContent"].Value.Equals(post.postPreviewContent)
                && c.Parameters["@postContent"].Value.Equals(post.postContent)
                && c.Parameters["@imgURL"].Value.Equals(post.imgURL)
                && c.Parameters["@numberOfComments"].Value.Equals(post.numberOfComments)
            ))).Returns(1);

            int id = postDataAccess.AddPost(post);

            baseDataAccessMock.Verify((m => m.Add(It.Is<SqlCommand>(c =>
                c.CommandType.Equals(CommandType.StoredProcedure)
                && c.Parameters["@date"].Value.Equals(post.date)
                && c.Parameters["@title"].Value.Equals(post.title)
                && c.Parameters["@postPreviewContent"].Value.Equals(post.postPreviewContent)
                && c.Parameters["@postContent"].Value.Equals(post.postContent)
                && c.Parameters["@imgURL"].Value.Equals(post.imgURL)
                && c.Parameters["@numberOfComments"].Value.Equals(post.numberOfComments)
            ))), Times.Once());

            Assert.Equal(1, id);
        }

        [Fact]
        public void PostControllerPost_True()
        {
            Post post = new Post()
            {
                id = 1,
                date = DateTime.Now,
                imgURL = "",
                numberOfComments = 1,
                postContent = "test content 1",
                postPreviewContent = "preview 1",
                title = "Post 1"
            };


            Mock<IPostDataAccess> mock = new Mock<IPostDataAccess>();

            mock.Setup(x => x.AddPost(post)).Returns(1);

            PostController postController = new PostController(mock.Object);
            
            int id = postController.Post(post);

            mock.Verify((m => m.AddPost(post)), Times.Once());

            Assert.Equal(1, id);
        }
    }
}
