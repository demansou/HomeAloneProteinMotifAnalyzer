using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // PUT: api/DataSetModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSetModel(int id, DataSetModel dataSetModel)
        {
            if (id != dataSetModel.Id)
            {
                return BadRequest();
            }

            _analyzerDbContext.Entry(dataSetModel).State = EntityState.Modified;

            try
            {
                await _analyzerDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSetModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DataSetModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataSetModel>> PostDataSetModel(DataSetModel dataSetModel)
        {
            _analyzerDbContext.DataSets.Add(dataSetModel);
            await _analyzerDbContext.SaveChangesAsync();

            return CreatedAtAction("GetDataSetModel", new { id = dataSetModel.Id }, dataSetModel);
        }

        // DELETE: api/DataSetModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataSetModel>> DeleteDataSetModel(int id)
        {
            var dataSetModel = await _analyzerDbContext.DataSets.FindAsync(id);
            if (dataSetModel == null)
            {
                return NotFound();
            }

            _analyzerDbContext.DataSets.Remove(dataSetModel);
            await _analyzerDbContext.SaveChangesAsync();

            return dataSetModel;
        }

        private bool DataSetModelExists(int id)
        {
            return _analyzerDbContext.DataSets.Any(e => e.Id == id);
        }
    }
}
