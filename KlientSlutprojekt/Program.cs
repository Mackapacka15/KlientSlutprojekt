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
            string firstname = null;
            string lastname = null;

            Console.WriteLine("What's your firstname?");
            firstname = Console.ReadLine();

            Console.WriteLine("What's your lastname?");
            lastname = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname))
            {
                if (string.IsNullOrWhiteSpace(firstname))
                {
                    Console.WriteLine("That's not a name");
                    Console.WriteLine("What's your firstname?");
                    firstname = Console.ReadLine();
                }
                if (string.IsNullOrWhiteSpace(lastname))
                {
                    Console.WriteLine("That's not a lastname");
                    Console.WriteLine("What's your lastname?");
                    lastname = Console.ReadLine();
                }
            }

            string name = firstname + "─" + lastname;

            RestRequest getRequest = new RestRequest("Attendance", Method.GET);
            getRequest.AddParameter("name", name);
            IRestResponse getResponse = attendanceCLient.Get(getRequest);

            if (getResponse.IsSuccessful)
            {
                Console.WriteLine("Välkommen!");
                Console.WriteLine(getResponse.Content);
            }
            else
            {
                Console.WriteLine("Du fanns inte med i listan. Vill du lägga till dig själv i listan?");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "ja")
                {
                    RestRequest putRequest = new RestRequest("Attendance", Method.POST) { RequestFormat = DataFormat.Json };
                    Person value = new Person(name);
                    putRequest.AddBody(value);
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
    }
}
