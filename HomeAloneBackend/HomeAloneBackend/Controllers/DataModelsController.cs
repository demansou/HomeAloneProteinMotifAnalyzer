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
    }
}
