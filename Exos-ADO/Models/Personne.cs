using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exos_ADO.Models
{
    internal class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Personne(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }
    }
}
