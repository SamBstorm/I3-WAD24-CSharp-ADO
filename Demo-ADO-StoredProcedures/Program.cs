using Demo_ADO_StoredProcedures.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_ADO_StoredProcedures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue!");
            string userInput;
            do
            {
                Console.WriteLine("1. Connecter");
                Console.WriteLine("2. S'enregistrer");
                Console.WriteLine("3. Quitter");
                userInput = Console.ReadLine();
                UserService service = new UserService();
                switch (userInput) {
                    case "1":
                        Console.WriteLine("Quel est votre email ?");
                        string email = Console.ReadLine();
                        Console.WriteLine("Quel est votre mot de passe ?");
                        string pwd = Console.ReadLine();
                        if(service.Login(email, pwd))
                        {
                            Console.WriteLine($"Bonjour {email}.");
                        }
                        else
                        {
                            Console.WriteLine("Mauvaises informations.");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Quel est votre email ?");
                        string email2 = Console.ReadLine();
                        Console.WriteLine("Quel est votre mot de passe ?");
                        string pwd2 = Console.ReadLine();
                        try
                        {
                            service.Register(email2, pwd2);
                            Console.WriteLine("Vous êtes inscrit!");
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Une erreur est survenue durant l'enregistrement.");
                        }
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Veuillez choisir une option");
                        break;
                }
            } while (userInput != "3");
            Console.WriteLine("Bye bye!");
        }
    }
}
