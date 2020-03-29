using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Models;
using System.Text.RegularExpressions;

namespace HomeAloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly AnalyzerDbContext _analyzerDbContext;

        public FilterController(AnalyzerDbContext analyzerDbContext)
        {
            _analyzerDbContext = analyzerDbContext;
        }

        // POST: api/Filter
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] ApiFilterModel apiFilterModel)
        {
            var result = await Task.Run(() => _analyzerDbContext.Data
                .Where(DataModelMeetsFilterCriteria)
                .ToList());

            return new JsonResult(result);

            bool DataModelMeetsFilterCriteria(DataModel dataModel)
            {
                return dataModel.DataSetId == apiFilterModel.DataSetModelId
                    && MatchFrequencyAndRange(dataModel.Data);
            }

            bool MatchFrequencyAndRange(string data)
            {
                var pattern = $@"(.*{apiFilterModel.AaSearch}.*)";
                var matches = Regex.Matches(data, pattern, RegexOptions.IgnoreCase);

                return matches.Count >= apiFilterModel.Frequency
                    && FrequencyOccursInRange(matches);
            }

            bool FrequencyOccursInRange(MatchCollection collection)
            {
                for (int i = 0; i < collection.Count; ++i)
                {
                    for (int j = collection.Count - 1; j > i; --j)
                    {
                        if (j - i < apiFilterModel.Frequency
                            || collection[j].Index - collection[i].Index < apiFilterModel.Range)
                        {
                            break;
                        }

                        if (j - i >= apiFilterModel.Frequency
                            && collection[j].Index - collection[i].Index >= apiFilterModel.Range)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }
    }
}
