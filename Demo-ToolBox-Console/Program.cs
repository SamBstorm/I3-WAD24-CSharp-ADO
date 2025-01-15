using Demo_ToolBox;

namespace Demo_ToolBox_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Command cmd = new Command("SELECT * FROM [student]");
            Command cmd2 = new Command("SP_Student_GetById", new Dictionary<string, object?> {
                { "student_id" , 13}
            }, true);
        }
    }
}
