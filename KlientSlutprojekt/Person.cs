using System;

namespace KlientSlutprojekt
{
    public class PersonIn
    {
        public string Name { get; set; }

        public string Key { get; set; } = "hejsansvejsan";

        public PersonIn(string name)
        {
            Name = name;
        }
    }

}