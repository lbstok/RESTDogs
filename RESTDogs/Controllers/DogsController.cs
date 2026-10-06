using Microsoft.AspNetCore.Mvc;
using RESTDogs.Models;

namespace RESTDogs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogsController : ControllerBase
{
    private readonly DogRepository _repository;

    public DogsController(DogRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Dog>> GetAll(
        [FromQuery] string? nameStartsWith,
        [FromQuery] double? minWeight,
        [FromQuery] string? sortOrder)
    {
        return Ok(_repository.GetDogs(nameStartsWith, minWeight, sortOrder));
    }

    [HttpGet("{id:int}")]
    public ActionResult<Dog> GetById(int id)
    {
        var dog = _repository.GetById(id);

        if (dog is null)
        {
            return NotFound();
        }

        return Ok(dog);
    }

    [HttpPost]
    public ActionResult<Dog> Create(Dog dog)
    {
        if (string.IsNullOrWhiteSpace(dog.Name))
        {
            return BadRequest("Name is required.");
        }

        if (dog.Weight <= 0)
        {
            return BadRequest("Weight must be greater than zero.");
        }

        var createdDog = _repository.AddDog(dog);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdDog.Id },
            createdDog);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Dog> Update(int id, Dog dog)
    {
        if (string.IsNullOrWhiteSpace(dog.Name))
        {
            return BadRequest("Name is required.");
        }

        if (dog.Weight <= 0)
        {
            return BadRequest("Weight must be greater than zero.");
        }

        var updatedDog = _repository.Update(id, dog);

        if (updatedDog is null)
        {
            return NotFound();
        }

        return Ok(updatedDog);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deletedDog = _repository.DeleteById(id);

        if (deletedDog is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}