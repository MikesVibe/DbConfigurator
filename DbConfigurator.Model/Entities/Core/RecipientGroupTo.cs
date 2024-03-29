﻿namespace DbConfigurator.Model.Entities.Core
{
    public class RecipientGroupTo
    {
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public int DistributionInformationId { get; set; }
        public DistributionInformation RecipientGroup { get; set; }
    }
}
