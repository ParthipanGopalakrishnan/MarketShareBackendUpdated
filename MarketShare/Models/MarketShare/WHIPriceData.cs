namespace MarketShare.Models.MarketShare
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WHIPartDetailsWithAggPrice" />.
    /// </summary>
    public class WHIPartDetailsWithAggPrice
    {
        public long PartId { get; set; }
        public string PartNumber { get; set; }
        public string Wholesaler { get; set; }
        public string Brand { get; set; }
        public string PartDescription { get; set; }
        public Nullable<int> TransactionCount { get; set; }
        public Nullable<decimal> mn { get; set; }
        public Nullable<decimal> mx { get; set; }
        public Nullable<decimal> avg { get; set; }
        public Nullable<decimal> me { get; set; }

    }
    
    /// <summary>
    /// Defines the <see cref="WHIMULPriceData" />.
    /// </summary>
    public class WHIMULPriceData
    {
        public decimal PriceValue { get; set; }
        public System.DateTime PriceDate { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }

    }

    /// <summary>
    /// Defines the <see cref="WHIPriceData" />.
    /// </summary>
    public class WHIPriceData
    {
        public long PartId { get; set; }
        public string PartNumber { get; set; }
        public string Wholesaler { get; set; }
        public string Brand { get; set; }
        public string PartDescription { get; set; }
        public Nullable<int> TransactionCount { get; set; }
        public Nullable<decimal> mn { get; set; }
        public Nullable<decimal> mx { get; set; }
        public Nullable<decimal> avg { get; set; }
        public Nullable<decimal> me { get; set; }
        public decimal PriceValue { get; set; }
        public System.DateTime PriceDate { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }

    }

    /// <summary>
    /// Defines the <see cref="WHIPriceDataDto" />.
    /// </summary>
    public class WHIPriceDataDto
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
        public IEnumerable<WHIPriceData> data { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="WHIMULPriceDataDto" />.
    /// </summary>
    public class WHIMULPriceDataDto
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
        public IEnumerable<WHIMULPriceData> data { get; set; }
    }
}
