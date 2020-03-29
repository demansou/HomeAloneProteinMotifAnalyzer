using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using HomeAloneBackend.Models;

namespace HomeAloneBackend.Lib
{
    public interface IFastaFileParser
    {
        IEnumerable<DataModel> ParseFile(
            IFormFile formFile);
    }

    public sealed class FastaFileParser : IFastaFileParser
    {
        private const char FASTA_FORMAT_INITIATOR = '>';

        public IEnumerable<DataModel> ParseFile(
            IFormFile formFile)
        {
            var returnList = new List<DataModel>();
            var stringBuilder = new StringBuilder();
            var model = new DataModel();

            using (var stream = formFile.OpenReadStream())
            {
                using var streamReader = new StreamReader(stream);
                while (NotEndOfFile(streamReader))
                {
                    var line = streamReader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    foreach (var cleanLine in CleanData(line))
                    {
                        if (cleanLine.StartsWith(FASTA_FORMAT_INITIATOR))
                        {
                            if (!string.IsNullOrWhiteSpace(stringBuilder.ToString()))
                            {
                                model.Data = stringBuilder.ToString();
                                returnList.Add(model);
                                stringBuilder.Clear();
                            }

                            model = new DataModel
                            {
                                Name = cleanLine
                            };
                        }
                        else
                        {
                            stringBuilder.AppendLine(cleanLine);
                        }
                    }
                }

                model.Data = stringBuilder.ToString();
                returnList.Add(model);
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
