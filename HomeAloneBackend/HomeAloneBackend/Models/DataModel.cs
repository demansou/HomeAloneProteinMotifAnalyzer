namespace HomeAloneBackend.Models
{
    public interface IDataModel
    {
        int Id { get; }
        string Data { get; }

        int DataSetId { get; }
        DataSetModel DataSet { get; }
    }

    public sealed class DataModel : IDataModel
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public int DataSetId { get; set; }
        public DataSetModel DataSet { get; set; }
    }
}
