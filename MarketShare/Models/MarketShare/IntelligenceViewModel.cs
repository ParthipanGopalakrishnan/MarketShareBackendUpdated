namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IntelligenceDto" />.
    /// </summary>
    public class IntelligenceDto
    {
        /// <summary>
        /// Gets or sets the IntelligenceID.
        /// </summary>
        public long? IntelligenceID { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCountryStr.
        /// </summary>
        public string IntelligenceCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCountry.
        /// </summary>
        public string IntelligenceCountry { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCurrencyCode.
        /// </summary>
        public string IntelligenceCurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the IntelligencCustomerPartId.
        /// </summary>
        public long? IntelligencCustomerPartId { get; set; }

        /// <summary>
        /// Gets or sets the IntelligencePartNo.
        /// </summary>
        public string IntelligencePartNo { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceDescription.
        /// </summary>
        public string IntelligenceDescription { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCategory.
        /// </summary>
        public string IntelligenceCategory { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceSubCategory.
        /// </summary>
        public string IntelligenceSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceVehicleName.
        /// </summary>
        public string IntelligenceVehicleName { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceListPrice.
        /// </summary>
        public decimal? IntelligenceListPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceACPOnlinePrice.
        /// </summary>
        public decimal? IntelligenceACPOnlinePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceNETPrice.
        /// </summary>
        public decimal? IntelligenceNETPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCorePrice.
        /// </summary>
        public decimal? IntelligenceCorePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceInstallerPrice.
        /// </summary>
        public decimal? IntelligenceInstallerPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMAvgListPrice.
        /// </summary>
        public decimal? IntelligenceOEMAvgListPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMAvgACPOnlinePrice.
        /// </summary>
        public decimal? IntelligenceOEMAvgACPOnlinePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMAvgNETPrice.
        /// </summary>
        public decimal? IntelligenceOEMAvgNETPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMAvgCorePrice.
        /// </summary>
        public decimal? IntelligenceOEMAvgCorePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMAvgInstallerPrice.
        /// </summary>
        public decimal? IntelligenceOEMAvgInstallerPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMAvgListPrice.
        /// </summary>
        public decimal? IntelligenceIAMAvgListPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMAvgACPOnlinePrice.
        /// </summary>
        public decimal? IntelligenceIAMAvgACPOnlinePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMAvgNETPrice.
        /// </summary>
        public decimal? IntelligenceIAMAvgNETPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMAvgCorePrice.
        /// </summary>
        public decimal? IntelligenceIAMAvgCorePrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMAvgInstallerPrice.
        /// </summary>
        public decimal? IntelligenceIAMAvgInstallerPrice { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceVIODemand.
        /// </summary>
        public int? IntelligenceVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceMarketPotential.
        /// </summary>
        public int? IntelligenceMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceMarketShare.
        /// </summary>
        public decimal? IntelligenceMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCustomer12MSales.
        /// </summary>
        public decimal? IntelligenceCustomer12MSales { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceOEMPriceTrend.
        /// </summary>
        public decimal? IntelligenceOEMPriceTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceIAMPriceTrend.
        /// </summary>
        public decimal? IntelligenceIAMPriceTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceVIODemandTrend.
        /// </summary>
        public decimal? IntelligenceVIODemandTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceMarketPotentialTrend.
        /// </summary>
        public decimal? IntelligenceMarketPotentialTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceMarketShareTrend.
        /// </summary>
        public decimal? IntelligenceMarketShareTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCustomer12MSalesTrend.
        /// </summary>
        public decimal? IntelligenceCustomer12MSalesTrend { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCurrencyStr.
        /// </summary>
        public string IntelligenceCurrencyStr { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="IntelligenceFilterDto" />.
    /// </summary>
    public class IntelligenceFilterDto
    {
        /// <summary>
        /// Gets or sets the IntelligenceSubCategory.
        /// </summary>
        public List<string> IntelligenceSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the IntelligencePartNo.
        /// </summary>
        public List<string> IntelligencePartNo { get; set; }

        /// <summary>
        /// Gets or sets the IntelligenceCountry.
        /// </summary>
        public List<string> IntelligenceCountry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntelligenceFilterDto"/> class.
        /// </summary>
        public IntelligenceFilterDto()
        {
            IntelligenceSubCategory = new List<string>();
            IntelligencePartNo = new List<string>();
            IntelligenceCountry = new List<string>();
        }
    }
    public class IntelligenceCategoryDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public List<string> CategoryName { get; set; }
    }
    public class IntelligenceGridDataDto
    {
        /// <summary>
        /// Gets or sets the totalCount.
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public IEnumerable<IntelligenceDto> data { get; set; }
    }
}
