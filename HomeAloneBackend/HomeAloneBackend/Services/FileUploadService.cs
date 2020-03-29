using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Models;
using BackgroundWorker;
using HomeAloneBackend.Lib;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAloneBackend.Services
{
    public interface IFileUploadService
    {
        void Save(IApiDataModel apiDataModel);
    }

    public sealed class FileUploadService : IFileUploadService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IFastaFileParser _fastaFileParser;
        private readonly ILogger<IFileUploadService> _logger;

        public FileUploadService(
            IServiceScopeFactory serviceScopeFactory,
            IBackgroundTaskQueue backgroundTaskQueue,
            IFastaFileParser fastaFileParser,
            ILogger<IFileUploadService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _backgroundTaskQueue = backgroundTaskQueue;
            _fastaFileParser = fastaFileParser;
            _logger = logger;
        }

        public void Save(IApiDataModel apiDataModel)
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
