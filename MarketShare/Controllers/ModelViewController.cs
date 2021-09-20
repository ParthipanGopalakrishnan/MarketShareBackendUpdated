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
    /// Defines the <see cref="ModelViewController" />.
    /// </summary>
    [RoutePrefix("api/ModelData")]
    public class ModelViewController : ApiController
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
        /// The GetModelNumberDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("ModelGrid")]
        [HttpPost]
        public HttpResponseMessage GetModelNumberDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var ModelDetails = GetModelViewData(parameters);
                ModelGridDataDto ModelGlobalData = new ModelGridDataDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = ModelDetails
                };
                return Request.CreateResponse(ModelGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetModelViewData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{ModelViewDto}"/>.</returns>
        public IEnumerable<ModelViewDto> GetModelViewData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjModelData = (from x in context
                                        where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(x.Category)
                                        where parameters.filterObj.CategoryYear.Count == 0 || parameters.filterObj.CategoryYear.Contains(x.Year)
                                        where parameters.filterObj.CategoryMake.Count == 0 || parameters.filterObj.CategoryMake.Contains(x.Make)
                                        where parameters.filterObj.CategoryModel.Count == 0 || parameters.filterObj.CategoryModel.Contains(x.Model)
                                        where parameters.filterObj.CategorySubModel.Count == 0 || parameters.filterObj.CategorySubModel.Contains(x.SubModel)
                                        select (new ModelViewDto()
                                        {
                                            ModelId = x.ID,
                                            ModelCategoryName = x.Category,
                                            ModelNumber = x.PartNumber,
                                            ModelYear = x.Year,
                                            ModelMake = x.Make,
                                            ModelModel = x.Model,
                                            ModelSubModel = x.SubModel,
                                            ModelVIODemand = x.VIODemand,
                                            ModelMarketPotential = x.MarketPotential,
                                            ModelMarketShare = x.Sum_of_Customer_12M_Sales_Market_Share,
                                            ModelSalesDistribution = x.Customer_12M_Sales_Distribution,
                                            ModelCountryStr = x.CountryStr,
                                            ModelCurrencyStr = x.CurrencyStr
                                        })).ToList();
                    parameters.Totalcount = ObjModelData.Count;
                    var ObjModelGriddata = parameters.offset != 0 ? ObjModelData.Skip(parameters.offset).Take(parameters.count) : ObjModelData;
                    return ObjModelGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var ObjModelData = Enumerable.Empty<ModelViewDto>();
                return ObjModelData;
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
                ModelCategoryDto ModelCategory = new ModelCategoryDto
                {
                    CategoryName = JCategory
                };
                return Request.CreateResponse(ModelCategory);
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
        /// The ModelSumData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("ModelTotal")]
        [HttpPost]
        public HttpResponseMessage ModelSumData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var ModelDetails = GetModelSumDetails(parameters);
                return Request.CreateResponse(ModelDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "ModelSumData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategorySumDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="CategoryTotalDto"/>.</returns>
        public IEnumerable<ModelTotalDto> GetModelSumDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjModelVDTotalData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                            where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                            where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                            where (parameters.CategorySubModel.Count == 0) || (parameters.CategorySubModel.Contains(a.SubModel))
                                            group a by new { a.Category, a.Year, a.Make, a.Model, a.SubModel, a.VIODemand } into g
                                            select new ModelViewDto
                                            {
                                                ModelCategoryName = g.Key.Category,
                                                ModelYear = g.Key.Year,
                                                ModelMake = g.Key.Make,
                                                ModelVIODemand = g.Key.VIODemand

                                            };
                    var ObjModelMPTotalData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                            where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                            where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                            where (parameters.CategorySubModel.Count == 0) || (parameters.CategorySubModel.Contains(a.SubModel))
                                            group a by new { a.Category, a.Year, a.Make, a.Model, a.SubModel, a.MarketPotential } into g
                                            select new ModelViewDto
                                            {
                                                ModelCategoryName = g.Key.Category,
                                                ModelYear = g.Key.Year,
                                                ModelMake = g.Key.Make,
                                                ModelMarketPotential = g.Key.MarketPotential
                                            };
                    var ObjModelPartData = (from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                            where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                            where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                            where (parameters.CategorySubModel.Count == 0) || (parameters.CategorySubModel.Contains(a.SubModel))
                                            select new ModelViewDto
                                            {
                                                ModelPartNumber = a.PartNumber
                                            });
                    var ObjModelMarketData = from a in context
                                             where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                             where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                             where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                             where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                             where (parameters.CategorySubModel.Count == 0) || (parameters.CategorySubModel.Contains(a.SubModel))
                                             select new ModelViewDto
                                             {
                                                 ModelMarketShare = a.Sum_of_Customer_12M_Sales_Market_Share
                                             };
                    var ObjModelSumData = ObjModelVDTotalData.Concat(ObjModelMPTotalData).Concat(ObjModelPartData).Concat(ObjModelMarketData);
                    var ObjModleAggregateData = from a in ObjModelSumData
                                                group a by 0 into g
                                                select new ModelTotalDto
                                                {
                                                    ModelVIODemand = g.Sum(x => x.ModelVIODemand),
                                                    ModelMarketPotential = g.Sum(x => x.ModelMarketPotential),
                                                    ModelPartCount = g.Where(x => x.ModelPartNumber != null).Select(x => x.ModelPartNumber).Distinct().Count(),
                                                    ModelMinMarketShare = g.Min(x => x.ModelMarketShare),
                                                    ModelMaxMarketShare = g.Max(x => x.ModelMarketShare)
                                                };
                    return ObjModleAggregateData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartSumDetails" + ex.StackTrace);
                var ObjModelSumData = Enumerable.Empty<ModelTotalDto>();
                return ObjModelSumData;
            }
        }

        /// <summary>
        /// The ModelFilterData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("ModelFilter")]
        [HttpPost]
        public HttpResponseMessage ModelFilterData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var ModelDetails = GetModelFilterDetails(parameters);
                ModelCategoryFilterDto ModelFilterData = new ModelCategoryFilterDto
                {
                    CategoryYearfilter = ModelDetails.CategoryYearfilter.Distinct().ToList(),
                    CategoryMakefilter = ModelDetails.CategoryMakefilter.Distinct().ToList(),
                    CategoryModelfilter = ModelDetails.CategoryModelfilter.Distinct().ToList(),
                    CategorySubModelfilter = ModelDetails.CategorySubModelfilter.Distinct().ToList()
                };
                return Request.CreateResponse(ModelFilterData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "ModelFilterData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{CategoryDto}"/>.</returns>
        public ModelCategoryFilterDto GetModelFilterDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    ModelCategoryFilterDto ObjModelFilterData = new ModelCategoryFilterDto();
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country && parameters.CategoryName.Contains(c.Category)).ToList();
                    var ContextData = context.Where(x => (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear) || parameters.CategoryYear.Contains(x.Year)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake) || parameters.CategoryMake.Contains(x.Make)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel) || parameters.CategoryModel.Contains(x.Model)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategorySubModel) || parameters.CategorySubModel.Contains(x.SubModel))).ToList();
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear))
                    {
                        context.ForEach(c => { ObjModelFilterData.CategoryYearfilter.Add(c.Year); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake))
                    {
                        context.Where(c => parameters.CategoryYear.Contains(c.Year)).ToList().ForEach(c => { ObjModelFilterData.CategoryMakefilter.Add(c.Make); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel))
                    {
                        context.Where(c => (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear) || parameters.CategoryYear.Contains(c.Year)) &&
                        (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake) || parameters.CategoryMake.Contains(c.Make))).ToList().ForEach(c => { ObjModelFilterData.CategoryModelfilter.Add(c.Model); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategorySubModel))
                    {
                        context.Where(c => (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear) || parameters.CategoryYear.Contains(c.Year)) &&
                        (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake) || parameters.CategoryMake.Contains(c.Make)) &&
                        (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel) || parameters.CategoryModel.Contains(c.Make))).ToList().ForEach(c => { ObjModelFilterData.CategorySubModelfilter.Add(c.SubModel); });
                    }
                    ContextData.ForEach(c =>
                    {
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear)) { ObjModelFilterData.CategoryYearfilter.Add(c.Year); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake)) { ObjModelFilterData.CategoryMakefilter.Add(c.Make); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel)) { ObjModelFilterData.CategoryModelfilter.Add(c.Model); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategorySubModel)) { ObjModelFilterData.CategorySubModelfilter.Add(c.SubModel); }
                    });
                    return ObjModelFilterData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetModelFilterDetails" + ex.StackTrace);
                ModelCategoryFilterDto ObjModelFilterData = new ModelCategoryFilterDto();
                return ObjModelFilterData;
            }
        }
    }
}
