using Microsoft.Data.SqlClient;

namespace Demo_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True;Encrypt=False";

            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;*/

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /* Test connection
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                connection.Open();
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                connection.Close();
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                */
                using (SqlCommand command = connection.CreateCommand()) { 
                
                }
            }
        }
    }
}
