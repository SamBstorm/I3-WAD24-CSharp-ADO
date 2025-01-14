using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exos_ADO.Models
{
    internal class Student
    {
        public int Student_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Login { get; set; }
        public int Section_id { get; set; }
        public int? Year_Result { get; set; }
        public string Course_Id { get; set; }

        public Student(string first_Name, string last_Name, DateTime birth_Date, int section_id)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            Birth_Date = birth_Date;
            Section_id = section_id;
            Login = (first_Name.Substring(0,1) + last_Name.Substring(0, (last_Name.Length<7)?last_Name.Length : 7)).ToLower();
            Year_Result = null;
            Course_Id = "0";
        }
    }
}
