namespace RESTDogs.Models
{
    public class DogRepository
    {
        private List<Dog> _dogs = new List<Dog>();
        private int nextId = 1;
        public DogRepository()
        {
            AddDog(new Dog { Name = "Dog 1", Weight = 10.5 });
            AddDog(new Dog { Name = "Dog 2", Weight = 5.2 });
            AddDog(new Dog { Name = "Dog 3", Weight = 7.8 });
            AddDog(new Dog { Name = "Dog 4", Weight = 12.3 });
        }


        public IEnumerable<Dog> GetDogs(
        string? nameStartsWith = null,
        double? minWeight = null,
        string? sortOrder = null)
        {
            IEnumerable<Dog> result = _dogs;

            if (!string.IsNullOrWhiteSpace(nameStartsWith))
            {
                result = result.Where(d =>
                    d.Name != null &&
                    d.Name.StartsWith(
                        nameStartsWith,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (minWeight.HasValue)
            {
                result = result.Where(d => d.Weight >= minWeight.Value);
            }

            result = sortOrder?.ToLowerInvariant() switch
            {
                "name" or "nameasc" => result.OrderBy(d => d.Name),
                "namedesc" => result.OrderByDescending(d => d.Name),
                "weight" or "weightasc" => result.OrderBy(d => d.Weight),
                "weightdesc" => result.OrderByDescending(d => d.Weight),
                _ => result
            };

            return result;
        }

        public Dog AddDog(Dog d)
        {
            d.Id = nextId++;
            _dogs.Add(d);
            return d;
        }
        public Dog? GetById(int id)
        {
            return _dogs.FirstOrDefault(d => d.Id == id);
        }
        public Dog? DeleteById(int id)
        {
            Dog? dog = GetById(id);
            if (dog != null)
            {
                _dogs.Remove(dog);
            }
            return dog;
        }

        public Dog? Update(int id, Dog data)
        {
            Dog? dog = GetById(id);
            if (dog != null)
            {
                dog.Weight = data.Weight;
                dog.Name = data.Name;

            }
            return dog;
        }
    }
}
