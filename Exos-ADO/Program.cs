using Exos_ADO.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Exos_ADO
{
    internal class Program
    {
        const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True;Encrypt=False";
        static void Main(string[] args)
        {
            /* Mode Connecté */
            Console.WriteLine($"Voici la liste des sections, veuillez en sélectionner une :");
            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [section_id], [section_name] FROM [section]";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["section_id"]} : {reader["section_name"]}");
                        }
                    }
                    connection.Close();
                }
                string inputUser = Console.ReadLine();
                int delegate_id;
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = $"SELECT [delegate_id] FROM [section] WHERE [section_id] = {inputUser}";
                    connection.Open();
                    delegate_id = (int)command.ExecuteScalar();
                    connection.Close();
                }
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT [first_name], [last_name] FROM [student] WHERE [student_id] = {delegate_id}";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Le délégué de la section choisie est : {reader["first_name"]} {reader["last_name"]}");
                        }
                    }
                    connection.Close();
                }
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT COUNT(student_id) FROM student WHERE section_id = {inputUser}";
                    connection.Open();
                    int nb_students = (int)command.ExecuteScalar();
                    Console.WriteLine($"Il y a en tout {nb_students} étudiant(s) dans cette section.");
                    connection.Close();
                }
            }
            List<Personne> personnes = new List<Personne>();
            DataTable datas = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [first_name], [last_name] FROM [student] UNION SELECT [professor_surname], [professor_name] FROM [professor]";
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(datas);
                }
            }
            foreach (DataRow row in datas.Rows)
            {
                personnes.Add(new Personne(row["first_name"].ToString(), (string)row["last_name"]));
            }
            foreach (Personne p in personnes)
            {
                Console.WriteLine($"{p.Prenom} {p.Nom}");
            }
        }
    }
}
