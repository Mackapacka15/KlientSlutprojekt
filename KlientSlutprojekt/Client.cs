using System;
using RestSharp;
using System.Text.Json;
using System.Net;

namespace KlientSlutprojekt
{
    public class Client
    {
        static RestClient attendanceCLient = new RestClient("https://localhost:7129/api/");

        public IRestResponse GetRequest(string firstname, string lastname)
        {
            string name = firstname + "─" + lastname;
            PersonIn person = new PersonIn(name);
            Console.WriteLine(name);

            RestRequest getRequest = new RestRequest("Attendance");
            getRequest.AddParameter("personin", JsonSerializer.Serialize<PersonIn>(person));
            IRestResponse getResponse = attendanceCLient.Get(getRequest);
            return getResponse;
        }

    }
}