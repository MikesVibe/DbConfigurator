using AutoMapper.Configuration;
using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.Models;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Input;

namespace DbConfigurator.DataAccess
{
    internal class GenericDataForTabels
    {
        public List<AreaWithIdDto> Areas { get; private set; } = new List<AreaWithIdDto>();
        public List<BuisnessUnitWithIdDto> BuisnessUnits { get; private set; } = new List<BuisnessUnitWithIdDto>();
        public List<CountryWithIdDto> Countries { get; private set; } = new List<CountryWithIdDto>();
        public List<PriorityWithIdDto> Priorities { get; private set; } = new List<PriorityWithIdDto>();


        public GenericDataForTabels()
        {
            Initialize();
        }

        private void Initialize()
        {
            Countries = new List<CountryWithIdDto>()
            {
                new CountryWithIdDto(1, "Canada", "CA"),
                new CountryWithIdDto(2, "Guatemala", "GT"),
                new CountryWithIdDto(3, "Mexico", "MX"),
                new CountryWithIdDto(4, "Puerto Rico", "PR"),
                new CountryWithIdDto(5, "USA", "US"),
                new CountryWithIdDto(6, "Argentina", "AR"),
                new CountryWithIdDto(7, "Brazil", "BR"),
                new CountryWithIdDto(8, "Chile", "CL"),
                new CountryWithIdDto(9, "Colombia", "CO"),
                new CountryWithIdDto(10, "Peru", "PE"),
                new CountryWithIdDto(11, "Uruguay", "UY"),
                new CountryWithIdDto(12, "Venezuela", "VE"),
                new CountryWithIdDto(13, "Germany", "DE"),
                new CountryWithIdDto(14, "Poland", "PL"),
                new CountryWithIdDto(15, "Russian Federation", "RU"),
                new CountryWithIdDto(16, "Austria", "AT"),
                new CountryWithIdDto(17, "Bulgaria", "BG"),
                new CountryWithIdDto(18, "Switzerland", "CH"),
                new CountryWithIdDto(19, "Cyprus", "CY"),
                new CountryWithIdDto(20, "Czech Republic", "CZ"),
                new CountryWithIdDto(21, "Greece", "GR"),
                new CountryWithIdDto(22, "Croatia", "HR"),
                new CountryWithIdDto(23, "Hungary", "HU"),
                new CountryWithIdDto(24, "Israel", "IL"),
                new CountryWithIdDto(25, "Kasakhstan", "KZ"),
                new CountryWithIdDto(26, "Romania", "RO"),
                new CountryWithIdDto(27, "Serbia", "RS"),
                new CountryWithIdDto(28, "Slovakia", "SK"),
                new CountryWithIdDto(29, "Ukraine", "UA"),
                new CountryWithIdDto(30, "United Arab Emirates", "AE"),
                new CountryWithIdDto(31, "Egypt", "EG"),
                new CountryWithIdDto(32, "Iran", "IR"),
                new CountryWithIdDto(33, "Lebanon", "LB"),
                new CountryWithIdDto(34, "Qatar", "QA"),
                new CountryWithIdDto(35, "Saudi Arabia", "SA"),
                new CountryWithIdDto(36, "Turkey", "TR"),
                new CountryWithIdDto(37, "Burkina Faso", "BF"),
                new CountryWithIdDto(38, "Benin", "BJ"),
                new CountryWithIdDto(39, "Cote d'Ivoire", "CI"),
                new CountryWithIdDto(40, "Algeria", "DZ"),
                new CountryWithIdDto(41, "Gabon", "GA"),
                new CountryWithIdDto(42, "Ivory Coast", "CI"),
                new CountryWithIdDto(43, "Morocco", "MA"),
                new CountryWithIdDto(44, "Madagascar", "MG"),
                new CountryWithIdDto(45, "Mali", "ML"),
                new CountryWithIdDto(46, "Mauritius", "MU"),
                new CountryWithIdDto(47, "Senegal", "SN"),
                new CountryWithIdDto(48, "Tunisia", "TN"),
                new CountryWithIdDto(49, "South Africa", "ZA"),
                new CountryWithIdDto(50, "India", "IN"),
                new CountryWithIdDto(51, "Australia", "AU"),
                new CountryWithIdDto(52, "People Rep China", "CN"),
                new CountryWithIdDto(53, "Hong Kong", "HK"),
                new CountryWithIdDto(54, "Indonesia", "ID"),
                new CountryWithIdDto(55, "Japan", "JP"),
                new CountryWithIdDto(56, "Korea", "KR"),
                new CountryWithIdDto(57, "Malaysia", "MY"),
                new CountryWithIdDto(58, "New Zealand", "NZ"),
                new CountryWithIdDto(59, "Philippines", "PH"),
                new CountryWithIdDto(60, "Singapore", "SG"),
                new CountryWithIdDto(61, "Thailand", "TH"),
                new CountryWithIdDto(62, "Taiwan", "TW"),
                new CountryWithIdDto(63, "Belgium", "BE"),
                new CountryWithIdDto(64, "Denmark", "DK"),
                new CountryWithIdDto(65, "Estonia", "EE"),
                new CountryWithIdDto(66, "Finland", "FI"),
                new CountryWithIdDto(67, "Lithuania", "LT"),
                new CountryWithIdDto(68, "Luxembourg", "LU"),
                new CountryWithIdDto(69, "Netherlands", "NL"),
                new CountryWithIdDto(70, "Norway", "NO"),
                new CountryWithIdDto(71, "Sweden", "SE"),
                new CountryWithIdDto(72, "United Kingdom", "GB"),
                new CountryWithIdDto(73, "Ireland", "IE"),
                new CountryWithIdDto(74, "Italy", "IT"),
                new CountryWithIdDto(75, "Andorra", "AD"),
                new CountryWithIdDto(76, "Spain", "ES"),
                new CountryWithIdDto(77, "Portugal", "PT"),
                new CountryWithIdDto(78, "France", "FR"),
                new CountryWithIdDto(79, "Morocco", "MA"),
                new CountryWithIdDto(80, "New Caledonia", "NC"),
                new CountryWithIdDto(81, "French Polynesia", "PF"),
                new CountryWithIdDto(99, "ANY", "ANY")
            };

            BuisnessUnits = new List<BuisnessUnitWithIdDto>()
            {
                new BuisnessUnitWithIdDto( 1, "NAO"),
                new BuisnessUnitWithIdDto( 2, "SAM"),
                new BuisnessUnitWithIdDto( 3, "GER"),
                new BuisnessUnitWithIdDto( 4, "CEE"),
                new BuisnessUnitWithIdDto( 5, "MEK"),
                new BuisnessUnitWithIdDto( 6, "AFR"),
                new BuisnessUnitWithIdDto( 7, "IND"),
                new BuisnessUnitWithIdDto( 8, "APAC"),
                new BuisnessUnitWithIdDto( 9, "BTN"),
                new BuisnessUnitWithIdDto( 10, "UK&I"),
                new BuisnessUnitWithIdDto( 11, "ITA"),
                new BuisnessUnitWithIdDto( 12, "IBE"),
                new BuisnessUnitWithIdDto( 13, "FRA"),
                new BuisnessUnitWithIdDto( 99, "ANY")
            };

            Areas = new List<AreaWithIdDto>()
            {
                 new AreaWithIdDto(1, "Americas"),
                 new AreaWithIdDto(2, "Central Europe"),
                 new AreaWithIdDto(3, "Growing Markets"),
                 new AreaWithIdDto(4, "Northern Europe"),
                 new AreaWithIdDto(5, "Southern Europe"),
                 new AreaWithIdDto(99, "ANY")
            };

            Priorities = new List<PriorityWithIdDto>()
            {
                new PriorityWithIdDto(1, "P1"),
                new PriorityWithIdDto(2, "P2"),
                new PriorityWithIdDto(3, "P3"),
                new PriorityWithIdDto(4, "P4"),
                new PriorityWithIdDto(99, "ANY")
            };
        }
    }
}

