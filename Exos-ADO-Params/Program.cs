using Exos_ADO_Params.Models;
using Microsoft.Data.SqlClient;

namespace Exos_ADO_Params
{
    internal class Program
    {
        const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBSlide;Integrated Security=True;Encrypt=False";
        static void Main(string[] args)
        {
            Student student = new Student("Nicole", "Lenoir", new DateTime(1975, 1, 1), 2425);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "INSERT INTO [student] ([first_name],[last_name],[birth_date],[login],[section_id],[year_result],[course_id]) OUTPUT [inserted].[student_id] VALUES (@first_name, @last_name, @birth_date, @login, @section_id, @year_result, @course_id)";

                    SqlParameter p_first_name = new SqlParameter()
                    {
                        ParameterName = "first_name",
                        Value = (object?)student.First_Name ?? DBNull.Value
                    };
                    command.Parameters.Add(p_first_name);

                    command.Parameters.AddWithValue("last_name", (object?)student.Last_Name ?? DBNull.Value);
                    command.Parameters.AddWithValue("birth_date", (object?)student.Birth_Date ?? DBNull.Value);
                    command.Parameters.AddWithValue("login", (object?)student.Login ?? DBNull.Value);
                    command.Parameters.AddWithValue("section_id", (object?)student.Section_Id ?? DBNull.Value);
                    command.Parameters.AddWithValue("year_result", (object?)student.Year_Result ?? DBNull.Value);
                    command.Parameters.AddWithValue("course_id", student.Course_Id);

                    connection.Open();
                    student.Student_Id = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }

            Console.WriteLine($"L'étudiante {student.First_Name} {student.Last_Name} a bien été inscrie avec l'identifiant {student.Student_Id}!");
        }
    }
}
