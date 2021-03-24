using System;

namespace SD.Toolkits.WebApi.Core.Tests.Models
{
    public class Person
    {
        public string Name { get; set; }
        public double Age { get; set; }
        public DateTime Birthday { get; set; }
    }

    public enum Gender
    {
        Male = 0,

        Female = 1
    }
}
