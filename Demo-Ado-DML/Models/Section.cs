using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Ado_DML.Models
{
    internal class Section
    {
        public int Section_Id { get; set; }
        public string? Section_Name { get; set; }
        public int? Delegate_Id { get; set; }
    }
}
