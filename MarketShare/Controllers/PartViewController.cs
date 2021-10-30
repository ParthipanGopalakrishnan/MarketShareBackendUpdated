namespace MarketShare.Controllers
{
    using log4net;
    using MarketShare.Models;
    using MarketShare.Models.MarketShare;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
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
        /// The PartNumberSumData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartNumberTotal")]
        [HttpPost]
        public HttpResponseMessage PartNumberSumData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartNumberDetails = GetPartNumberSumDetails(parameters);
                return Request.CreateResponse(PartNumberDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "PartNumberSumData" + ex.StackTrace);
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

        /// <summary>
        /// The GetPartNumberGridDetail.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartNumberGrid")]
        [HttpPost]
        public HttpResponseMessage GetPartNumberGridDetail(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartNumberDetails = GetPartNumberGridData(parameters);
                return Request.CreateResponse(PartNumberDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartNumberDetails" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetPartNumberGridData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{PartNumberModelDto}"/>.</returns>
        public IEnumerable<PartNumberModelDto> GetPartNumberGridData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    List<PartNumberModelDto> ObjPartNumberdata = new List<PartNumberModelDto>();
                    PartNumberModelDto ObjPartNumber = new PartNumberModelDto();
                    PartNumberModelDto ObjPartSuperNumber = new PartNumberModelDto();
                    string Category = string.Join(";", parameters.filterObj.CategoryName);
                    string PartNumber = string.Join(";", parameters.filterObj.PartNumber);
                    var PartDistlst = db.GetPartNumberDetails("1", PartNumber, Category).GroupBy(x => new { x.Category, x.PartId, x.PartNumber, x.SuperSessionData }).ToList();
                    var PartSuperDistlst = db.GetPartNumberDetails("0", PartNumber, Category).GroupBy(x => new { x.Category, x.PartId, x.PartNumber, x.SuperSessionData }).ToList();

                    foreach (var Item in PartDistlst)
                    {
                        ObjPartNumber = new PartNumberModelDto()
                        {
                            PartId = Item.Key.PartId.ToString(),
                            PartCategoryName = Item.Key.Category,
                            PartNumber = Item.Key.PartNumber,
                            PartYear = ObjPartNumber.PartYear,
                            PartMake = ObjPartNumber.PartMake,
                            PartModel = ObjPartNumber.PartModel,
                            PartSubModel = ObjPartNumber.PartSubModel,
                            PartVIODemand = ObjPartNumber.PartVIODemand,
                            PartMarketPotential = ObjPartNumber.PartMarketPotential,
                            PartMarketShare = ObjPartNumber.PartMarketShare,
                            PartSalesDistribution = ObjPartNumber.PartSalesDistribution,
                            PartCountryStr = ObjPartNumber.PartCountryStr,
                            PartCurrencyStr = ObjPartNumber.PartCurrencyStr,
                            items = Item.Select(x => new Item()
                            {
                                PartId = x.ID.ToString(),
                                PartCategoryName = x.Category,
                                PartNumber = x.PartNumber,
                                PartYear = x.Year,
                                PartMake = x.Make,
                                PartModel = x.Model,
                                PartSubModel = x.SubModel,
                                PartVIODemand = x.VIODemand,
                                PartMarketPotential = x.MarketPotential,
                                PartMarketShare = x.Customer_12M_Sales_Market_Share,
                                PartSalesDistribution = x.Customer_12M_Sales_Distribution,
                                PartCountryStr = x.CountryStr,
                                PartCurrencyStr = x.CurrencyStr
                            }).ToList()
                        };
                        ObjPartNumberdata.Add(ObjPartNumber);
                    }

                    int idx = 1;
                    foreach (var CItems in PartSuperDistlst)
                    {
                        if (CItems.Key.SuperSessionData == null)
                        {
                            int idxx = 1;
                            ObjPartSuperNumber = new PartNumberModelDto()
                            {
                                PartId = CItems.Key.PartId + "_" + idx,
                                PartCategoryName = CItems.Key.Category,
                                PartNumber = CItems.Key.PartNumber,
                                PartYear = ObjPartSuperNumber.PartYear,
                                PartMake = ObjPartSuperNumber.PartMake,
                                PartModel = ObjPartSuperNumber.PartModel,
                                PartSubModel = ObjPartSuperNumber.PartSubModel,
                                PartVIODemand = ObjPartSuperNumber.PartVIODemand,
                                PartMarketPotential = ObjPartSuperNumber.PartMarketPotential,
                                PartMarketShare = ObjPartSuperNumber.PartMarketShare,
                                PartSalesDistribution = ObjPartSuperNumber.PartSalesDistribution,
                                PartCountryStr = ObjPartSuperNumber.PartCountryStr,
                                PartCurrencyStr = ObjPartSuperNumber.PartCurrencyStr,
                                items = new List<Item>(),
                            };

                            foreach (var CCItem in PartSuperDistlst.Where(x => (x.Key.PartId == CItems.Key.PartId)))
                            {
                                Item CCItems = new Item()
                                {
                                    PartId = CCItem.Key.PartId + "_" + idx + "_" + idxx,
                                    PartCategoryName = CCItem.Key.Category,
                                    PartNumber = CCItem.Key.PartNumber,
                                    PartYear = ObjPartSuperNumber.PartYear,
                                    PartMake = ObjPartSuperNumber.PartMake,
                                    PartModel = ObjPartSuperNumber.PartModel,
                                    PartSubModel = ObjPartSuperNumber.PartSubModel,
                                    PartVIODemand = ObjPartSuperNumber.PartVIODemand,
                                    PartMarketPotential = ObjPartSuperNumber.PartMarketPotential,
                                    PartMarketShare = ObjPartSuperNumber.PartMarketShare,
                                    PartSalesDistribution = ObjPartSuperNumber.PartSalesDistribution,
                                    PartCountryStr = ObjPartSuperNumber.PartCountryStr,
                                    PartCurrencyStr = ObjPartSuperNumber.PartCurrencyStr,
                                    items = CCItem.Select(x => new Item()
                                    {
                                        PartId = x.ID.ToString(),
                                        PartCategoryName = x.Category,
                                        PartNumber = x.PartNumber,
                                        PartYear = x.Year,
                                        PartMake = x.Make,
                                        PartModel = x.Model,
                                        PartSubModel = x.SubModel,
                                        PartVIODemand = x.VIODemand,
                                        PartMarketPotential = x.MarketPotential,
                                        PartMarketShare = x.Customer_12M_Sales_Market_Share,
                                        PartSalesDistribution = x.Customer_12M_Sales_Distribution,
                                        PartCountryStr = x.CountryStr,
                                        PartCurrencyStr = x.CurrencyStr
                                    }).ToList()
                                };
                                ObjPartSuperNumber.items.Add(CCItems);
                                idxx++;
                            }
                            idx++;
                            ObjPartNumberdata.Add(ObjPartSuperNumber);
                        }

                    }

                    var ObjPartNumberGriddata = parameters.offset != 0 ? ObjPartNumberdata.Skip(parameters.offset).Take(parameters.count) : ObjPartNumberdata;
                    return ObjPartNumberGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartNumberData" + ex.StackTrace);
                var ObjPartNumberdata = new List<PartNumberModelDto>();
                return ObjPartNumberdata;
            }
        }

        /// <summary>
        /// The PartNumberFilterData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartNumberFilter")]
        [HttpPost]
        public HttpResponseMessage PartNumberFilterData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartDetails = GetPartFilterDatas(parameters);
                int i = 1;
                PartDetails.ForEach(c => { c.PartID = c.PartID + "_" + i++; int j = 1; c.items.ForEach(x => { x.PartID = c.PartID + "_" + j++; }); });
                return Request.CreateResponse(PartDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "PartNumberFilterData" + ex.StackTrace);
                return Request.CreateResponse(ex.StackTrace);
            }
        }

        /// <summary>
        /// The GetPartFilterDatas.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="List{PartNumberFilterdto}"/>.</returns>
        public List<PartNumberFilterdto> GetPartFilterDatas(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var ObjPartFilterData = (from x in db.PartNumberViews
                                             where x.CountryStr == Country
                                             where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(x.Category)
                                             //where parameters.filterObj.PartNumber.Count == 0 || parameters.filterObj.PartNumber.Contains(x.PartNumber)
                                             group x by new { x.PartNumber, x.PartId, x.Category } into k
                                             select new PartNumberFilterdto
                                             {
                                                 PartNumber = k.Key.PartNumber,
                                                 PartID = k.Key.PartId.ToString(),
                                                 Category = k.Key.Category,
                                                 items = (from m in db.SupersessionParts
                                                          where m.PartNumber == k.Key.PartNumber
                                                          select new PrePartNumberFilterdto { PartID = k.Key.PartId.ToString(), PartNumber = m.PrePartNo, Category = k.Key.Category }).ToList()
                                             }).ToList();
                    return ObjPartFilterData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartFilterDatas" + ex.StackTrace);
                var ObjPartNumberdata = new List<PartNumberFilterdto>();
                return ObjPartNumberdata;
            }
        }

        /// <summary>
        /// The GetPartNumberSumDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{PartTotalDto}"/>.</returns>
        public IEnumerable<PartTotalDto> GetPartNumberSumDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var ObjPartData = db.SupersessionParts.
                        Where(x => parameters.CategoryPartNumber.Count == 0 || parameters.CategoryPartNumber.Contains(x.PrePartNo) || parameters.CategoryPartNumber.Contains(x.PartNumber))
                        .GroupBy(u => u.PartNumber)
                        .Select(grp => grp.ToList()).ToList();
                    IList<string> JPartNumber = new List<string>();
                    ObjPartData.ForEach(x => x.ForEach(y =>
                    {
                        if (parameters.CategoryPartNumber.Contains(y.PartNumber)) { parameters.CategoryPartNumber.Remove(y.PartNumber); }
                        if (parameters.CategoryPartNumber.Contains(y.PrePartNo)) { parameters.CategoryPartNumber.Remove(y.PrePartNo); }
                        JPartNumber.Add(y.PartNumber);
                    }));
                    parameters.CategoryPartNumber.ToList().ForEach(c => JPartNumber.Add(c));
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjModelTotalData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (JPartNumber.Count == 0) || (JPartNumber.Contains(a.PartNumber))
                                            group a by new { a.Category, a.Year, a.Make, a.Model, a.SubModel, a.VIODemand, a.MarketPotential } into g
                                            select new PartModelDto
                                            {
                                                PartVIODemand = g.Key.VIODemand,
                                                PartMarketPotential = g.Key.MarketPotential,
                                            };
                    var ObjModelPartData = (from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (JPartNumber.Count == 0) || (JPartNumber.Contains(a.PartNumber))
                                            select new PartModelDto
                                            {
                                                PartNumber = a.PartNumber
                                            });
                    var ObjModelVechileData = from a in context
                                              where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                              where (JPartNumber.Count == 0) || (JPartNumber.Contains(a.PartNumber))
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
                                                where (JPartNumber.Count == 0) || (JPartNumber.Contains(a.PartNumber))
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
                Log.Error(_authData.GetUsername() + "GetPartNumberSumDetails" + ex.StackTrace);
                var ObjPartSumData = Enumerable.Empty<PartTotalDto>();
                return ObjPartSumData;
            }
        }

        /// <summary>
        /// The GetPartNumberExcelDetail.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartNumberExcel")]
        [HttpPost]
        public HttpResponseMessage GetPartNumberExcelDetail(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartNumberDetails = GetExcelPartNumberData(parameters);
                var fileName = "Part_Number_" + parameters.filterObj.CategoryName[0].ToString() + "_Report";

                var resultContent = new ExcelExportModel().ExportExcelData(PartNumberDetails.ToList(), fileName);

                return resultContent;
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartNumberExcelDetail" + ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// The GetExcelPartNumberData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IList{PartNumberExcelDto}"/>.</returns>
        public IList<PartNumberExcelDto> GetExcelPartNumberData(LoadParameter parameters)
        {
            try
            {
                Utility ObjUtils = new Utility();
                using (var db = _authData.GetContext())
                {
                    List<PartNumberExcelDto> ObjPartNumberdata = new List<PartNumberExcelDto>();
                    PartNumberExcelDto ObjPartNumber = new PartNumberExcelDto();
                    PartNumberExcelDto ObjPartNumber1 = new PartNumberExcelDto();
                    PartNumberExcelDto ObjPartSuperNumber = new PartNumberExcelDto();
                    string Category = string.Join(";", parameters.filterObj.CategoryName);
                    string PartNumber = string.Join(";", parameters.filterObj.PartNumber);
                    var PartDistlst = db.GetPartNumberDetails("1", PartNumber, Category).GroupBy(x => new { x.Category, x.PartId, x.PartNumber, x.SuperSessionData }).ToList();
                    var PartSuperDistlst = db.GetPartNumberDetails("0", PartNumber, Category).GroupBy(x => new { x.Category, x.PartId, x.PartNumber, x.SuperSessionData }).ToList();

                    foreach (var Item in PartDistlst)
                    {
                        ObjPartNumber = new PartNumberExcelDto()
                        {
                            PartCategoryName = Item.Key.Category,
                            PartNumber = Item.Key.PartNumber,
                            PartYear = ObjPartNumber.PartYear,
                            PartMake = ObjPartNumber.PartMake,
                            PartModel = ObjPartNumber.PartModel,
                            PartSubModel = ObjPartNumber.PartSubModel,
                            PartVIODemand = ObjPartNumber.PartVIODemand,
                            PartMarketPotential = ObjPartNumber.PartMarketPotential,
                            PartMarketShare = ObjPartNumber.PartMarketShare,
                            PartSalesDistribution = ObjPartNumber.PartSalesDistribution,
                            PartCountryStr = ObjPartNumber.PartCountryStr,
                            PartCurrencyStr = ObjPartNumber.PartCurrencyStr
                        };
                        List<PartNumberExcelDto> Excelitems = new List<PartNumberExcelDto>();
                        Excelitems = Item.Select(x => new PartNumberExcelDto()
                        {
                            PartCategoryName = x.Category,
                            PartNumber = x.PartNumber,
                            PartYear = x.Year,
                            PartMake = x.Make,
                            PartModel = x.Model,
                            PartSubModel = x.SubModel,
                            PartVIODemand = ObjUtils.FormatNumber(x.VIODemand, 4),
                            PartMarketPotential = ObjUtils.FormatNumber(x.MarketPotential, 4),
                            PartMarketShare = Math.Round((double)x.Customer_12M_Sales_Market_Share, 1, MidpointRounding.AwayFromZero) + " %",
                            PartSalesDistribution = ObjUtils.FormatNumber(x.Customer_12M_Sales_Distribution, 4),
                            PartCountryStr = x.CountryStr,
                            PartCurrencyStr = x.CurrencyStr
                        }).ToList();
                        ObjPartNumberdata.Add(ObjPartNumber);
                        ObjPartNumberdata.AddRange(Excelitems);
                    }
                    foreach (var CItems in PartSuperDistlst)
                    {
                        if (CItems.Key.SuperSessionData == null)
                        {
                            ObjPartSuperNumber = new PartNumberExcelDto()
                            {

                                PartCategoryName = CItems.Key.Category,
                                PartNumber = CItems.Key.PartNumber,
                                PartYear = ObjPartSuperNumber.PartYear,
                                PartMake = ObjPartSuperNumber.PartMake,
                                PartModel = ObjPartSuperNumber.PartModel,
                                PartSubModel = ObjPartSuperNumber.PartSubModel,
                                PartVIODemand = ObjPartSuperNumber.PartVIODemand,
                                PartMarketPotential = ObjPartSuperNumber.PartMarketPotential,
                                PartMarketShare = ObjPartSuperNumber.PartMarketShare,
                                PartSalesDistribution = ObjPartSuperNumber.PartSalesDistribution,
                                PartCountryStr = ObjPartSuperNumber.PartCountryStr,
                                PartCurrencyStr = ObjPartSuperNumber.PartCurrencyStr

                            };
                            ObjPartNumberdata.Add(ObjPartSuperNumber);
                            foreach (var CCItem in PartSuperDistlst.Where(x => (x.Key.PartId == CItems.Key.PartId)))
                            {
                                PartNumberExcelDto Excelitems = new PartNumberExcelDto();
                                Excelitems = new PartNumberExcelDto()
                                {
                                    PartCategoryName = CCItem.Key.Category,
                                    PartNumber = CCItem.Key.PartNumber,
                                    PartYear = ObjPartSuperNumber.PartYear,
                                    PartMake = ObjPartSuperNumber.PartMake,
                                    PartModel = ObjPartSuperNumber.PartModel,
                                    PartSubModel = ObjPartSuperNumber.PartSubModel,
                                    PartVIODemand = ObjPartSuperNumber.PartVIODemand,
                                    PartMarketPotential = ObjPartSuperNumber.PartMarketPotential,
                                    PartMarketShare = ObjPartSuperNumber.PartMarketShare,
                                    PartSalesDistribution = ObjPartSuperNumber.PartSalesDistribution,
                                    PartCountryStr = ObjPartSuperNumber.PartCountryStr,
                                    PartCurrencyStr = ObjPartSuperNumber.PartCurrencyStr

                                };
                                ObjPartNumberdata.Add(Excelitems);
                                List<PartNumberExcelDto> Excelitems1 = new List<PartNumberExcelDto>();
                                Excelitems1 = CCItem.Select(x => new PartNumberExcelDto()
                                {
                                    PartCategoryName = x.Category,
                                    PartNumber = x.PartNumber,
                                    PartYear = x.Year,
                                    PartMake = x.Make,
                                    PartModel = x.Model,
                                    PartSubModel = x.SubModel,
                                    PartVIODemand = ObjUtils.FormatNumber(x.VIODemand, 4),
                                    PartMarketPotential = ObjUtils.FormatNumber(x.MarketPotential, 4),
                                    PartMarketShare = Math.Round((double)x.Customer_12M_Sales_Market_Share, 1, MidpointRounding.AwayFromZero) + " %",
                                    PartSalesDistribution = ObjUtils.FormatNumber(x.Customer_12M_Sales_Distribution, 4),
                                    PartCountryStr = x.CountryStr,
                                    PartCurrencyStr = x.CurrencyStr
                                }).ToList();
                                ObjPartNumberdata.AddRange(Excelitems1);
                            }
                        }

                    }

                    return ObjPartNumberdata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetExcelPartNumberData" + ex.StackTrace);
                var ObjPartNumberdata = Enumerable.Empty<PartNumberExcelDto>();
                return (IList<PartNumberExcelDto>)ObjPartNumberdata;
            }
        }
    }
}