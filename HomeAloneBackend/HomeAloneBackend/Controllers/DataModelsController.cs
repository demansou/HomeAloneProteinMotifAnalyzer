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
    public class DataModelsController : ControllerBase
    {
        private readonly AnalyzerDbContext _context;

        public DataModelsController(AnalyzerDbContext context)
        {
            _context = context;
        }

        // GET: api/DataModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModel>>> GetData()
        {
            return await _context.Data.ToListAsync();
        }

        // GET: api/DataModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModel>> GetDataModel(int id)
        {
            var dataModel = await _context.Data.FindAsync(id);

            if (dataModel == null)
            {
                return NotFound();
            }

            return dataModel;
        }

        // PUT: api/DataModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataModel(int id, DataModel dataModel)
        {
            if (id != dataModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataModelExists(id))
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

        // POST: api/DataModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataModel>> PostDataModel(DataModel dataModel)
        {
            _context.Data.Add(dataModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataModel", new { id = dataModel.Id }, dataModel);
        }

        // DELETE: api/DataModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModel>> DeleteDataModel(int id)
        {
            var dataModel = await _context.Data.FindAsync(id);
            if (dataModel == null)
            {
                return NotFound();
            }

            _context.Data.Remove(dataModel);
            await _context.SaveChangesAsync();

            return dataModel;
        }

        private bool DataModelExists(int id)
        {
            return _context.Data.Any(e => e.Id == id);
        }
    }
}
