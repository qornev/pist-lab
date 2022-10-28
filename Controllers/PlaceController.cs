using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private static List<Place> _memCache = new List<Place>();

    [HttpGet]
    public ActionResult<IEnumerable<Place>> Get()
    {
        return _memCache;
    }

    [HttpGet("{id}")]
    public ActionResult<Place> Get(int id)
    {
        if (_memCache.Count <= id) throw new IndexOutOfRangeException("Такого места пока нет");

        return _memCache[id];
    }

    [HttpPost]
    public void Post([FromBody] Place value)
    {
        _memCache.Add(value);
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Place value)
    {
        if (_memCache.Count <= id) throw new IndexOutOfRangeException("Такого места пока нет");

        _memCache[id] = value;
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        if (_memCache.Count <= 10) throw new IndexOutOfRangeException("Такого места пока нет");

        _memCache.RemoveAt(id);
    }
}
