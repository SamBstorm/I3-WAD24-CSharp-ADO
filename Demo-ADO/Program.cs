using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True;Encrypt=False";

            /*SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;*/

            int nbStudent;

            DataTable table_student = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /* Test connection
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                connection.Open();
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                connection.Close();
                Console.WriteLine($"Le status de la connection est : {connection.State}");
                */
                /* MODE CONNECTÉ : Scalaire (une seule valeur : sinon première valeur (première ligne && première colonne)
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "SELECT COUNT(student_id), AVG(year_result) FROM student";
                    connection.Open();
                    nbStudent = (int)command.ExecuteScalar();
                    connection.Close();
                }
                Console.WriteLine($"Il y a actuellement {nbStudent} étudiant(s) enregistré(s) en base de données.");*/                /* MODE CONNECTÉ : Tabulaire (Un ensemble de valeur : Peu importe le nombre de ligne, ou le nombre de colonne)
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT login, birth_date FROM student";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Login : {reader["login"]} - Date de naissance : {(DateTime)reader["birth_date"]}");
                        }
                    } 
                    connection.Close();
                }*/
                /* MODE DÉCONNECTÉ : Récupération complète (Simulation de la DB via DataSet/DataTable)*/
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM student";
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(table_student);
                }
            }

            foreach (DataRow ligne_db in table_student.Rows)
            {
                Console.WriteLine($"{ligne_db["student_id"]} : {ligne_db["first_name"]} {ligne_db["last_name"]}");
            }

        }
    }
}
