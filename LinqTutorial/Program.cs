using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Data();
            var people = data.GetPeople();
            var animals = data.GetAnimals();
            var intList = Enumerable.Range(1, 100);
            #region Select / SelectMany
            //names
            var namesLambda = people.Select(x => x.Name);
            var namesQuery = from person in people
                                   select person;
            //names and age
            var namesAndAgesLambda = people.Select(x => new { x.Name, x.Age });
            var namesAndAgesQuery = from person in people
                                          select new { person.Name, person.Age };

            //lastName letters flat list
            var lastNameLettersLambda = people.SelectMany(x => x.LastName);
            var lastNameLettersQuery = from person in people
                                       from letter in person.LastName
                                       select letter;
            #endregion

            #region Where / First / FirstOrDefault / Single / SingleOrDefault
            var namesContainsLetterLowerCaseALambda = people.Where(x => x.Name.Contains('a')).Select(x => x);
            var namesContainsLetterLowerCaseAQuery = from person in people
                                                     where person.Name.Contains('a')
                                                     select person;
            var firstAnimal = animals.First();
            var firstAnimalFilter = animals.First(x => x.Type == "Kot");

            var lastAnimal = animals.Last();

            #endregion

            #region GroupBy / Join / GroupJoin / Aggregate
            var groupByLambda = animals.GroupBy(x => x.Owner)
                .Select(x => new
                {
                    name = x.Key.Name,
                    pets = x.Select(y => y.Name)
                });

            var groupByJoinLambda = people.GroupJoin(animals, Person => Person,
                animal => animal.Owner, (person, animalCollection) =>
                 new
                 {
                     ownerName = person.Name,
                     animalNames = animalCollection.Select(x => x.Name)
                 });
            var sumAggregate = intList.Aggregate((x, y) => x + y);
            var sum = intList.Sum();
            #endregion

            #region Skip / Take / Any / All / Order

            var skipTakeLambda = intList.Skip(10).Take(10).Skip(5).Take(5);
            var skip = intList.Skip(Math.Max(0, intList.Count() - 10));

            var any = intList.Any(x => x > 100);
            var all = intList.All(x => x > -1);

            var descendingLambda = intList.OrderByDescending(x => x);
            #endregion

            #region Expressions

            var xParam = Expression.Parameter(typeof(Person), "x");
            var nameProperty = Expression.Property(xParam, "Name");
            Expression<Func<Person, string>> expressionFunc = Expression.Lambda<Func<Person, string>>(nameProperty, xParam);
            var expressionResult = people.Select(expressionFunc.Compile());

            var expressionCall = Expression.Call(typeof(Enumerable), "Select", new[] { typeof(Person), typeof(string) },
                Expression.Constant(people), expressionFunc);
            var expressionResultHarder = Expression.Lambda(expressionCall).Compile().DynamicInvoke();

            #endregion

            #region exercises
            //na podstawie intList napisz listę list, która zawiera dla danego n wszystkie wielokrotności z danego przedziału
            
            #region solution
            var multiple = intList.Select(x => intList.Where(y => y % x == 0)).ToList();
            #endregion

            //napisz funkcje, która na podstawie intList zgrupujesz elementy po 2 tj. na wyjściu lista list, gdzie wewnętrzna ma 2 elementy. {{1,2},{3,4}, {...}}. Rozwiązanie powinno w łatwy sposób
            //umożliwić zmianę liczby elementów zgrupowanych. Podpowiedź, Select( (x,y) => new {x, y}) y to index.

            #region solution
            var batch = intList.Select((item, inx) => new { item, inx })
                    .GroupBy(x => x.inx / 2)
                    .Select(g => g.Select(x => x.item));
            #endregion

            //napisz funkcje, która ustawi liczby parzystę najpierw w kolejności rosnącej następnie nieparzyste w malejącej
            //sprawdź funkcje Union
            #region solution
            var sortEven = intList.Where(x => x % 2 == 0).OrderBy(x => x);
            var sortUneven = intList.Where(x => x % 2 != 0).OrderByDescending(x => x);
            var resultSort = sortEven.Union(sortUneven);

            #endregion

            //Zadanie domowe, napiszcie własny filtr za pomocą Expression
            #endregion
        }
    }
}
