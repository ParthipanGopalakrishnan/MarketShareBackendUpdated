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
    /// Defines the <see cref="StandardController" />.
    /// </summary>
    [RoutePrefix("api/Standard")]
    public class StandardController : ApiController
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
        /// The GetStandarGloabalDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("GlobalGrid")]
        [HttpPost]
        public HttpResponseMessage GetStandarGloabalDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var StandardGlobalDetails = GetGlobalDatas(parameter);
                StandardGridDataDto StandardGlobalData = new StandardGridDataDto
                {
                    totalCount = parameter.Totalcount,
                    offset = parameter.offset,
                    count = parameter.count,
                    data = StandardGlobalDetails
                };
                return Request.CreateResponse(StandardGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetGlobalDatas.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{StandardGlobalDto}"/>.</returns>
        public IEnumerable<StandardGlobalDto> GetGlobalDatas(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewGlobals.Where(c => c.CountryStr == Country).ToList();
                    var ObjStandardGlobalData = (from a in context
                                                 where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(a.Category)
                                                 where parameters.filterObj.CategoryYear.Count == 0 || parameters.filterObj.CategoryYear.Contains(a.Year)
                                                 where parameters.filterObj.CategoryMake.Count == 0 || parameters.filterObj.CategoryMake.Contains(a.Make)
                                                 where parameters.filterObj.CategoryModel.Count == 0 || parameters.filterObj.CategoryModel.Contains(a.Model)
                                                 select new StandardGlobalDto
                                                 {
                                                     GlobalId = a.ID,
                                                     GlobalCategory = a.Category,
                                                     GlobalMake = a.Make,
                                                     GlobalYear = a.Year,
                                                     GlobalModel = a.Model,
                                                     GlobalVIODemand = a.VIODemand,
                                                     GlobalMarketPotential = a.MarketPotential,
                                                     GlobalCountryStr = a.CountryStr,
                                                     GlobalCurrencyStr = a.CurrencyStr
                                                 }).ToList();
                    parameters.Totalcount = ObjStandardGlobalData.Count;
                    var ObjStandardGriddata = parameters.offset != 0 ? ObjStandardGlobalData.Skip(parameters.offset).Take(parameters.count) : ObjStandardGlobalData;
                    return ObjStandardGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var ObjStandardGlobalData = Enumerable.Empty<StandardGlobalDto>();
                return ObjStandardGlobalData;
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
                StandardCategoryDto StandardCategory = new StandardCategoryDto
                {
                    CategoryName = JCategory
                };
                return Request.CreateResponse(StandardCategory);
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
                    var ObjStandardCategoryData = db.ModelViewGlobals.Where(c => c.CountryStr == Country).GroupBy(e => e.Category).Select(x => new CategoryDto() { CategoryName = x.Key }).ToList();
                    return ObjStandardCategoryData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryDetails" + ex.StackTrace);
                var ObjStandardCategoryData = Enumerable.Empty<CategoryDto>();
                return ObjStandardCategoryData;
            }
        }

        /// <summary>
        /// The CategorySumData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("StandardTotal")]
        [HttpPost]
        public HttpResponseMessage StandardSumData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var StandardSumDetails = GetStandardSumDetails(parameter);
                return Request.CreateResponse(StandardSumDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "StandardSumData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategorySumDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="CategoryTotalDto"/>.</returns>
        public IEnumerable<StandardTotalDto> GetStandardSumDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewGlobals.Where(c => c.CountryStr == Country).ToList();
                    var ObjStandardSumData = from a in context
                                             where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                             where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                             where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                             where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                             group a by 0 into g
                                             select new StandardTotalDto
                                             {
                                                 CategoryVIODemand = g.Sum(x => x.VIODemand),
                                                 CategoryMarketPotential = g.Sum(x => x.MarketPotential)
                                             };
                    return ObjStandardSumData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetStandardSumDetails" + ex.StackTrace);
                var ObjStandardSumData = Enumerable.Empty<StandardTotalDto>();
                return ObjStandardSumData;
            }
        }

        /// <summary>
        /// The CategoryData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("StandardFilter")]
        [HttpPost]
        public HttpResponseMessage StandardFilterData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var StandardDetails = GetStandardFilterDetail(parameter);
                StandardCategoryFilterdto StandarFilterData = new StandardCategoryFilterdto
                {
                    CategoryYearfilter = StandardDetails.CategoryYearfilter.Distinct().ToList(),
                    CategoryMakefilter = StandardDetails.CategoryMakefilter.Distinct().ToList(),
                    CategoryModelfilter = StandardDetails.CategoryModelfilter.Distinct().ToList()
                };
                return Request.CreateResponse(StandarFilterData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryFilterData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetStandardFilterDetail.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{StandardFilterDto}"/>.</returns>
        public StandardCategoryFilterdto GetStandardFilterDetail(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    StandardCategoryFilterdto ObjStandardFilterData = new StandardCategoryFilterdto();
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewGlobals.Where(c => c.CountryStr == Country && parameters.CategoryName.Contains(c.Category)).ToList();
                    var ContextData = context.Where(x => (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear) || parameters.CategoryYear.Contains(x.Year)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake) || parameters.CategoryMake.Contains(x.Make)) &&
                      (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel) || parameters.CategoryModel.Contains(x.Model))).ToList();
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear))
                    {
                        context.ForEach(c => { ObjStandardFilterData.CategoryYearfilter.Add(c.Year); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake))
                    {
                        context.Where(c => parameters.CategoryYear.Contains(c.Year)).ToList().ForEach(c => { ObjStandardFilterData.CategoryMakefilter.Add(c.Make); });
                    }
                    if (parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel))
                    {
                        context.Where(c => (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear) || parameters.CategoryYear.Contains(c.Year)) &&
                        (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake) || parameters.CategoryMake.Contains(c.Make))).ToList().ForEach(c => { ObjStandardFilterData.CategoryModelfilter.Add(c.Model); });
                    }
                    ContextData.ForEach(c =>
                    {
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryYear)) { ObjStandardFilterData.CategoryYearfilter.Add(c.Year); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryMake)) { ObjStandardFilterData.CategoryMakefilter.Add(c.Make); }
                        if (!parameters.filterSeq.Contains(MarketShareFieldConstant.CategoryModel)) { ObjStandardFilterData.CategoryModelfilter.Add(c.Model); }
                    });
                    return ObjStandardFilterData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryFilterDetails" + ex.StackTrace);
                StandardCategoryFilterdto ObjStandardFilterData = new StandardCategoryFilterdto();
                return ObjStandardFilterData;
            }
        }

        /// <summary>
        /// The GetStandarAgeDistDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("StandardAgeDistribution")]
        [HttpPost]
        public HttpResponseMessage GetStandarAgeDistDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var GlobalDetails = GetAgeDistData(parameter);
                return Request.CreateResponse(GlobalDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetAgeDistData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{StandardAgeDistDto}"/>.</returns>
        public IEnumerable<StandardAgeDistDto> GetAgeDistData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    //var ObjAgeDistData = db.ModelViewAgeDistributions.Where(c => c.CountryStr == Country && ((parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(c.Category))) && ((parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(c.Year)))).Select(x => new StandardAgeDistDto() { AgeDistId = x.ID, AgeDistYear = x.Year, AgeDistMarketPotential = x.MarketPotential, AgeDistVIODemand = x.VIODemand, AgeDistCountryStr = x.CountryStr, AgeDistCurrencyStr = x.CurrencyStr, AgeCategory = x.Category }).ToList();
                    //return ObjAgeDistData;

                    var context = db.ModelViewGlobals.Where(c => c.CountryStr == Country).ToList();

                    var ObjAgeDistData = (from a in context
                                             where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                             where (parameters.CategoryYear.Count == 0) || (parameters.CategoryYear.Contains(a.Year))
                                             where (parameters.CategoryMake.Count == 0) || (parameters.CategoryMake.Contains(a.Make))
                                             where (parameters.CategoryModel.Count == 0) || (parameters.CategoryModel.Contains(a.Model))
                                             group a by new { a.Category, a.Year } into g
                                             select new StandardAgeDistDto
                                             {
                                                 AgeDistYear = g.Key.Year,
                                                 AgeCategory = g.Key.Category,
                                                 AgeDistVIODemand = g.Sum(x => x.VIODemand),
                                                 AgeDistMarketPotential = g.Sum(x => x.MarketPotential)
                                             }).OrderBy(x => x.AgeDistYear).ToList(); 
                    return ObjAgeDistData;

                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var ObjAgeDistData = Enumerable.Empty<StandardAgeDistDto>();
                return ObjAgeDistData;
            }
        }
    }
}
