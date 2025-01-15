using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ADO_StoredProcedures.Services
{
    internal class UserService
    {
        const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WAD24-ADO-SQLServer;Integrated Security=True;Encrypt=False";
        public void Register(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_User_Insert";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter p_email = new SqlParameter()
                    {
                        ParameterName = "email",
                        Value = email
                    };
                    command.Parameters.Add(p_email);
                    command.Parameters.AddWithValue("password", password);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Login(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SP_User_CheckPassword";
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter p_email = new SqlParameter()
                    {
                        ParameterName = "email",
                        Value = email
                    };
                    command.Parameters.Add(p_email);
                    SqlParameter p_password = new SqlParameter() { 
                        ParameterName = "password",
                        Value = password 
                    };
                    command.Parameters.Add(p_password);
                    connection.Open(); 
                    return (int)command.ExecuteScalar() == 1;
                }
            }
        }
    }
}
