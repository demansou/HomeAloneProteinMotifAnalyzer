using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Models;
using BackgroundWorker;
using HomeAloneBackend.Lib;

namespace HomeAloneBackend.Services
{
    public interface IFileUploadService
    {
        void Save(IApiDataModel apiDataModel);
    }

    public sealed class FileUploadService : IFileUploadService
    {
        private readonly AnalyzerDbContext _analyzerDbContext;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IFastaFileParser _fastaFileParser;
        private readonly ILogger<IFileUploadService> _logger;

        public FileUploadService(
            AnalyzerDbContext analyzerDbContext,
            IBackgroundTaskQueue backgroundTaskQueue,
            IFastaFileParser fastaFileParser,
            ILogger<IFileUploadService> logger)
        {
            _analyzerDbContext = analyzerDbContext;
            _backgroundTaskQueue = backgroundTaskQueue;
            _fastaFileParser = fastaFileParser;
            _logger = logger;
        }

        public void Save(IApiDataModel apiDataModel)
        {
            _backgroundTaskQueue.QueueBackgroundWorkItem(async cancellationToken =>
            {
                var newDataSetModelId = await InsertDataSetModelAsync(cancellationToken, apiDataModel);

                await InsertDataFromModelAsync(cancellationToken, apiDataModel, newDataSetModelId);
            });

            async Task<int> InsertDataSetModelAsync(CancellationToken ct, IApiDataModel m)
            {
                var newDataSetModel = new DataSetModel
                {
                    Name = m.CollectionName,
                    SequenceType = m.SequenceType,
                };

                await _analyzerDbContext.DataSets.AddAsync(newDataSetModel);
                await _analyzerDbContext.SaveChangesAsync(ct);

                return newDataSetModel.Id;
            }

            async Task InsertDataFromModelAsync(CancellationToken ct, IApiDataModel m, int id)
            {
                try
                {
                    foreach (var newDataModel in await _fastaFileParser.ParseFileAsync(ct, m.File))
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
}
