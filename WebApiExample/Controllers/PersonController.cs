using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;

namespace WebApiExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private static List<Person> _people = new List<Person>();

    public PersonController()
    {
        if(_people.Count == 0)
        {
            _people.Add(new Person() { Id = 1, FirstName = "James", LastName = "Sprayberry" });
            _people.Add(new Person() { Id = 2, FirstName = "Jason", LastName = "Mosley" });
            _people.Add(new Person() { Id = 3, FirstName = "Jennifer", LastName = "Dietz" });
            _people.Add(new Person() { Id = 4, FirstName = "Bessie", LastName = "Duppstadt" });
        }
    }

    [HttpGet]
    [Produces("application/xml")]
    //[Produces("application/json")]
    public List<Person> GetAll()
    {
        return _people;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Person>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> GetPersonById(int id)
    {
        var person = _people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }


    [HttpPut("{id}")]
    [ProducesResponseType<Person>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Person>> GetPersonById([System.Web.Http.FromUri]int id, [FromBody] Person person)
    {
        var personToUpdate = _people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        personToUpdate.FirstName = person.FirstName;
        personToUpdate.LastName = person.LastName;

        return Ok(person);
    }
}