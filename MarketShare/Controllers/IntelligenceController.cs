namespace MarketShare.Controllers
{
    using log4net;
    using MarketShare.Models.MarketShare;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Configuration;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="IntelligenceController" />.
    /// </summary>
    [RoutePrefix("api/IntelligenceDashboard")]
    public class IntelligenceController : ApiController
    {
        /// <summary>
        /// Defines the _authData.
        /// </summary>
        private readonly AuthData.AuthData _authData = new AuthData.AuthData();

        /// <summary>
        /// Defines the Log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The GetIntelligenceDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("IntelligenceView")]
        [HttpPost]
        public HttpResponseMessage GetIntelligenceDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var IntelligenceDetails = GetIntelligenceData(parameters);
                IntelligenceGridDataDto IntelligenceGlobalData = new IntelligenceGridDataDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = IntelligenceDetails
                };
                return Request.CreateResponse(IntelligenceGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetIntelligenceDetails" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetIntelligenceData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{IntelligenceDto}"/>.</returns>
        public IEnumerable<IntelligenceDto> GetIntelligenceData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.t_IntelligenceViewData.Where(c => c.CountryStr == Country).ToList();
                    var ObjIntelligenceData = (from x in context
                                               where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(x.Category)
                                               where parameters.filterObj.CategorySubCategory.Count == 0 || parameters.filterObj.CategorySubCategory.Contains(x.SubCategory)
                                               where parameters.filterObj.CategoryPartNumber.Count == 0 || parameters.filterObj.CategoryPartNumber.Contains(x.PartNo)
                                               where parameters.filterObj.CategoryCountry.Count == 0 || parameters.filterObj.CategoryCountry.Contains(x.Country)
                                               select (new IntelligenceDto() { IntelligenceID = x.ID, IntelligenceCountryStr = x.CountryStr, IntelligenceCountry = x.Country, IntelligenceCurrencyCode = x.CurrencyCode, IntelligencCustomerPartId = x.CustomerPartId, IntelligencePartNo = x.PartNo, IntelligenceDescription = x.Description, IntelligenceCategory = x.Category, IntelligenceSubCategory = x.SubCategory, IntelligenceVehicleName = x.VehicleName, IntelligenceListPrice = x.List_Price, IntelligenceACPOnlinePrice = x.ACP_Online_Price, IntelligenceNETPrice = x.NET_Price, IntelligenceCorePrice = x.Core_Price, IntelligenceInstallerPrice = x.Installer_Price, IntelligenceOEMAvgListPrice = x.OEMAvgList_Price, IntelligenceOEMAvgACPOnlinePrice = x.OEMAvgACP_Online_Price, IntelligenceOEMAvgNETPrice = x.OEMAvgNET_Price, IntelligenceOEMAvgCorePrice = x.OEMAvgCore_Price, IntelligenceOEMAvgInstallerPrice = x.OEMAvgInstaller_Price, IntelligenceIAMAvgListPrice = x.IAMAvgList_Price, IntelligenceIAMAvgACPOnlinePrice = x.IAMAvgACP_Online_Price, IntelligenceIAMAvgNETPrice = x.IAMAvgNET_Price, IntelligenceIAMAvgCorePrice = x.IAMAvgCore_Price, IntelligenceIAMAvgInstallerPrice = x.IAMAvgInstaller_Price, IntelligenceVIODemand = x.VIO_Demand, IntelligenceMarketPotential = x.Market_Potential, IntelligenceMarketShare = x.Market_Share, IntelligenceCustomer12MSales = x.Customer_12M_Sales, IntelligenceOEMPriceTrend = x.OEM_Price_Trend, IntelligenceIAMPriceTrend = x.IAM_Price_Trend, IntelligenceVIODemandTrend = x.VIO_Demand_Trend, IntelligenceMarketPotentialTrend = x.Market_Potential_Trend, IntelligenceMarketShareTrend = x.Market_Share_Trend, IntelligenceCustomer12MSalesTrend = x.Customer_12M_Sales_Trend, IntelligenceCurrencyStr = x.CurrencyStr })).ToList();
                    parameters.Totalcount = ObjIntelligenceData.Count;
                    var ObjPartGriddata = parameters.offset != 0 ? ObjIntelligenceData.Skip(parameters.offset).Take(parameters.count) : ObjIntelligenceData;
                    return ObjPartGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetIntelligenceData" + ex.StackTrace);
                var ObjIntelligenceData = Enumerable.Empty<IntelligenceDto>();
                return ObjIntelligenceData;
            }
        }

        /// <summary>
        /// The CategoryData.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("Category")]
        [HttpPost]
        public HttpResponseMessage CategoryData()
        {
            try
            {
                var CategoryDetails = GetCategoryDetails();
                List<string> JCategory = new List<string>();
                CategoryDetails.ToList().ForEach(c => JCategory.Add(c.CategoryName));
                IntelligenceCategoryDto IntelligenceCategory = new IntelligenceCategoryDto
                {
                    CategoryName = JCategory
                };
                return Request.CreateResponse(IntelligenceCategory);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{CategoryDto}"/>.</returns>
        public IEnumerable<CategoryDto> GetCategoryDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var ObjModelGlobalData = db.t_IntelligenceViewData.Where(c => c.CountryStr == Country).GroupBy(e => e.Category).Select(x => new CategoryDto() { CategoryName = x.Key }).ToList();
                    return ObjModelGlobalData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryDetails" + ex.StackTrace);
                var ObjCategoryData = Enumerable.Empty<CategoryDto>();
                return ObjCategoryData;
            }
        }

        /// <summary>
        /// The IntelligenceFilterData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("IntelligenceFilter")]
        [HttpPost]
        public HttpResponseMessage IntelligenceFilterData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var IntelligenceDetails = GetIntelligenceFilterDetails(parameters);
                IntelligenceFilterDto IntelligenceFilterData = new IntelligenceFilterDto
                {
                    IntelligenceSubCategory = IntelligenceDetails.IntelligenceSubCategory.Distinct().ToList(),
                    IntelligencePartNo = IntelligenceDetails.IntelligencePartNo.Distinct().ToList(),
                    IntelligenceCountry = IntelligenceDetails.IntelligenceCountry.Distinct().ToList()
                };
                return Request.CreateResponse(IntelligenceFilterData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "IntelligenceFilterData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{CategoryDto}"/>.</returns>
        public IntelligenceFilterDto GetIntelligenceFilterDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    IntelligenceFilterDto ObjIntelligenceFilterData = new IntelligenceFilterDto();
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.t_IntelligenceViewData.Where(c => c.CountryStr == Country && parameters.CategoryName.Contains(c.Category)).ToList();
                    var ContextData = context.Where(x => (!parameters.filterSeq.Contains(MarketShareFieldConstant.SubCategory) || parameters.SubCategory.Contains(x.SubCategory)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.PartNumber) || parameters.Country.Contains(x.Country))).ToList();
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.SubCategory))
                    {
                        context.ForEach(c => { ObjIntelligenceFilterData.IntelligenceSubCategory.Add(c.SubCategory); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.PartNumber))
                    {
                        context.Where(c => parameters.SubCategory.Contains(c.SubCategory)).ToList().ForEach(c => { ObjIntelligenceFilterData.IntelligencePartNo.Add(c.PartNo); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.Country))
                    {
                        context.Where(c => (!parameters.filterSeq.Contains(MarketShareFieldConstant.SubCategory) || parameters.SubCategory.Contains(c.SubCategory)) &&
                        (!parameters.filterSeq.Contains(MarketShareFieldConstant.PartNumber) || parameters.CategoryPartNumber.Contains(c.PartNo))).ToList().ForEach(c => { ObjIntelligenceFilterData.IntelligenceCountry.Add(c.Country); });
                    }
                    ContextData.ForEach(c =>
                    {
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.SubCategory)) { ObjIntelligenceFilterData.IntelligenceSubCategory.Add(c.SubCategory); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.PartNumber)) { ObjIntelligenceFilterData.IntelligencePartNo.Add(c.PartNo); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.Country)) { ObjIntelligenceFilterData.IntelligenceCountry.Add(c.Country); }
                    });
                    return ObjIntelligenceFilterData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetIntelligenceFilterDetails" + ex.StackTrace);
                IntelligenceFilterDto ObjIntelligenceFilterData = new IntelligenceFilterDto();
                return ObjIntelligenceFilterData;
            }
        }
    }
}
