using webapp.Models;

namespace webapp.Storage;

public class MemCache : IStorage<Place>
{
    private object _sync = new object();
    private List<Place> _memCache = new List<Place>();
    public Place this[Guid id]
    {
        get
        {
            lock (_sync)
            {
                if (!Has(id)) throw new IncorrectPlaceException($"No Place with id {id}");

                return _memCache.Single(x => x.Id == id);
            }
        }
        set
        {
            if (id == Guid.Empty) throw new IncorrectPlaceException("Cannot request Place with an empty id");

            lock (_sync)
            {
                if (Has(id))
                {
                    RemoveAt(id);
                }

                value.Id = id;
                _memCache.Add(value);
            }
        }
    }

    public System.Collections.Generic.List<Place> All => _memCache.Select(x => x).ToList();

    public void Add(Place value)
    {
        if (value.Id != Guid.Empty) throw new IncorrectPlaceException($"Cannot add value with predefined id {value.Id}");

        value.Id = Guid.NewGuid();
        this[value.Id] = value;
    }

    public bool Has(Guid id)
    {
        return _memCache.Any(x => x.Id == id);
    }

    public void RemoveAt(Guid id)
    {
        lock (_sync)
        {
            _memCache.RemoveAll(x => x.Id == id);
        }
    }
}
