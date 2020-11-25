using LivingSimpleNonEF.BaseDataAccess;
using LivingSImpleNonEF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LivingSImpleNonEF.BaseDataAccess
{
    public class BaseDataAccess : IBaseDataAccess
    {
        IDbConnection _sqlConnection;

        public BaseDataAccess(IDbConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public DataTable Get(IDbCommand command)
        {
            //change this function to Get(string ProcedureName)
            //add another PostDataAccess file, change current file to BaseDataAccess
            //call BaseDataAccess from PostDataAccess and write a test case for PostDataAccess.GetPosts()

            DataTable dtPosts = new DataTable();

            try
            {
                _sqlConnection.Open();

                command.Connection = _sqlConnection;

                IDataReader _sqlDataReader = command.ExecuteReader();

                dtPosts.Load(_sqlDataReader);
                
                //_sqlDataReader.Close();
                
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtPosts;

        }


        public int Add(IDbCommand command)
        {
            int id = 0;

            try
            {
                _sqlConnection.Open();

                command.Connection = _sqlConnection;
         
                id = Convert.ToInt32(command.ExecuteScalar());

                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;

        }
    }
}
