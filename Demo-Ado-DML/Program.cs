using Microsoft.Data.SqlClient;

namespace Demo_Ado_DML
{
    internal class Program
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True;Encrypt=False";
        static void Main(string[] args)
        {

            /*Insertion avec ExecuteNonQuery
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO section (section_id, section_name, delegate_id) VALUES (2425,'WAD24',13)";
                    connection.Open();
                    int nb_row = 0;
                    try
                    {
                        nb_row = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex) {
                        Console.WriteLine("Erreur en base de données.");
                    }
                    connection.Close();
                    if (nb_row > 0) {
                        Console.WriteLine("La donnée a bien été enregistrée!");
                    }
                    else
                    {
                        Console.WriteLine("Impossible de sauvegarder la donnée.");
                    }
                }
            }*/

            /* Suppression avec ExecuteScalar (OUTPUT) */
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [section] OUTPUT [deleted].[section_name] WHERE [section_id] = 2425";
                    connection.Open();
                    string section_name = (string)command.ExecuteScalar();
                    connection.Close();
                    Console.WriteLine($"La section supprimée est {section_name}.");
                }
            }
        }
    }
}
