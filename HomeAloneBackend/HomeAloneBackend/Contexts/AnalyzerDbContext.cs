using Microsoft.EntityFrameworkCore;

using HomeAloneBackend.Models;

namespace HomeAloneBackend.Contexts
{
    public sealed class AnalyzerDbContext : DbContext
    {
        public DbSet<DataSetModel> DataSets { get; set; }

        public DbSet<DataModel> Data { get; set; }

        public AnalyzerDbContext(DbContextOptions<AnalyzerDbContext> options)
            : base(options)
        { }
    }
}
