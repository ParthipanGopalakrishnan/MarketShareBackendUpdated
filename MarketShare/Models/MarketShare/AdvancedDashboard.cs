namespace MarketShare.Models.MarketShare
{
    /// <summary>
    /// Defines the <see cref="AdvancedDistributionDto" />.
    /// </summary>
    public class AdvancedDistributionDto
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
    /// Defines the <see cref="AdvancedPotentialAgeDto" />.
    /// </summary>
    public class AdvancedPotentialAgeDto
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
    /// Defines the <see cref="AdvancedPotentialSummaryDto" />.
    /// </summary>
    public class AdvancedPotentialSummaryDto
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
        /// Gets or sets the PartCount.
        /// </summary>
        public int? PartCount { get; set; }

        /// <summary>
        /// Gets or sets the RevCoverage.
        /// </summary>
        public decimal? RevCoverage { get; set; }

        /// <summary>
        /// Gets or sets the MarketSharePercentage.
        /// </summary>
        public decimal? MarketSharePercentage { get; set; }

        /// <summary>
        /// Gets or sets the PartCountryStr.
        /// </summary>
        public string PartCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartCurrencyStr.
        /// </summary>
        public string PartCurrencyStr { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="AdvanceSumTotalDto" />.
    /// </summary>
    public class AdvanceSumTotalDto
    {
        /// <summary>
        /// Gets or sets the PartSumId.
        /// </summary>
        public int PartSumId { get; set; }

        /// <summary>
        /// Gets or sets the PartDashboard.
        /// </summary>
        public string PartDashboard { get; set; }

        /// <summary>
        /// Gets or sets the PartVIOCoverage.
        /// </summary>
        public int? PartVIOCoverage { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketPotential.
        /// </summary>
        public int? PartMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the PartGroup.
        /// </summary>
        public int? PartGroup { get; set; }

        /// <summary>
        /// Gets or sets the PartNumber.
        /// </summary>
        public int? PartNumber { get; set; }

        /// <summary>
        /// Gets or sets the PartVehicleCount.
        /// </summary>
        public int? PartVehicleCount { get; set; }

        /// <summary>
        /// Gets or sets the PartRevCoverage.
        /// </summary>
        public decimal? PartRevCoverage { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketSharePercentage.
        /// </summary>
        public decimal? PartMarketSharePercentage { get; set; }

        /// <summary>
        /// Gets or sets the PartCountryStr.
        /// </summary>
        public string PartCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartCurrencyStr.
        /// </summary>
        public string PartCurrencyStr { get; set; }
    }
}
