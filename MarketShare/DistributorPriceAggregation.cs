//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketShare
{
    using System;
    using System.Collections.Generic;
    
    public partial class DistributorPriceAggregation
    {
        public long Id { get; set; }
        public long RefId { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public int PriceTypeId { get; set; }
        public Nullable<int> TransactionCount { get; set; }
        public Nullable<decimal> MinimumPrice { get; set; }
        public Nullable<decimal> MaximumPrice { get; set; }
        public Nullable<decimal> AveragePrice { get; set; }
        public Nullable<decimal> MedianPrice { get; set; }
    }
}