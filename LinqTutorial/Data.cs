using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{
    public class Data
    {
        public IEnumerable<Person> GetPeople()
        {

            yield return new Person(1, "Arek", "Pierwszy", 12);
            yield return new Person(2, "Darek", "Drugi", 13);
            yield return new Person(3, "Krzysztof", "Trzeci", 14);
            yield return new Person(4, "Marek", "Czwarty", 15);
        }

        public IEnumerable<Animal> GetAnimals()
        {

            yield return new Animal("Reksio", "Pies", new Person(1, "Arek", "Pierwszy", 12));
            yield return new Animal("Pluto", "Pies", new Person(2, "Darek", "Drugi", 13));
            yield return new Animal("Ernest", "Kot", new Person(1, "Arek", "Pierwszy", 12));
            yield return new Animal("Płotka", "Koń", new Person(2, "Darek", "Drugi", 13));
            yield return new Animal("Rafał", "Koń", new Person(4, "Marek", "Czwarty", 15));
        }
    }
}
