using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Models;

namespace HomeAloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSetModelsController : ControllerBase
    {
        private readonly AnalyzerDbContext _analyzerDbContext;

        public DataSetModelsController(AnalyzerDbContext analyzerDbContext)
        {
            _analyzerDbContext = analyzerDbContext;
        }

        // GET: api/DataSetModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataSetModel>>> GetDataSets()
        {
            return await _analyzerDbContext.DataSets.ToListAsync();
        }

        // GET: api/DataSetModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataSetModel>> GetDataSetModel(int id)
        {
            var dataSetModel = await _analyzerDbContext.DataSets.FindAsync(id);

            if (dataSetModel == null)
            {
                return NotFound();
            }

            return dataSetModel;
        }
    }
}
