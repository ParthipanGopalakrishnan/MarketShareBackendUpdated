namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ModelViewDto" />.
    /// </summary>
    public class ModelViewDto
    {
        /// <summary>
        /// Gets or sets the ModelId.
        /// </summary>
        public int ModelId { get; set; }

        /// <summary>
        /// Gets or sets the ModelCategoryName.
        /// </summary>
        public string ModelCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the ModelNumber.
        /// </summary>
        public string ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the ModelYear.
        /// </summary>
        public int? ModelYear { get; set; }

        /// <summary>
        /// Gets or sets the ModelMake.
        /// </summary>
        public string ModelMake { get; set; }

        /// <summary>
        /// Gets or sets the ModelModel.
        /// </summary>
        public string ModelModel { get; set; }

        /// <summary>
        /// Gets or sets the ModelSubModel.
        /// </summary>
        public string ModelSubModel { get; set; }

        /// <summary>
        /// Gets or sets the ModelVIODemand.
        /// </summary>
        public int? ModelVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the ModelMarketPotential.
        /// </summary>
        public int? ModelMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the ModelMarketShare.
        /// </summary>
        public decimal? ModelMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the ModelSalesDistribution.
        /// </summary>
        public decimal? ModelSalesDistribution { get; set; }

        /// <summary>
        /// Gets or sets the ModelCountryStr.
        /// </summary>
        public string ModelCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the ModelCurrencyStr.
        /// </summary>
        public string ModelCurrencyStr { get; set; }
        public string ModelPartNumber { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ModelFilterDto" />.
    /// </summary>
    public class ModelCategoryFilterDto
    {
        /// <summary>
        /// Gets or sets the CategoryYearfilter.
        /// </summary>
        public List<int?> CategoryYearfilter { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMakefilter.
        /// </summary>
        public List<string> CategoryMakefilter { get; set; }

        /// <summary>
        /// Gets or sets the CategoryModelfilter.
        /// </summary>
        public List<string> CategoryModelfilter { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubModelfilter
        /// Initializes a new instance of the <see cref="StandardCategoryFilterdto"/> class....
        /// </summary>
        public List<string> CategorySubModelfilter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelCategoryFilterDto"/> class.
        /// </summary>
        public ModelCategoryFilterDto()
        {
            CategoryYearfilter = new List<int?>();
            CategoryMakefilter = new List<string>();
            CategoryModelfilter = new List<string>();
            CategorySubModelfilter = new List<string>();
        }
    }

    /// <summary>
    /// Defines the <see cref="ModelCategoryDto" />.
    /// </summary>
    public class ModelCategoryDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public List<string> CategoryName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ModelTotalDto" />.
    /// </summary>
    public class ModelTotalDto
    {
        /// <summary>
        /// Gets or sets the ModelVIODemand.
        /// </summary>
        public int? ModelVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the ModelMarketPotential.
        /// </summary>
        public int? ModelMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the ModelMinMarketShare.
        /// </summary>
        public decimal? ModelMinMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the ModelPartCount.
        /// </summary>
        public int? ModelPartCount { get; set; }

        /// <summary>
        /// Gets or sets the ModelMaxMarketShare.
        /// </summary>
        public decimal? ModelMaxMarketShare { get; set; }
    }
    public class ModelGridDataDto
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
        public IEnumerable<ModelViewDto> data { get; set; }
    }
}
