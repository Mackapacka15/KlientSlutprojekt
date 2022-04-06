using System;

namespace KlientSlutprojekt
{
    public class Controller
    {
        Client c = new Client();
        string firstname = "";
        string lastname = "";
        string state = "getInfo";

        public Controller()
        {
            GetUserInfo();
        }

        public void Update()
        {
            if (state == "")
            {

            }
        }

        public void GetUserInfo()
        {
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
            state = "SendGet";
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