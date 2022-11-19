using Newtonsoft.Json;
using webapp.Models;

namespace webapp.Storage;

public class FileStorage : MemCache, IStorage<Place>
{
    private Timer _timer;

    public string FileName { get; }
    public int FlushPeriod { get; }

    public FileStorage(string fileName, int flushPeriod)
    {
        FileName = fileName;
        FlushPeriod = flushPeriod;

        Load();

        _timer = new Timer((x) => Flush(), null, flushPeriod, flushPeriod);
    }

    private void Load()
    {
        if (File.Exists(FileName))
        {
            var allLines = File.ReadAllText(FileName);


            try
            {
                var deserialized = JsonConvert.DeserializeObject<List<Place>>(allLines);

                if (deserialized != null)
                {
                    foreach (var place in deserialized)
                    {
                        base[place.Id] = place;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException($"Cannot load data from file {FileName}:\r\n{ex.Message}");
            }
        }
    }

    private void Flush()
    {
        var serializedContents = JsonConvert.SerializeObject(All);

        File.WriteAllText(FileName, serializedContents);
    }
}