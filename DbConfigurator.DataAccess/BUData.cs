using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace DbConfigurator.DataAccess
{
    public partial class DbConfiguratorDbContext
    {
        public class BUData
        {
            public BUData()
            {
                Area = new List<string>();
                CountryCluster = new List<string>();
                CountryCode = new List<string>();
                Country = new List<string>();
            }
            public void Add(string area, string countryCluster, string countryCode, string country)
            {
                this.Area.Add(area);
                this.CountryCluster.Add(countryCluster);
                this.CountryCode.Add(countryCode);
                this.Country.Add(country);
            }

            public List<string> Area { get; }
            public List<string> CountryCluster { get; }
            public List<string> CountryCode { get; }
            public List<string> Country { get; }


            public BUData GetBUData()
            {
                string fileName = "./SeedingData/CountrysData.csv";

                BUData bUDatas = new BUData();
                using (var reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (line == null)
                            continue;

                        var temp = line.Split(';');

                        bUDatas.Add(temp[0], temp[1], temp[2], temp[3]);
                    }
                }

                return bUDatas;
                //return new BUData();
            }
        }
    }
}
