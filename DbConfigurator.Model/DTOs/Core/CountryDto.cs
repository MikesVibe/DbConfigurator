﻿namespace DbConfigurator.Model.DTOs.Core
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }
}