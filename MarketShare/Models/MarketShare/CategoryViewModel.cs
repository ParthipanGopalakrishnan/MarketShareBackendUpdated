namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CategoryViewDto" />.
    /// </summary>
    public class CategoryViewDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryPartNumber.
        /// </summary>
        public string CategoryPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the CategoryVIODemand.
        /// </summary>
        public int? CategoryVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketPotential.
        /// </summary>
        public int? CategoryMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCustomerSales.
        /// </summary>
        public decimal? CategoryCustomerSales { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketShare.
        /// </summary>
        public decimal? CategoryMarketShare { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryTotalDto" />.
    /// </summary>
    public class CategoryTotalDto
    {
        /// <summary>
        /// Gets or sets the CategoryVIODemand.
        /// </summary>
        public int? CategoryVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketPotential.
        /// </summary>
        public int? CategoryMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCustomerSales.
        /// </summary>
        public decimal? CategoryCustomerSales { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketShare.
        /// </summary>
        public decimal? CategoryMarketShare { get; set; }

        /// <summary>
        /// Gets or sets the CategoryVehicle.
        /// </summary>
        public long? CategoryVehicle { get; set; }

        /// <summary>
        /// Gets or sets the CateogryPartCount.
        /// </summary>
        public long? CateogryPartCount { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryModelDto" />.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryModel" />.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public List<string> CategoryName { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryGridDataDto" />.
    /// </summary>
    public class CategoryGridDataDto
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
        public IEnumerable<CategoryModelDto> data { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryModelDto" />.
    /// </summary>
    public class CategoryModelDto
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryPartNumber.
        /// </summary>
        public string CategoryPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the CategoryVIODemand.
        /// </summary>
        public int? CategoryVIODemand { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMarketPotential.
        /// </summary>
        public int? CategoryMarketPotential { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCustomerSales.
        /// </summary>
        public decimal? CategoryCustomerSales { get; set; }

        /// <summary>
        /// Gets or sets the CategoryYear.
        /// </summary>
        public int? CategoryYear { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMake.
        /// </summary>
        public string CategoryMake { get; set; }

        /// <summary>
        /// Gets or sets the CategoryModel.
        /// </summary>
        public string CategoryModel { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubModel.
        /// </summary>
        public string CategorySubModel { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCustomerDistribution.
        /// </summary>
        public decimal? CategoryCustomerDistribution { get; set; }
    }
}
