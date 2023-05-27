﻿using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.DTOs.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public class CSVParser
    {
        private string _filePath;

        public CSVParser(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<RegionForParserDto> Parse()
        {
            //List<RegionForParserDto> regionsToReturn = new();
            var lines = File.ReadAllLines(_filePath);

            List<AreaBuisnessUnitForParserDto> allAreaBuisnessUnits = new();
            List<string> allAreas = new();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                if (line == null || line == String.Empty)
                    continue;

                var lineParsed = line.Split(",");

                yield return (new RegionForParserDto(lineParsed[0], lineParsed[1], lineParsed[2]));

                allAreaBuisnessUnits.Add(new AreaBuisnessUnitForParserDto(lineParsed[0], lineParsed[1]));
                allAreas.Add(lineParsed[0]);
            }
            var aBuDistinct = allAreaBuisnessUnits.Distinct(new AreaBuisnessUnitComparer());
            foreach (var aBu in aBuDistinct)
            {
                yield return (new RegionForParserDto(aBu.Area, aBu.BuisnessUnit, "ANY"));
            }

            foreach (var area in allAreas.Distinct())
            {
                yield return (new RegionForParserDto(area, "ANY", "ANY"));
            }
        }
    }
}