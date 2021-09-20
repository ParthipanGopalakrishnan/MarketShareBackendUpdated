namespace MarketShare.Models.MarketShare
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="VehicleFilterDto" />.
    /// </summary>
    public class VehicleFilterDto
    {
        /// <summary>
        /// Gets or sets the VehicleYearfilter.
        /// </summary>
        public List<int?> VehicleYearfilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleMakefilter.
        /// </summary>
        public List<string> VehicleMakefilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleModelfilter.
        /// </summary>
        public List<string> VehicleModelfilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleEnginefilter.
        /// </summary>
        public List<string> VehicleEnginefilter { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleFilter" />.
    /// </summary>
    public class VehicleFilter
    {
        /// <summary>
        /// Gets or sets the VehicleYearfilter.
        /// </summary>
        public int? VehicleYearfilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleMakefilter.
        /// </summary>
        public List<string> VehicleMakefilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleModelfilter.
        /// </summary>
        public List<string> VehicleModelfilter { get; set; }

        /// <summary>
        /// Gets or sets the VehicleEnginefilter.
        /// </summary>
        public List<string> VehicleEnginefilter { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleViewDto" />.
    /// </summary>
    public class WhiDataDto
    {       
        /// <summary>
        /// Gets or sets the WhiPartNumber.
        /// </summary>
        public string WhiPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the WhiCustomerPartNumber.
        /// </summary>
        public string WhiCustomerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the WhiDescription.
        /// </summary>
        public string WhiDescription { get; set; }

        /// <summary>
        /// Gets or sets the WhiCategory.
        /// </summary>
        public string WhiCategory { get; set; }

        /// <summary>
        /// Gets or sets the WhiSubCategory.
        /// </summary>
        public string WhiSubCategory { get; set; }

        /// <summary>
        /// Gets or sets the WhiPartTerminology.
        /// </summary>
        public string WhiPartTerminology { get; set; }

        /// <summary>
        /// Gets or sets the WhiYearID.
        /// </summary>
        public string WhiYearID { get; set; }

        /// <summary>
        /// Gets or sets the WhiMake.
        /// </summary>
        public string WhiMake { get; set; }

        /// <summary>
        /// Gets or sets the WhiModel.
        /// </summary>
        public string WhiModel { get; set; }

        /// <summary>
        /// Gets or sets the WhiEngine.
        /// </summary>
        public string WhiEngine { get; set; }

        /// <summary>
        /// Gets or sets the WhiBrand.
        /// </summary>
        public string WhiBrand { get; set; }

        /// <summary>
        /// Gets or sets the WhiBrand.
        /// </summary>
        public string WhiDistributor { get; set; }

        /// <summary>
        /// Gets or sets the WhiAvgPrice.
        /// </summary>
        public Nullable<decimal> WhiAvgPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiAvgCorePrice.
        /// </summary>
        public Nullable<decimal> WhiAvgCorePrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiMedianPrice.
        /// </summary>
        public Nullable<decimal> WhiMedianPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiMinPrice.
        /// </summary>
        public Nullable<decimal> WhiMinPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiMaxPrice.
        /// </summary>
        public Nullable<decimal> WhiMaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiStandardDeviationPrice.
        /// </summary>
        public Nullable<decimal> WhiStandardDeviationPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiVariancePrice.
        /// </summary>
        public Nullable<decimal> WhiVariancePrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiSumPrice.
        /// </summary>
        public Nullable<decimal> WhiSumPrice { get; set; }

        /// <summary>
        /// Gets or sets the WhiTransactionCount.
        /// </summary>
        public Nullable<int> WhiTransactionCount { get; set; }

        /// <summary>
        /// Gets or sets the WhiPriceDate.
        /// </summary>
        public DateTime? WhiPriceDate { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleGridDataDto" />.
    /// </summary>
    public class WhiViewDto
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
        public IEnumerable<WhiDataDto> data { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleYearDto" />.
    /// </summary>
    public class VehicleYearDto
    {
        /// <summary>
        /// Gets or sets the VehicleYear.
        /// </summary>
        public int? VehicleYear { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleYearFilterDto" />.
    /// </summary>
    public class VehicleYearFilterDto
    {
        /// <summary>
        /// Gets or sets the VehicleYear.
        /// </summary>
        public List<int?> VehicleYear { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleMakeDto" />.
    /// </summary>
    public class VehicleMakeDto
    {
        /// <summary>
        /// Gets or sets the VehicleMake.
        /// </summary>
        public string VehicleMake { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleMakeFilterDto" />.
    /// </summary>
    public class VehicleMakeFilterDto
    {
        /// <summary>
        /// Gets or sets the VehicleMake.
        /// </summary>
        public List<string> VehicleMake { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleModelDto" />.
    /// </summary>
    public class VehicleModelDto
    {
        /// <summary>
        /// Gets or sets the VehicleModel.
        /// </summary>
        public string VehicleModel { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleModelFilterDto" />.
    /// </summary>
    public class VehicleModelFilterDto
    {
        /// <summary>
        /// Gets or sets the VehicleModel.
        /// </summary>
        public List<string> VehicleModel { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleEngineDto" />.
    /// </summary>
    public class VehicleEngineDto
    {
        /// <summary>
        /// Gets or sets the VehicleEngine.
        /// </summary>
        public string VehicleEngine { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleEngineFilterDto" />.
    /// </summary>
    public class VehicleEngineFilterDto
    {
        /// <summary>
        /// Gets or sets the VehicleEngine.
        /// </summary>
        public List<string> VehicleEngine { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="BrandDto" />.
    /// </summary>
    public class BrandDto
    {
        /// <summary>
        /// Gets or sets the Brands.
        /// </summary>
        public string Brands { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="VehicleYearFilterDto" />.
    /// </summary>
    public class BrandFilterDto
    {
        /// <summary>
        /// Gets or sets the Brands.
        /// </summary>
        public List<string> Brands { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryDataDto" />.
    /// </summary>
    public class CategoryDataDto
    {
        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        public string Category { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryFilterDto" />.
    /// </summary>
    public class CategoryFilterDto
    {
        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        public List<string> Category { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="SubCategoryDataDto" />.
    /// </summary>
    public class SubCategoryDataDto
    {
        /// <summary>
        /// Gets or sets the SubCategory.
        /// </summary>
        public string SubCategory { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="SubCategoryFilterDto" />.
    /// </summary>
    public class SubCategoryFilterDto
    {
        /// <summary>
        /// Gets or sets the SubCategory.
        /// </summary>
        public List<string> SubCategory { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartTerminologyDataDto" />.
    /// </summary>
    public class PartTerminologyDataDto
    {
        /// <summary>
        /// Gets or sets the PartTerminology.
        /// </summary>
        public string PartTerminology { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="PartTerminologyFilterDto" />.
    /// </summary>
    public class PartTerminologyFilterDto
    {
        /// <summary>
        /// Gets or sets the PartTerminology.
        /// </summary>
        public List<string> PartTerminology { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryBrandDataDto" />.
    /// </summary>
    public class CategoryBrandDataDto
    {
        /// <summary>
        /// Gets or sets the CategoryBrand.
        /// </summary>
        public string CategoryBrand { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="CategoryBrandFilterDto" />.
    /// </summary>
    public class CategoryBrandFilterDto
    {
        /// <summary>
        /// Gets or sets the CategoryBrand.
        /// </summary>
        public List<string> CategoryBrand { get; set; }
    }
}
