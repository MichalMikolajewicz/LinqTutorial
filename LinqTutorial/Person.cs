using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{
    public class Person : IEquatable<Person>
    {
        public Person(int id, string name, string lastName, int age)
        {
            ID = id;
            Name = name;
            LastName = lastName;
            Age = age;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            return this.Name == other.Name;
        }
    }
}
