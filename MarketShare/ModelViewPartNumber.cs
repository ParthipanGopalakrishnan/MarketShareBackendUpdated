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
    
    public partial class ModelViewPartNumber
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string PartNumber { get; set; }
        public Nullable<int> Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public Nullable<int> VIODemand { get; set; }
        public Nullable<int> MarketPotential { get; set; }
        public Nullable<decimal> Sum_of_Customer_12M_Sales_Market_Share { get; set; }
        public Nullable<decimal> Customer_12M_Sales_Distribution { get; set; }
        public string CountryStr { get; set; }
        public string CurrencyStr { get; set; }
        public Nullable<int> Customer_12M_Sales { get; set; }
    }
}
