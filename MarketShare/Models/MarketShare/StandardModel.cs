namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="StandardGlobalDto" />.
    /// </summary>
    public class StandardGlobalDto
    {
        /// <summary>
        /// Gets or sets the GlobalId.
        /// </summary>
        public int GlobalId { get; set; }

        /// <summary>
        /// Gets or sets the GlobalCategory.
        /// </summary>
        public string GlobalCategory { get; set; }

        /// <summary>
        /// Gets or sets the GlobalMake.
        /// </summary>
        public string GlobalMake { get; set; }

        /// <summary>
        /// Gets or sets the GlobalYear.
        /// </summary>
        public int? GlobalYear { get; set; }

        /// <summary>
        /// Gets or sets the GlobalModel.
        /// </summary>
        public string GlobalModel { get; set; }

        /// <summary>
        /// Gets or sets the GlobalVIODemand.
        /// </summary>
        public long? GlobalVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the GlobalMarketPotential.
        /// </summary>
        public long? GlobalMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the GlobalCountryStr.
        /// </summary>
        public string GlobalCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the GlobalCurrencyStr.
        /// </summary>
        public string GlobalCurrencyStr { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="StandardAgeDistDto" />.
    /// </summary>
    public class StandardAgeDistDto
    {
        /// <summary>
        /// Gets or sets the AgeDistId.
        /// </summary>
        public int AgeDistId { get; set; }

        /// <summary>
        /// Gets or sets the AgeDistYear.
        /// </summary>
        public int? AgeDistYear { get; set; }

        /// <summary>
        /// Gets or sets the AgeDistVIODemand.
        /// </summary>
        public long? AgeDistVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the AgeDistMarketPotential.
        /// </summary>
        public long? AgeDistMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the AgeDistCountryStr.
        /// </summary>
        public string AgeDistCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the AgeDistCurrencyStr.
        /// </summary>
        public string AgeDistCurrencyStr { get; set; }

        /// <summary>
        /// Gets or sets the AgeCategory.
        /// </summary>
        public string AgeCategory { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="StandardCategoryFilterdto" />.
    /// </summary>
    public class StandardCategoryFilterdto
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
        /// Initializes a new instance of the <see cref="StandardCategoryFilterdto"/> class.
        /// </summary>
        public StandardCategoryFilterdto()
        {
            CategoryYearfilter = new List<int?>();
            CategoryMakefilter = new List<string>();
            CategoryModelfilter = new List<string>();
        }
    }

    /// <summary>
    /// Defines the <see cref="StandardGridDataDto" />.
    /// </summary>
    public class StandardGridDataDto
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
        public IEnumerable<StandardGlobalDto> data { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="StandardChartDetails" />.
    /// </summary>
    public class StandardChartDetails
    {
        /// <summary>
        /// Gets or sets the JAgeDistCategory.
        /// </summary>
        public List<string> JAgeDistCategory { get; set; }

        /// <summary>
        /// Gets or sets the JAgeDistYear.
        /// </summary>
        public List<int?> JAgeDistYear { get; set; }

        /// <summary>
        /// Gets or sets the JAgeMarketPotential.
        /// </summary>
        public List<int?> JAgeMarketPotential { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardChartDetails"/> class.
        /// </summary>
        public StandardChartDetails()
        {
            JAgeDistCategory = new List<string>();
            JAgeDistYear = new List<int?>();
            JAgeMarketPotential = new List<int?>();
        }
    }

    /// <summary>
    /// Defines the <see cref="StandardCategoryDto" />.
    /// </summary>
    public class StandardCategoryDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public List<string> CategoryName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="StandardTotalDto" />.
    /// </summary>
    public class StandardTotalDto
    {
        /// <summary>
        /// Gets or sets the CategoryVIODemand.
        /// </summary>
        public long? CategoryVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketPotential.
        /// </summary>
        public long? CategoryMarketPotential { get; set; }
    }
}
