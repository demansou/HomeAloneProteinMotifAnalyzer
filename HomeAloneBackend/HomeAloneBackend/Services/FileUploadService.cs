using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IFastaFileParser _fastaFileParser;
        private readonly ILogger<IFileUploadService> _logger;

        public FileUploadService(
            AnalyzerDbContext analyzerDbContext,
            IServiceScopeFactory serviceScopeFactory,
            IFastaFileParser fastaFileParser,
            ILogger<IFileUploadService> logger)
        {
            _analyzerDbContext = analyzerDbContext;
            _serviceScopeFactory = serviceScopeFactory;
            _fastaFileParser = fastaFileParser;
            _logger = logger;
        }

        public async Task SaveAsync(IApiDataModel apiDataModel)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var analyzerDbContext = scope.ServiceProvider.GetRequiredService<AnalyzerDbContext>();

            var newDataSetModelId = InsertDataSetModel(
                apiDataModel,
                analyzerDbContext);

            InsertDataFromModel(
                apiDataModel,
                newDataSetModelId,
                analyzerDbContext);
            
            int InsertDataSetModel(IApiDataModel m, AnalyzerDbContext c)
            {
                var newDataSetModel = new DataSetModel
                {
                    Name = m.CollectionName,
                    SequenceType = m.SequenceType,
                };

                c.DataSets.Add(newDataSetModel);
                c.SaveChanges();

                return newDataSetModel.Id;
            }

            void InsertDataFromModel(IApiDataModel m, int id, AnalyzerDbContext c)
            {
                try
                {
                    foreach (var newDataModel in _fastaFileParser.ParseFile(m.File))
                    {
                        newDataModel.DataSetId = id;
                        c.Data.Add(newDataModel);
                    }

                    c.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
