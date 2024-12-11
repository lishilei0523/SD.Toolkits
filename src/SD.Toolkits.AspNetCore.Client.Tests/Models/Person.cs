using System;

namespace SD.Toolkits.AspNetCore.Client.Tests.Models
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string name, double age, DateTime birthday)
        {
            this.Name = name;
            this.Age = age;
            this.Birthday = birthday;
        }

        public string Name { get; set; }

        public double Age { get; set; }

        public DateTime Birthday { get; set; }
    }
}
