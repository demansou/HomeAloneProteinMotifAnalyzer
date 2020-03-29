using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Models;
using HomeAloneBackend.Lib;

namespace HomeAloneBackend.Services
{
    public interface IFileUploadService
    {
        Task SaveAsync(IApiDataModel apiDataModel);
    }

    public sealed class FileUploadService : IFileUploadService
    {
        private readonly AnalyzerDbContext _analyzerDbContext;
        private readonly IFastaFileParser _fastaFileParser;
        private readonly ILogger<IFileUploadService> _logger;

        public FileUploadService(
            AnalyzerDbContext analyzerDbContext,
            IFastaFileParser fastaFileParser,
            ILogger<IFileUploadService> logger)
        {
            _analyzerDbContext = analyzerDbContext;
            _fastaFileParser = fastaFileParser;
            _logger = logger;
        }

        public async Task SaveAsync(IApiDataModel apiDataModel)
        {
            var newDataSetModelId = await InsertDataSetModelAsync(apiDataModel);

            await InsertDataFromModelAsync(apiDataModel, newDataSetModelId);
        }

        private async Task<int> InsertDataSetModelAsync(IApiDataModel m)
        {
            var newDataSetModel = new DataSetModel
            {
                Name = m.CollectionName,
                SequenceType = m.SequenceType,
            };

            await _analyzerDbContext.DataSets.AddAsync(newDataSetModel);
            await _analyzerDbContext.SaveChangesAsync();

            return newDataSetModel.Id;
        }

        private async Task InsertDataFromModelAsync(IApiDataModel m, int id)
        {
            try
            {
                foreach (var newDataModel in _fastaFileParser.ParseFile(m.File))
                {
                    newDataModel.DataSetId = id;
                    await _analyzerDbContext.Data.AddAsync(newDataModel);
                }

                await _analyzerDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
