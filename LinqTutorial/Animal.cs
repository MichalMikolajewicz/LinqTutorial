using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{
    public class Animal
    {
        public Animal(string name, string type, Person owner)
        {
            Name = name;
            Type = type;
            Owner = owner;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public Person Owner { get; set; }
    }
}
