using Demo_Ado_DML.Models;
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

            /* Suppression avec ExecuteScalar (OUTPUT) 
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
            */

            /* Insertion avec SqlParameter 

            //// Sans valeur null
            //Section wad = new Section() { 
            //    Section_Id = 2425,
            //    Section_Name = "WAD24",
            //    Delegate_Id = 13
            //};

            // Avec valeur null
            Section wad = new Section() { 
                Section_Id = 2526,
                Section_Name = "WAD25",
                Delegate_Id = null
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [section] ([section_id], [section_name], [delegate_id]) VALUES (@section_id, @section_name, @delegate_id)";
                    //Version Longue sans Null (compatible avec tout les types de serveur)
                    SqlParameter p_section_id = new SqlParameter()
                    {
                        ParameterName = "section_id",
                        Value = wad.Section_Id
                    };
                    command.Parameters.Add(p_section_id);
                    //Version Longue avec Null (compatible avec tout les types de serveur)
                    SqlParameter p_section_name = new SqlParameter()
                    {
                        ParameterName = "section_name",
                        Value = (object)wad.Section_Name ?? DBNull.Value
                    };
                    command.Parameters.Add(p_section_name);
                    //Version Courte avec Null (compatible avec SqlServer seulement)
                    command.Parameters.AddWithValue("delegate_id", (object)wad.Delegate_Id ?? DBNull.Value);
                    connection.Open();
                    int nb_row = command.ExecuteNonQuery();
                    if (nb_row > 0)
                    {
                        Console.WriteLine("Enregistrement effectué!");
                    }
                    else
                    {
                        Console.WriteLine("Impossible de sauvegarder.");
                    }
                }
            }*/

            /* Récupération de données Nullable */

            int section_id = 2526;
            Section wad = new Section();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "SELECT [section_id], [section_name], [delegate_id] FROM [section] WHERE [section_id] = @section_id";
                    command.Parameters.AddWithValue(nameof(section_id), section_id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wad = new Section()
                            {
                                Section_Id = (int)reader["section_id"],
                                Section_Name = (reader["section_name"] is DBNull)? null :(string?)reader["section_name"],
                                Delegate_Id = (reader["delegate_id"] is DBNull) ? null : (int?)reader["delegate_id"]
                            };
                        }
                    }
                }
            }

            Console.WriteLine($"Voici le groupe {wad.Section_Name ?? "Pas de nom"} ({wad.Section_Id}), qui ont pour délégué {wad.Delegate_Id?.ToString() ?? "Aucun"}!");
        }
    }
}
