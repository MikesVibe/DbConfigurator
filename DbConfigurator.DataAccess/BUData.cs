using AutoMapper.Configuration;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace DbConfigurator.DataAccess
{
    public class BUData
    {
        public List<Area> Areas { get; private set; } = new List<Area>();
        public List<BuisnessUnit> BuisnessUnits { get; private set; } = new List<BuisnessUnit>();
        public List<Country> Countries { get; private set; } = new List<Country>();


        public BUData()
        {
            Initialize();
        }

        private void Initialize()
        {
            string diretctory = System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            
            string fileName = "DbConfigurator.DataAccess/SeedingData/CountriesData.csv";
            string combinedPath = Path.Combine(diretctory, fileName); 
            using (var reader = new StreamReader(combinedPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                        continue;

                    var temp = line.Split(';');

                    Areas.Add(new Area
                    {
                        Id = Int32.Parse(temp[0]),
                        Name = temp[1]
                    });

                    BuisnessUnits.Add(new BuisnessUnit
                    {
                        Id = Int32.Parse(temp[2]),
                        Name = temp[3]
                    });

                    Countries.Add(new Country
                    {
                        Id = Int32.Parse(temp[4]),
                        Name = temp[5],
                        ShortCode = temp[6]

                    });


                    Areas = Areas.DistinctBy(a => a.Id).ToList();
                    BuisnessUnits = BuisnessUnits.DistinctBy(a => a.Id).ToList();
                    Countries = Countries.DistinctBy(a => a.Id).ToList();
                }
            }
        }


    }


}
