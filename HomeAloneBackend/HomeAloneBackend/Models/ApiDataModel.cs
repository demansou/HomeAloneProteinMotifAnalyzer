using HomeAloneBackend.Lib;
using Microsoft.AspNetCore.Http;

namespace HomeAloneBackend.Models
{
    public interface IApiDataModel
    {
        string CollectionName { get; }
        SequenceTypeEnum SequenceType { get; }
        IFormFile File { get; }
    }

    public sealed class ApiDataModel : IApiDataModel
    {
        public string CollectionName { get; set; }
        public SequenceTypeEnum SequenceType { get; set; }
        public IFormFile File { get; set; }
    }
}
