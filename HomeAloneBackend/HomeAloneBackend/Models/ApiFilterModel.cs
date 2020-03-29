namespace HomeAloneBackend.Models
{
    public interface IApiFilterModel
    {
        int DataSetModelId { get; }
        string AaSearch { get; }
        int Range { get; }
        int Frequency { get; }
    }

    public sealed class ApiFilterModel : IApiFilterModel
    {
        public int DataSetModelId { get; set; }
        public string AaSearch { get; set; }
        public int Range { get; set; }
        public int Frequency { get; set; }
    }
}
