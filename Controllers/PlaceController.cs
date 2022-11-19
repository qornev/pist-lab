using Microsoft.AspNetCore.Mvc;
using webapp.Storage;
using webapp.Models;

namespace webapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private IStorage<Place> _memCache = new MemCache();

    public PlaceController(IStorage<Place> memCache)
    {
        _memCache = memCache;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Place>> Get()
    {
        return Ok(_memCache.All);
    }

    [HttpGet("{id}")]
    public ActionResult<Place> Get(Guid id)
    {
        if (!_memCache.Has(id)) return NotFound("No such");

        return Ok(_memCache[id]);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Place value)
    {
        var validationResult = value.Validate();

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        _memCache.Add(value);

        return Ok($"{value.ToString()} has been added");
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Place value)
    {
        if (!_memCache.Has(id)) return NotFound("No such");

        var validationResult = value.Validate();

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var previousValue = _memCache[id];
        _memCache[id] = value;

        return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (_memCache.Has(id)) return NotFound("No such");

        var valueToRemove = _memCache[id];
        _memCache.RemoveAt(id);

        return Ok($"{valueToRemove.ToString()} has been removed");
    }
}
