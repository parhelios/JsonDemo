using System.Text.Json;
using JsonDemo;

var animals = new List<Animal>();

animals.Add(new Dog() { Name = "Zoe" });
animals.Add(new Cat() { Name = "Tussan" });

var json = "[";

foreach (var animal in animals)
{
    if (animal is Dog dog)
    {
        Console.WriteLine(dog.Name);
        json += JsonSerializer.Serialize(dog);
    }
    else if (animal is Cat cat)
    {
        Console.WriteLine(cat.Name);
        json += JsonSerializer.Serialize(cat);

    }

    if (animals[^1] != animal)
    {
        json += ",";
    }

    //if (animals.IndexOf(animal) < animals.Count - 1) // if (animals[^1] != animal)
}

json += "]";

//var json = JsonSerializer.Serialize(animals);
//Console.WriteLine(json);

////går inte att instansera Animal, som är en abstrakt klass
////behöver göra en liknande foreach loop som ovan för att avgöra vilken underklass till den abstrakta klassen som ska skapas
//var animalsTest = JsonSerializer.Deserialize<List<Animal>>(json);

//var json = JsonSerializer.Serialize((object)animals, new JsonSerializerOptions() { WriteIndented = true });
Console.WriteLine(json);

var deserialisedAnimal = new List<Animal>();
using (var jsonDoc = JsonDocument.Parse(json))
{
    if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
    {
        foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
        {
            //initierar ett objekt av typen animal
            //och lägger sedan till i katter eller hundar, respektive
            //om det är en hund eller en katt
            Animal a;
            switch (jsonElement.GetProperty("Type").GetString())
            {
                case nameof(Dog):
                    a = jsonElement.Deserialize<Dog>();
                    deserialisedAnimal.Add(a);
                    break;
                case nameof(Cat):
                    a = jsonElement.Deserialize<Cat>();
                    deserialisedAnimal.Add(a);
                    break;
            }
        }
    }

}

foreach (var animal in deserialisedAnimal)
{
    Console.WriteLine(animal.GetType());
    Console.WriteLine(animal.Type);

}

foreach (var animal in deserialisedAnimal)
{
    if (animal is Dog dog)
    {
        Console.WriteLine(dog.Name);
    }
    else if (animal is Cat cat)
    {
        Console.WriteLine(cat.Name);
    }
}