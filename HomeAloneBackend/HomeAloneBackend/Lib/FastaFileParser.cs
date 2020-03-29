using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using HomeAloneBackend.Models;

namespace HomeAloneBackend.Lib
{
    public interface IFastaFileParser
    {
        Task<IEnumerable<DataModel>> ParseFileAsync(CancellationToken cancellationToken, IFormFile formFile);
    }

    public sealed class FastaFileParser : IFastaFileParser
    {
        private const char FASTA_FORMAT_INITIATOR = '>';

        public async Task<IEnumerable<DataModel>> ParseFileAsync(CancellationToken cancellationToken, IFormFile formFile)
        {
            var returnList = new List<DataModel>();
            var stringBuilder = new StringBuilder();
            var model = new DataModel();

            using (var streamReader = new StreamReader(formFile.OpenReadStream()))
            {
                while (NotEndOfFile(streamReader))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    var line = await streamReader.ReadLineAsync();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    foreach (var cleanLine in CleanData(line))
                    {
                        if (cleanLine.StartsWith(FASTA_FORMAT_INITIATOR))
                        {
                            // DM 03/28/2020 The line is the protein sequence title.
                            model.Name = cleanLine;
                            model.Data = stringBuilder.ToString();
                            stringBuilder.Clear();
                            model = new DataModel();
                        }
                        else
                        {
                            // DM 03/28/2020 The line is part of the protein sequence.
                            stringBuilder.AppendLine(cleanLine);
                        }
                    }
                }
            }

            return returnList;
        }

        private bool NotEndOfFile(StreamReader streamReader)
        {
            return streamReader.Peek() >= 0;
        }

        private IEnumerable<string> CleanData(string line)
        {
            return line
                .SplitKeepDelimiters(FASTA_FORMAT_INITIATOR, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => Regex.Replace(s, @"\t|\n|\r", string.Empty))
                .Select(s => s.Trim())
                .ToList();
        }
    }
}
