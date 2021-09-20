namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PartModelDto" />.
    /// </summary>
    public class PartModelDto
    {
        /// <summary>
        /// Gets or sets the PartId.
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        /// Gets or sets the PartCategoryName.
        /// </summary>
        public string PartCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the PartNumber.
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// Gets or sets the PartYear.
        /// </summary>
        public int? PartYear { get; set; }

        /// <summary>
        /// Gets or sets the PartMake.
        /// </summary>
        public string PartMake { get; set; }

        /// <summary>
        /// Gets or sets the PartModel.
        /// </summary>
        public string PartModel { get; set; }

        /// <summary>
        /// Gets or sets the PartSubModel.
        /// </summary>
        public string PartSubModel { get; set; }

        /// <summary>
        /// Gets or sets the PartVIODemand.
        /// </summary>
        public int? PartVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketPotential.
        /// </summary>
        public int? PartMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketShare.
        /// </summary>
        public decimal? PartMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the PartSalesDistribution.
        /// </summary>
        public decimal? PartSalesDistribution { get; set; }

        /// <summary>
        /// Gets or sets the PartCountryStr.
        /// </summary>
        public string PartCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the PartCurrencyStr.
        /// </summary>
        public string PartCurrencyStr { get; set; }
        public int? PartCustSales { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartFilterDto" />.
    /// </summary>
    public class PartFilterDto
    {
        /// <summary>
        /// Gets or sets the PartNumber.
        /// </summary>
        public string PartNumber { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartCategoryDto" />.
    /// </summary>
    public class PartCategoryDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public List<string> CategoryName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartCategoryFilterdto" />.
    /// </summary>
    public class PartCategoryFilterdto
    {
        /// <summary>
        /// Gets or sets the PartNumber.
        /// </summary>
        public List<string> PartNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartCategoryFilterdto"/> class.
        /// </summary>
        public PartCategoryFilterdto()
        {
            PartNumber = new List<string>();
        }
    }

    /// <summary>
    /// Defines the <see cref="PartTotalDto" />.
    /// </summary>
    public class PartTotalDto
    {
        /// <summary>
        /// Gets or sets the PartVIODemand.
        /// </summary>
        public int? PartVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketPotential.
        /// </summary>
        public int? PartMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the PartCustomerSales.
        /// </summary>
        public decimal? PartCustomerSales { get; set; }

        /// <summary>
        /// Gets or sets the PartMarketShare.
        /// </summary>
        public decimal? PartMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the PartVehicleCount.
        /// </summary>
        public int? PartVehicleCount { get; set; }

        /// <summary>
        /// Gets or sets the PartPartCount.
        /// </summary>
        public long? PartPartCount { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartGridDataDto" />.
    /// </summary>
    public class PartGridDataDto
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
        public IEnumerable<PartModelDto> data { get; set; }
    }
}
