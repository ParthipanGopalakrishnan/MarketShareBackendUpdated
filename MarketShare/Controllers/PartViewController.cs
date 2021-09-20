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
    /// Defines the <see cref="PartViewController" />.
    /// </summary>
    [RoutePrefix("api/PartData")]
    public class PartViewController : ApiController
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
        /// The GetPartNumberDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartGrid")]
        [HttpPost]
        public HttpResponseMessage GetPartNumberDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartNumberDetails = GetPartNumberData(parameters);
                PartGridDataDto PartGlobalData = new PartGridDataDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = PartNumberDetails
                };
                return Request.CreateResponse(PartGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartNumberDetails" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetPartNumberData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{PartModelDto}"/>.</returns>
        public IEnumerable<PartModelDto> GetPartNumberData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjPartData = (from x in context
                                       where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(x.Category)
                                       where parameters.filterObj.CategoryPartNumber.Count == 0 || parameters.filterObj.CategoryPartNumber.Contains(x.PartNumber)
                                       select (new PartModelDto()
                                       {
                                           PartId = x.ID,
                                           PartCategoryName = x.Category,
                                           PartNumber = x.PartNumber,
                                           PartYear = x.Year,
                                           PartMake = x.Make,
                                           PartModel = x.Model,
                                           PartSubModel = x.SubModel,
                                           PartVIODemand = x.VIODemand,
                                           PartMarketPotential = x.MarketPotential,
                                           PartMarketShare = x.Sum_of_Customer_12M_Sales_Market_Share,
                                           PartSalesDistribution = x.Customer_12M_Sales_Distribution,
                                           PartCountryStr = x.CountryStr,
                                           PartCurrencyStr = x.CurrencyStr
                                       })).ToList();
                    parameters.Totalcount = ObjPartData.Count;
                    var ObjPartGriddata = parameters.offset != 0 ? ObjPartData.Skip(parameters.offset).Take(parameters.count) : ObjPartData;
                    return ObjPartGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartNumberData" + ex.StackTrace);
                var ObjPartGriddata = Enumerable.Empty<PartModelDto>();
                return ObjPartGriddata;
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
                PartCategoryDto PartCategory = new PartCategoryDto
                {
                    CategoryName = JCategory
                };
                return Request.CreateResponse(PartCategory);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryData in Part" + ex.StackTrace);
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
                    var ObjModelGlobalData = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).GroupBy(e => e.Category).Select(x => new CategoryDto() { CategoryName = x.Key }).ToList();
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
        /// The PartSumData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartTotal")]
        [HttpPost]
        public HttpResponseMessage PartSumData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartDetails = GetPartSumDetails(parameters);
                return Request.CreateResponse(PartDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "PartSumData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategorySumDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="CategoryTotalDto"/>.</returns>
        public IEnumerable<PartTotalDto> GetPartSumDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjModelTotalData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (parameters.CategoryPartNumber.Count == 0) || (parameters.CategoryPartNumber.Contains(a.PartNumber))
                                            group a by new { a.Category, a.Year, a.Make, a.Model, a.SubModel, a.VIODemand, a.MarketPotential } into g
                                            select new PartModelDto
                                            {
                                                PartVIODemand = g.Key.VIODemand,
                                                PartMarketPotential = g.Key.MarketPotential,
                                            };
                    var ObjModelPartData = (from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (parameters.CategoryPartNumber.Count == 0) || (parameters.CategoryPartNumber.Contains(a.PartNumber))
                                            select new PartModelDto
                                            {
                                                PartNumber = a.PartNumber
                                            });
                    var ObjModelVechileData = from a in context
                                              where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                              where (parameters.CategoryPartNumber.Count == 0) || (parameters.CategoryPartNumber.Contains(a.PartNumber))
                                              group a by new { a.Category, a.Year, a.Make, a.Model, a.SubModel } into g
                                              select new PartModelDto
                                              {
                                                  PartCategoryName = g.Key.Category,
                                                  PartYear = g.Key.Year,
                                                  PartMake = g.Key.Make,
                                                  PartModel = g.Key.Model,
                                                  PartSubModel = g.Key.SubModel
                                              };

                    var ObjModelCustSalesData = from a in context
                                                where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                                where (parameters.CategoryPartNumber.Count == 0) || (parameters.CategoryPartNumber.Contains(a.PartNumber))
                                                group a by new { a.PartNumber, a.Customer_12M_Sales } into g
                                                select new PartModelDto
                                                {
                                                    PartNumber = g.Key.PartNumber,
                                                    PartCustSales = g.Key.Customer_12M_Sales,
                                                };
                    var ObjModelSumData = ObjModelTotalData.Concat(ObjModelPartData).Concat(ObjModelVechileData).Concat(ObjModelCustSalesData);
                    var ObjModleAggregateData = from a in ObjModelSumData
                                                group a by 0 into g
                                                select new PartTotalDto
                                                {
                                                    PartVIODemand = g.Sum(x => x.PartVIODemand),
                                                    PartMarketPotential = g.Sum(x => x.PartMarketPotential),
                                                    PartPartCount = g.Where(x => x.PartNumber != null).Select(x => x.PartNumber).Distinct().Count(),
                                                    PartVehicleCount = g.Where(x => x.PartCategoryName != null && x.PartYear != null && x.PartMake != null && x.PartModel != null && x.PartSubModel != null).Select(x => new { x.PartCategoryName, x.PartYear, x.PartMake, x.PartModel, x.PartSubModel }).Count(),
                                                    PartCustomerSales = g.Sum(x => x.PartCustSales),
                                                    PartMarketShare = g.Sum(x => x.PartCustSales) > g.Sum(x => x.PartMarketPotential) ? 100 : ((decimal)g.Sum(x => x.PartCustSales) / (decimal)g.Sum(x => x.PartMarketPotential)) * 100
                                                };
                    return ObjModleAggregateData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartSumDetails" + ex.StackTrace);
                var ObjPartSumData = Enumerable.Empty<PartTotalDto>();
                return ObjPartSumData;
            }
        }

        /// <summary>
        /// The PartFilterData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartFilter")]
        [HttpPost]
        public HttpResponseMessage PartFilterData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartDetails = GetPartFilterDetails(parameters);
                PartCategoryFilterdto PartFilterData = new PartCategoryFilterdto
                {
                    PartNumber = PartDetails.PartNumber.Distinct().ToList()
                };
                return Request.CreateResponse(PartFilterData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "PartFilterData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{CategoryDto}"/>.</returns>
        public PartCategoryFilterdto GetPartFilterDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    PartCategoryFilterdto ObjPartFilterData = new PartCategoryFilterdto();
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country && parameters.CategoryName.Contains(c.Category)).ToList();
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryName))
                    {
                        context.ForEach(c => { ObjPartFilterData.PartNumber.Add(c.PartNumber); });
                    }
                    return ObjPartFilterData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartFilterDetails" + ex.StackTrace);
                PartCategoryFilterdto ObjPartData = new PartCategoryFilterdto();
                return ObjPartData;
            }
        }
    }
}
