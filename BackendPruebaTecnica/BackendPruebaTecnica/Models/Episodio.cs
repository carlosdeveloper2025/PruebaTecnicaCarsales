namespace BackendPruebaTecnica.Models
{
    public class Episodio
    {
        public string name { get; set; }
        public string air_date { get; set; }
        public string episode { get; set; }
        public List<string> characters { get; set; }
        public string url { get; set; }
        public string created { get; set; }

    }

    public class PagedResponse<T>
    {
        public object Info { get; init; }    // Puedes definir tipo Info si quieres
        public IEnumerable<T> Results { get; init; } = Enumerable.Empty<T>();
    }
}
