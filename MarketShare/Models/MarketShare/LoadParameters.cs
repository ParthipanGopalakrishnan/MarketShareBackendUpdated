namespace MarketShare.Models.MarketShare
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="LoadParameters" />.
    /// </summary>
    public class LoadParameters
    {
        /// <summary>
        /// Gets or sets the CategoryID.
        /// </summary>
        public int CategoryID { get; set; }

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

        /// <summary>
        /// Gets or sets the CategoryCountryStr.
        /// </summary>
        public string CategoryCountryStr { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCurrencyStr.
        /// </summary>
        public string CategoryCurrencyStr { get; set; }

        /// <summary>
        /// Gets or sets the CategoryYear.
        /// </summary>
        public int? CategoryYear { get; set; }

        /// <summary>
        /// Gets or sets the CategoryModel.
        /// </summary>
        public string CategoryModel { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubCategory.
        /// </summary>
        public string CategorySubCategory { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCountry.
        /// </summary>
        public string CategoryCountry { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMake.
        /// </summary>
        public string CategoryMake { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubModel.
        /// </summary>
        public string CategorySubModel { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="LoadParameter" />.
    /// </summary>
    public class LoadParameter
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public IList<string> CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryYear.
        /// </summary>
        public IList<int?> CategoryYear { get; set; }

        /// <summary>
        /// Gets or sets the CategoryModel.
        /// </summary>
        public IList<string> CategoryModel { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubModel.
        /// </summary>
        public IList<string> CategorySubModel { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMake.
        /// </summary>
        public IList<string> CategoryMake { get; set; }

        /// <summary>
        /// Gets or sets the CategoryPartNumber.
        /// </summary>
        public IList<string> CategoryPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the SubCategory.
        /// </summary>
        public IList<string> SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public IList<string> Country { get; set; }

        /// <summary>
        /// Gets or sets the filterSeq.
        /// </summary>
        public IList<string> filterSeq { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesYear.
        /// </summary>
        public IList<string> VehiclesYear { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesMake.
        /// </summary>
        public IList<string> VehiclesMake { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesModel.
        /// </summary>
        public IList<string> VehiclesModel { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesEngine.
        /// </summary>
        public IList<string> VehiclesEngine { get; set; }

        /// <summary>
        /// Gets or sets the PartTerminology.
        /// </summary>
        public IList<string> PartTerminology { get; set; }

        /// <summary>
        /// Gets or sets the filterObj.
        /// </summary>
        public FilterObj filterObj { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Gets or sets the Totalcount.
        /// </summary>
        public int Totalcount { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="FilterObj" />.
    /// </summary>
    public class FilterObj
    {
        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public IList<string> CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryYear.
        /// </summary>
        public IList<int?> CategoryYear { get; set; }

        /// <summary>
        /// Gets or sets the CategoryModel.
        /// </summary>
        public IList<object> CategoryModel { get; set; }

        /// <summary>
        /// Gets or sets the CategoryMake.
        /// </summary>
        public IList<object> CategoryMake { get; set; }

        /// <summary>
        /// Gets or sets the CategoryPartNumber.
        /// </summary>
        public IList<object> CategoryPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubModel.
        /// </summary>
        public IList<object> CategorySubModel { get; set; }

        /// <summary>
        /// Gets or sets the CategorySubCategory.
        /// </summary>
        public IList<string> CategorySubCategory { get; set; }

        /// <summary>
        /// Gets or sets the CategoryCountry.
        /// </summary>
        public IList<object> CategoryCountry { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesYear.
        /// </summary>
        public IList<string> VehiclesYear { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesMake.
        /// </summary>
        public IList<string> VehiclesMake { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesModel.
        /// </summary>
        public IList<string> VehiclesModel { get; set; }

        /// <summary>
        /// Gets or sets the VehiclesEngine.
        /// </summary>
        public IList<string> VehiclesEngine { get; set; }

        /// <summary>
        /// Gets or sets the Brands.
        /// </summary>
        public IList<string> Brands { get; set; }

        /// <summary>
        /// Gets or sets the PartTerminology.
        /// </summary>
        public IList<string> PartTerminology { get; set; }

        /// <summary>
        /// Gets or sets the SubCategory.
        /// </summary>
        public IList<string> SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the CategoryBrand.
        /// </summary>
        public IList<string> CategoryBrand { get; set; }

        /// <summary>
        /// Gets or sets the PartNumber.
        /// </summary>
        public IList<string> PartNumber { get; set; }

        /// <summary>
        /// Gets or sets the SearchScope.
        /// </summary>
        public int? SearchScope { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="WHIParameter" />.
    /// </summary>
    public class WHIParameter
    {
        public IList<long?> RefID { get; set; }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Gets or sets the Totalcount.
        /// </summary>
        public int Totalcount { get; set; }
    }

}
