using System;

namespace KlientSlutprojekt
{
    public class PersonIn
    {
        public string Name { get; set; }

        private string key = "hejsansvejsan";

        public PersonIn(string name)
        {
            Name = name;
        }
    }

}