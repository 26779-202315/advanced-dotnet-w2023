using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week03_1_Reflection
{
    public class Person
    {
        public List<Name> Names { get; set; }
        static Person()
        {
        }

        public Person()
        {
            Names = new List<Name>();
            Console.WriteLine("Default Person constructor invoked.");
        }

        public Person(string name)
        {
            Names = new List<Name>();
            Console.WriteLine("Parameterized Person constructor invoked.");
        }

        public void AddName(string name)
        {
            AddName(new Name(Name.NameType.FirstName, name));
        }

        public void AddName(Name name)
        {
            Names.Add(name);
        }

        public void DoSomething()
        {
            Console.WriteLine("The do something method was invoked!");
        }
    }

    public class Name
    {
        public NameType Type { get; set; }
        [Required]
        public string Value { get; set; }
        public Name()
        {
        }

        public Name(NameType type, string value)
        {
            Type = type;
            Value = value;
        }

        public enum NameType
        {
            FirstName,
            MiddleName,
            LastName
        }

    }
}
