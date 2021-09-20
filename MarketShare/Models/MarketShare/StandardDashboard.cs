namespace MarketShare.Models.MarketShare
{
    /// <summary>
    /// Defines the <see cref="PartsPotentialDistributionDto" />.
    /// </summary>
    public class PartsPotentialDistributionDto
    {
        /// <summary>
        /// Gets or sets the PartdistId.
        /// </summary>
        public int PartdistId { get; set; }

        /// <summary>
        /// Gets or sets the PartdistName.
        /// </summary>
        public string PartdistName { get; set; }

        /// <summary>
        /// Gets or sets the PartdistPercentage.
        /// </summary>
        public decimal? PartdistPercentage { get; set; }

        /// <summary>
        /// Gets or sets the PartdistVIOCoverage.
        /// </summary>
        public int? PartdistVIOCoverage { get; set; }

        /// <summary>
        /// Gets or sets the PartdistCountryStr.
        /// </summary>
        public string PartdistCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartdistCurrencyStr.
        /// </summary>
        public string PartdistCurrencyStr { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartsPotentialAgeDto" />.
    /// </summary>
    public class PartsPotentialAgeDto
    {
        /// <summary>
        /// Gets or sets the PartAgeId.
        /// </summary>
        public int PartAgeId { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeDistLevel.
        /// </summary>
        public string PartAgeDistLevel { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeDistPercentage.
        /// </summary>
        public decimal? PartAgeDistPercentage { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeCountryStr.
        /// </summary>
        public string PartAgeCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeCurrencyStr.
        /// </summary>
        public string PartAgeCurrencyStr { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartsPotentialSummaryDto" />.
    /// </summary>
    public class PartsPotentialSummaryDto
    {
        /// <summary>
        /// Gets or sets the PartSummaryId.
        /// </summary>
        public int PartSummaryId { get; set; }

        /// <summary>
        /// Gets or sets the PartCategoryName.
        /// </summary>
        public string PartCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the PartDatabasePercentage.
        /// </summary>
        public decimal? PartDatabasePercentage { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketPotential.
        /// </summary>
        public int? PartMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeCountryStr.
        /// </summary>
        public string PartAgeCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartAgeCurrencyStr.
        /// </summary>
        public string PartAgeCurrencyStr { get; set; }
    }
}
