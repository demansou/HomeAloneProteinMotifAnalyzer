using System.Collections.Generic;

using HomeAloneBackend.Lib;

namespace HomeAloneBackend.Models
{
    public interface IDataSetModel
    {
        int Id { get; }
        string Name { get; }
        SequenceTypeEnum SequenceType { get; }

        List<DataModel> Data { get; }
    }

    public sealed class DataSetModel : IDataSetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SequenceTypeEnum SequenceType { get; set; }

        public List<DataModel> Data { get; set; }
    }
}
