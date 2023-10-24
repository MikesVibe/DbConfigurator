using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.UnitTests.Base;
using DbConfigurator.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.UnitTests.Features.DistributionInformation
{
    public class DistributionInformationDetailViewModelTests : DetailViewModelBaseTests<IDistributionInformationService, DistributionInformationDto>
    {
        public DistributionInformationDetailViewModelTests()
            : base()
        {
            DetialViewModel = new DistributionInformationDetailViewModel(
                DataServiceMock.Object, EventAgregatorMock.Object);
        }

        protected override DistributionInformationDto CreateNewEntityDtoItem(int id)
        {
            return new DistributionInformationDto
            {
                Id = id,
                Region = new RegionDto
                {
                    Id = 1,
                    Area = new AreaDto
                    {
                        Id = 1,
                        Name = "Americas"
                    },
                    BusinessUnit = new BusinessUnitDto
                    {
                        Id = 1,
                        Name = "NAO"
                    },
                    Country = new CountryDto
                    {
                        Id = 1,
                        CountryName = "Canada",
                        CountryCode = "CA"
                    }
                },
                Priority = new PriorityDto
                {
                    Id = 1,
                    Name = "P1"
                }
            };
        }

    }
}
