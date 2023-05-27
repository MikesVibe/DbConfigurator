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
using DbConfigurator.Model.DTOs.Creation;

namespace DbConfigurator.DataAccess
{
    internal class GenericDataForTabels
    {
        public List<AreaForCreationDto> Areas { get; private set; } = new List<AreaForCreationDto>();
        public List<BuisnessUnitForCreationDto> BuisnessUnits { get; private set; } = new List<BuisnessUnitForCreationDto>();
        public List<CountryForCreationDto> Countries { get; private set; } = new List<CountryForCreationDto>();
        public List<PriorityForCreationDto> Priorities { get; private set; } = new List<PriorityForCreationDto>();


        public GenericDataForTabels()
        {
            Initialize();
        }

        private void Initialize()
        {
            Areas = new List<AreaForCreationDto>()
            {
                 new AreaForCreationDto(1, "Americas"),
                 new AreaForCreationDto(2, "Central Europe"),
                 new AreaForCreationDto(3, "Growing Markets"),
                 new AreaForCreationDto(4, "Northern Europe"),
                 new AreaForCreationDto(5, "Southern Europe"),
                 new AreaForCreationDto(99, "ANY")
            };

            BuisnessUnits = new List<BuisnessUnitForCreationDto>()
            {
                new BuisnessUnitForCreationDto( 1, "NAO"),
                new BuisnessUnitForCreationDto( 2, "SAM"),
                new BuisnessUnitForCreationDto( 3, "GER"),
                new BuisnessUnitForCreationDto( 4, "CEE"),
                new BuisnessUnitForCreationDto( 5, "MEK"),
                new BuisnessUnitForCreationDto( 6, "AFR"),
                new BuisnessUnitForCreationDto( 7, "IND"),
                new BuisnessUnitForCreationDto( 8, "APAC"),
                new BuisnessUnitForCreationDto( 9, "BTN"),
                new BuisnessUnitForCreationDto( 10, "UK&I"),
                new BuisnessUnitForCreationDto( 11, "ITA"),
                new BuisnessUnitForCreationDto( 12, "IBE"),
                new BuisnessUnitForCreationDto( 13, "FRA"),
                new BuisnessUnitForCreationDto( 99, "ANY")
            };

            Countries = new List<CountryForCreationDto>()
            {
                new CountryForCreationDto(1, "Canada", "CA"),
                new CountryForCreationDto(2, "Guatemala", "GT"),
                new CountryForCreationDto(3, "Mexico", "MX"),
                new CountryForCreationDto(4, "Puerto Rico", "PR"),
                new CountryForCreationDto(5, "USA", "US"),
                new CountryForCreationDto(6, "Argentina", "AR"),
                new CountryForCreationDto(7, "Brazil", "BR"),
                new CountryForCreationDto(8, "Chile", "CL"),
                new CountryForCreationDto(9, "Colombia", "CO"),
                new CountryForCreationDto(10, "Peru", "PE"),
                new CountryForCreationDto(11, "Uruguay", "UY"),
                new CountryForCreationDto(12, "Venezuela", "VE"),
                new CountryForCreationDto(13, "Germany", "DE"),
                new CountryForCreationDto(14, "Poland", "PL"),
                new CountryForCreationDto(15, "Russian Federation", "RU"),
                new CountryForCreationDto(16, "Austria", "AT"),
                new CountryForCreationDto(17, "Bulgaria", "BG"),
                new CountryForCreationDto(18, "Switzerland", "CH"),
                new CountryForCreationDto(19, "Cyprus", "CY"),
                new CountryForCreationDto(20, "Czech Republic", "CZ"),
                new CountryForCreationDto(21, "Greece", "GR"),
                new CountryForCreationDto(22, "Croatia", "HR"),
                new CountryForCreationDto(23, "Hungary", "HU"),
                new CountryForCreationDto(24, "Israel", "IL"),
                new CountryForCreationDto(25, "Kasakhstan", "KZ"),
                new CountryForCreationDto(26, "Romania", "RO"),
                new CountryForCreationDto(27, "Serbia", "RS"),
                new CountryForCreationDto(28, "Slovakia", "SK"),
                new CountryForCreationDto(29, "Ukraine", "UA"),
                new CountryForCreationDto(30, "United Arab Emirates", "AE"),
                new CountryForCreationDto(31, "Egypt", "EG"),
                new CountryForCreationDto(32, "Iran", "IR"),
                new CountryForCreationDto(33, "Lebanon", "LB"),
                new CountryForCreationDto(34, "Qatar", "QA"),
                new CountryForCreationDto(35, "Saudi Arabia", "SA"),
                new CountryForCreationDto(36, "Turkey", "TR"),
                new CountryForCreationDto(37, "Burkina Faso", "BF"),
                new CountryForCreationDto(38, "Benin", "BJ"),
                new CountryForCreationDto(39, "Cote d'Ivoire", "CI"),
                new CountryForCreationDto(40, "Algeria", "DZ"),
                new CountryForCreationDto(41, "Gabon", "GA"),
                new CountryForCreationDto(42, "Ivory Coast", "CI"),
                new CountryForCreationDto(43, "Morocco", "MA"),
                new CountryForCreationDto(44, "Madagascar", "MG"),
                new CountryForCreationDto(45, "Mali", "ML"),
                new CountryForCreationDto(46, "Mauritius", "MU"),
                new CountryForCreationDto(47, "Senegal", "SN"),
                new CountryForCreationDto(48, "Tunisia", "TN"),
                new CountryForCreationDto(49, "South Africa", "ZA"),
                new CountryForCreationDto(50, "India", "IN"),
                new CountryForCreationDto(51, "Australia", "AU"),
                new CountryForCreationDto(52, "People Rep China", "CN"),
                new CountryForCreationDto(53, "Hong Kong", "HK"),
                new CountryForCreationDto(54, "Indonesia", "ID"),
                new CountryForCreationDto(55, "Japan", "JP"),
                new CountryForCreationDto(56, "Korea", "KR"),
                new CountryForCreationDto(57, "Malaysia", "MY"),
                new CountryForCreationDto(58, "New Zealand", "NZ"),
                new CountryForCreationDto(59, "Philippines", "PH"),
                new CountryForCreationDto(60, "Singapore", "SG"),
                new CountryForCreationDto(61, "Thailand", "TH"),
                new CountryForCreationDto(62, "Taiwan", "TW"),
                new CountryForCreationDto(63, "Belgium", "BE"),
                new CountryForCreationDto(64, "Denmark", "DK"),
                new CountryForCreationDto(65, "Estonia", "EE"),
                new CountryForCreationDto(66, "Finland", "FI"),
                new CountryForCreationDto(67, "Lithuania", "LT"),
                new CountryForCreationDto(68, "Luxembourg", "LU"),
                new CountryForCreationDto(69, "Netherlands", "NL"),
                new CountryForCreationDto(70, "Norway", "NO"),
                new CountryForCreationDto(71, "Sweden", "SE"),
                new CountryForCreationDto(72, "United Kingdom", "GB"),
                new CountryForCreationDto(73, "Ireland", "IE"),
                new CountryForCreationDto(74, "Italy", "IT"),
                new CountryForCreationDto(75, "Andorra", "AD"),
                new CountryForCreationDto(76, "Spain", "ES"),
                new CountryForCreationDto(77, "Portugal", "PT"),
                new CountryForCreationDto(78, "France", "FR"),
                new CountryForCreationDto(79, "Morocco", "MA"),
                new CountryForCreationDto(80, "New Caledonia", "NC"),
                new CountryForCreationDto(81, "French Polynesia", "PF"),
                new CountryForCreationDto(99, "ANY", "ANY")
            };
            Priorities = new List<PriorityForCreationDto>()
            {
                new PriorityForCreationDto(1, "P1"),
                new PriorityForCreationDto(2, "P2"),
                new PriorityForCreationDto(3, "P3"),
                new PriorityForCreationDto(4, "P4"),
                new PriorityForCreationDto(99, "ANY")
            };
        }
    }
}

