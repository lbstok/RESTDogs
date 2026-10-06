namespace RESTDogs.Models
{
    public interface IDogRepository
    {
        Dog AddDog(Dog d);
        Dog? DeleteById(int id);
        IEnumerable<Dog> GetDogs(string? nameStartsWith = null, double? minWeight = null, string? sortOrder = null);
        Dog? GetById(int id);
        Dog? Update(int id, Dog data);
    }
}
