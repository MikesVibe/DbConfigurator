﻿namespace  DbConfigurator.Core.Entities
{
    public class RecipientGroupCc
    {
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public int DistributionInformationId { get; set; }
        public DistributionInformation RecipientGroup { get; set; }
    }
}
