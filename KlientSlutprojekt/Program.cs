using System;
using RestSharp;
using System.Text.Json;
using System.Net;


namespace KlientSlutprojekt
{
    class Program
    {
        static void Main(string[] args)
        {

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            RestClient attendanceCLient = new RestClient("https://localhost:7129/api/");
            string firstname = "";
            string lastname = "";

            Console.WriteLine("What's your firstname?");
            firstname = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstname) || HasNonLetters(firstname))
            {
                Console.WriteLine("That's not a name");
                Console.WriteLine("What's your firstname?");
                firstname = Console.ReadLine();
            }
            Console.WriteLine("What's your lastname?");
            lastname = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastname) || HasNonLetters(lastname))
            {

                Console.WriteLine("That's not a lastname");
                Console.WriteLine("What's your lastname?");
                lastname = Console.ReadLine();
            }

            string name = firstname + "─" + lastname;
            PersonIn person = new PersonIn(name);
            Console.WriteLine(name);

            RestRequest getRequest = new RestRequest("Attendance");
            getRequest.AddParameter("personin", JsonSerializer.Serialize<PersonIn>(person));
            IRestResponse getResponse = attendanceCLient.Get(getRequest);

            if (getResponse.IsSuccessful)
            {
                Console.WriteLine("Välkommen!");
                Console.WriteLine(getResponse.Content);
            }
            else if ((int)getResponse.StatusCode == 400)
            {
                Console.WriteLine(getResponse.Content);
            }
            else
            {
                Console.WriteLine("Du fanns inte med i listan. Vill du lägga till dig själv i listan?");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "ja")
                {
                    RestRequest putRequest = new RestRequest("Attendance", Method.POST) { RequestFormat = DataFormat.Json };
                    putRequest.AddBody(person);
                    IRestResponse putResponse = attendanceCLient.Post(putRequest);
                    if (putResponse.IsSuccessful)
                    {
                        Console.WriteLine("Grattis du är nu med i listan");
                    }
                    else
                    {
                        Console.WriteLine(putResponse.Content);
                    }
                }
                else
                {
                    Console.WriteLine("Du läggs inte in i listan");
                }
            }

            Console.ReadLine();
        }
        public static bool HasNonLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return true;
            }
            return false;
        }
    }
}
