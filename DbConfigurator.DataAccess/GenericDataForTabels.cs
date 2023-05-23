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
        //public List<AreaBuisnessUnitWithIdDto> AreaBuisnessUnits { get; private set; } = new List<AreaBuisnessUnitWithIdDto>();
        public List<BuisnessUnitWithIdDto> BuisnessUnits { get; private set; } = new List<BuisnessUnitWithIdDto>();
        //public List<BuisnessUnitCountryWithIdDto> BuisnessUnitCountries { get; private set; } = new List<BuisnessUnitCountryWithIdDto>();
        public List<CountryWithIdDto> Countries { get; private set; } = new List<CountryWithIdDto>();
        public List<PriorityWithIdDto> Priorities { get; private set; } = new List<PriorityWithIdDto>();


        public GenericDataForTabels()
        {
            Initialize();
        }

        private void Initialize()
        {
            Areas = new List<AreaWithIdDto>()
            {
                 new AreaWithIdDto(1, "Americas"),
                 new AreaWithIdDto(2, "Central Europe"),
                 new AreaWithIdDto(3, "Growing Markets"),
                 new AreaWithIdDto(4, "Northern Europe"),
                 new AreaWithIdDto(5, "Southern Europe"),
                 new AreaWithIdDto(99, "ANY")
            };
            //AreaBuisnessUnits = new List<AreaBuisnessUnitWithIdDto>()
            //{
            //    new AreaBuisnessUnitWithIdDto(1, 1, 1),
            //    new AreaBuisnessUnitWithIdDto(2, 1, 2),
            //    new AreaBuisnessUnitWithIdDto(3, 2, 3),
            //    new AreaBuisnessUnitWithIdDto(4, 2, 4),
            //    new AreaBuisnessUnitWithIdDto(5, 3, 5),
            //    new AreaBuisnessUnitWithIdDto(6, 3, 6),
            //    new AreaBuisnessUnitWithIdDto(7, 3, 7),
            //    new AreaBuisnessUnitWithIdDto(8, 4, 8),
            //    new AreaBuisnessUnitWithIdDto(9, 4, 9),
            //    new AreaBuisnessUnitWithIdDto(10, 4, 10),
            //    new AreaBuisnessUnitWithIdDto(11, 5, 11),
            //    new AreaBuisnessUnitWithIdDto(12, 5, 12),
            //    new AreaBuisnessUnitWithIdDto(13, 5, 13),
            //    new AreaBuisnessUnitWithIdDto(14, 1, 99),
            //    new AreaBuisnessUnitWithIdDto(15, 2, 99),
            //    new AreaBuisnessUnitWithIdDto(16, 3, 99),
            //    new AreaBuisnessUnitWithIdDto(17, 4, 99),
            //    new AreaBuisnessUnitWithIdDto(18, 5, 99),
            //    new AreaBuisnessUnitWithIdDto(19, 99, 99),

            //};
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
            //BuisnessUnitCountries = new List<BuisnessUnitCountryWithIdDto>()
            //{
            //    new BuisnessUnitCountryWithIdDto(1, 1, 1),
            //    new BuisnessUnitCountryWithIdDto(2, 1, 2),
            //    new BuisnessUnitCountryWithIdDto(3, 1, 3),
            //    new BuisnessUnitCountryWithIdDto(4, 1, 4),
            //    new BuisnessUnitCountryWithIdDto(5, 1, 5),
            //    new BuisnessUnitCountryWithIdDto(6, 2, 6),
            //    new BuisnessUnitCountryWithIdDto(7, 2, 7),
            //    new BuisnessUnitCountryWithIdDto(8, 2, 8),
            //    new BuisnessUnitCountryWithIdDto(9, 2, 9),
            //    new BuisnessUnitCountryWithIdDto(10, 2, 10),
            //    new BuisnessUnitCountryWithIdDto(11, 2, 11),
            //    new BuisnessUnitCountryWithIdDto(12, 2, 12),
            //    new BuisnessUnitCountryWithIdDto(13, 3, 13),
            //    new BuisnessUnitCountryWithIdDto(14, 4, 14),
            //    new BuisnessUnitCountryWithIdDto(15, 4, 15),
            //    new BuisnessUnitCountryWithIdDto(16, 4, 16),
            //    new BuisnessUnitCountryWithIdDto(17, 4, 17),
            //    new BuisnessUnitCountryWithIdDto(18, 4, 18),
            //    new BuisnessUnitCountryWithIdDto(19, 4, 19),
            //    new BuisnessUnitCountryWithIdDto(20, 4, 20),
            //    new BuisnessUnitCountryWithIdDto(21, 4, 21),
            //    new BuisnessUnitCountryWithIdDto(22, 4, 22),
            //    new BuisnessUnitCountryWithIdDto(23, 4, 23),
            //    new BuisnessUnitCountryWithIdDto(24, 4, 24),
            //    new BuisnessUnitCountryWithIdDto(25, 4, 25),
            //    new BuisnessUnitCountryWithIdDto(26, 4, 26),
            //    new BuisnessUnitCountryWithIdDto(27, 4, 27),
            //    new BuisnessUnitCountryWithIdDto(28, 4, 28),
            //    new BuisnessUnitCountryWithIdDto(29, 4, 29),
            //    new BuisnessUnitCountryWithIdDto(30, 5, 30),
            //    new BuisnessUnitCountryWithIdDto(31, 5, 31),
            //    new BuisnessUnitCountryWithIdDto(32, 5, 32),
            //    new BuisnessUnitCountryWithIdDto(33, 5, 33),
            //    new BuisnessUnitCountryWithIdDto(34, 5, 34),
            //    new BuisnessUnitCountryWithIdDto(35, 5, 35),
            //    new BuisnessUnitCountryWithIdDto(36, 5, 36),
            //    new BuisnessUnitCountryWithIdDto(37, 6, 37),
            //    new BuisnessUnitCountryWithIdDto(38, 6, 38),
            //    new BuisnessUnitCountryWithIdDto(39, 6, 39),
            //    new BuisnessUnitCountryWithIdDto(40, 6, 40),
            //    new BuisnessUnitCountryWithIdDto(41, 6, 41),
            //    new BuisnessUnitCountryWithIdDto(42, 6, 42),
            //    new BuisnessUnitCountryWithIdDto(43, 6, 43),
            //    new BuisnessUnitCountryWithIdDto(44, 6, 44),
            //    new BuisnessUnitCountryWithIdDto(45, 6, 45),
            //    new BuisnessUnitCountryWithIdDto(46, 6, 46),
            //    new BuisnessUnitCountryWithIdDto(47, 6, 47),
            //    new BuisnessUnitCountryWithIdDto(48, 6, 48),
            //    new BuisnessUnitCountryWithIdDto(49, 6, 49),
            //    new BuisnessUnitCountryWithIdDto(50, 7, 50),
            //    new BuisnessUnitCountryWithIdDto(51, 8, 51),
            //    new BuisnessUnitCountryWithIdDto(52, 8, 52),
            //    new BuisnessUnitCountryWithIdDto(53, 8, 53),
            //    new BuisnessUnitCountryWithIdDto(54, 8, 54),
            //    new BuisnessUnitCountryWithIdDto(55, 8, 55),
            //    new BuisnessUnitCountryWithIdDto(56, 8, 56),
            //    new BuisnessUnitCountryWithIdDto(57, 8, 57),
            //    new BuisnessUnitCountryWithIdDto(58, 8, 58),
            //    new BuisnessUnitCountryWithIdDto(59, 8, 59),
            //    new BuisnessUnitCountryWithIdDto(60, 8, 60),
            //    new BuisnessUnitCountryWithIdDto(61, 8, 61),
            //    new BuisnessUnitCountryWithIdDto(62, 8, 62),
            //    new BuisnessUnitCountryWithIdDto(63, 9, 63),
            //    new BuisnessUnitCountryWithIdDto(64, 9, 64),
            //    new BuisnessUnitCountryWithIdDto(65, 9, 65),
            //    new BuisnessUnitCountryWithIdDto(66, 9, 66),
            //    new BuisnessUnitCountryWithIdDto(67, 9, 67),
            //    new BuisnessUnitCountryWithIdDto(68, 9, 68),
            //    new BuisnessUnitCountryWithIdDto(69, 9, 69),
            //    new BuisnessUnitCountryWithIdDto(70, 9, 70),
            //    new BuisnessUnitCountryWithIdDto(71, 9, 71),
            //    new BuisnessUnitCountryWithIdDto(72, 10, 72),
            //    new BuisnessUnitCountryWithIdDto(73, 10, 73),
            //    new BuisnessUnitCountryWithIdDto(74, 11, 74),
            //    new BuisnessUnitCountryWithIdDto(75, 12, 75),
            //    new BuisnessUnitCountryWithIdDto(76, 12, 76),
            //    new BuisnessUnitCountryWithIdDto(77, 12, 77),
            //    new BuisnessUnitCountryWithIdDto(78, 13, 78),
            //    new BuisnessUnitCountryWithIdDto(79, 13, 79),
            //    new BuisnessUnitCountryWithIdDto(80, 13, 80),
            //    new BuisnessUnitCountryWithIdDto(81, 13, 81),
            //    new BuisnessUnitCountryWithIdDto(82, 1, 99),
            //    new BuisnessUnitCountryWithIdDto(83, 2, 99),
            //    new BuisnessUnitCountryWithIdDto(84, 3, 99),
            //    new BuisnessUnitCountryWithIdDto(85, 4, 99),
            //    new BuisnessUnitCountryWithIdDto(86, 5, 99),
            //    new BuisnessUnitCountryWithIdDto(87, 6, 99),
            //    new BuisnessUnitCountryWithIdDto(88, 7, 99),
            //    new BuisnessUnitCountryWithIdDto(89, 8, 99),
            //    new BuisnessUnitCountryWithIdDto(90, 9, 99),
            //    new BuisnessUnitCountryWithIdDto(91, 10, 99),
            //    new BuisnessUnitCountryWithIdDto(92, 11, 99),
            //    new BuisnessUnitCountryWithIdDto(93, 12, 99),
            //    new BuisnessUnitCountryWithIdDto(94, 13, 99),
            //    new BuisnessUnitCountryWithIdDto(95, 99, 99)
            //};
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

