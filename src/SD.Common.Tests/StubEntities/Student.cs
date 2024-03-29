﻿using System;

namespace SD.Common.Tests.StubEntities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public decimal? Age { get; set; }
        public DateTime BirthDay { get; set; }
        public StudentExtension Extension { get; set; }
    }
}
