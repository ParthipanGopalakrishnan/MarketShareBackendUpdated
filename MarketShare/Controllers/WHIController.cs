namespace MarketShare.Controllers
{
    using log4net;
    using MarketShare.Models.MarketShare;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="WhiController" />.
    /// </summary>
    [RoutePrefix("api/WHI")]
    public class WhiController : ApiController
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
        [Route("VehicleData")]
        [HttpPost]
        public HttpResponseMessage GetVehicleDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var VehicleDetails = GetVehicleViewData(parameters);
                WhiViewDto ModelGlobalData = new WhiViewDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = VehicleDetails
                };
                return Request.CreateResponse(ModelGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehicleDetails in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetModelViewData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{ModelViewDto}"/>.</returns>
        public IEnumerable<WhiDataDto> GetVehicleViewData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjYearData = GetJsonParameters(parameters.filterObj.VehiclesYear);
                    string ObjMakeData = GetJsonParameters(parameters.filterObj.VehiclesMake);
                    string ObjModelData = GetJsonParameters(parameters.filterObj.VehiclesModel);
                    string ObjEngineData = GetJsonParameters(parameters.filterObj.VehiclesEngine);
                    var ObjVehicleData = db.usp_getVehiclesdata(Country, dbString, ObjYearData, ObjMakeData, ObjModelData, ObjEngineData).
                        Select(x => new WhiDataDto()
                        {
                            //WhiCustomerPartNumber = x.CustomerPartNumber,
                            WhiPartNumber = x.PartNumber,
                            WhiDescription = x.Description,
                            WhiCategory = x.CategoryName,
                            WhiBrand = x.Brand,
                            //WhiAvgPrice = x.AvgPrice,
                            //WhiAvgCorePrice = x.AvgCorePrice,
                            WhiPriceDate = x.PriceDate,
                            //WhiDistributor = x.Distributor,
                            //WhiMedianPrice = x.MedianPrice,
                            //WhiMinPrice = x.MinPrice,
                            //WhiMaxPrice = x.MaxPrice,
                            //WhiStandardDeviationPrice = x.StandardDeviationPrice,
                            //WhiVariancePrice = x.VariancePrice,
                            //WhiSumPrice = x.SumPrice,
                            //WhiTransactionCount = x.TransactionCount,
                            WhiSubCategory = x.SubCategoryName,
                            WhiPartTerminology = x.PartTerminologyName,
                            WhiYearID = x.YearID,
                            WhiMake = x.MakeName,
                            WhiModel = x.ModelName,
                            WhiEngine = x.Engine,
                        }).ToList();
                    parameters.Totalcount = ObjVehicleData.Count;
                    var ObjVehicleDetails = parameters.offset != 0 ? ObjVehicleData.Skip(parameters.offset).Take(parameters.count) : ObjVehicleData;
                    return ObjVehicleDetails;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehicleViewData in WHIController" + ex.StackTrace);
                var ObjVehicleDetails = Enumerable.Empty<WhiDataDto>();
                return ObjVehicleDetails;
            }
        }

        /// <summary>
        /// The CategoryData.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("VehicleYear")]
        [HttpPost]
        public HttpResponseMessage VehicleYearData()
        {
            try
            {

                var YearDetails = GetVehicleYearDetails();
                List<int?> JYear = new List<int?>();
                YearDetails.ToList().ForEach(c => JYear.Add(c.VehicleYear));
                VehicleYearFilterDto VehicleYear = new VehicleYearFilterDto
                {
                    VehicleYear = JYear
                };
                return Request.CreateResponse(VehicleYear);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "VehicleYearData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehicleYearDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{VehicleYearDto}"/>.</returns>
        public IEnumerable<VehicleYearDto> GetVehicleYearDetails()
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    var ObjYearData = db.usp_getVehiclesYear(Country, dbString).Select(x => new VehicleYearDto() { VehicleYear = x.Value }).ToList();
                    return ObjYearData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehicleYearDetails in WHIController" + ex.StackTrace);
                var ObjYearData = Enumerable.Empty<VehicleYearDto>();
                return ObjYearData;
            }
        }

        /// <summary>
        /// The VehiclesMakeData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("VehicleMake")]
        [HttpPost]
        public HttpResponseMessage VehiclesMakeData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var MakeDetails = GetVehiclesMakeDetails(parameter);
                List<string> JMake = new List<string>();
                MakeDetails.ToList().ForEach(c => JMake.Add(c.VehicleMake));
                VehicleMakeFilterDto VehicleMake = new VehicleMakeFilterDto
                {
                    VehicleMake = JMake
                };
                return Request.CreateResponse(VehicleMake);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "VehiclesMakeData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<VehicleMakeDto> GetVehiclesMakeDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjYearData = GetJsonParameters(parameters.VehiclesYear);
                    var ObjMakeData = db.usp_getVehiclesMake(Country, dbString, ObjYearData).Select(x => new VehicleMakeDto() { VehicleMake = x }).ToList();
                    return ObjMakeData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehiclesMakeDetails in WHIController" + ex.StackTrace);
                var ObjMakeData = Enumerable.Empty<VehicleMakeDto>();
                return ObjMakeData;
            }
        }

        /// <summary>
        /// The VehiclesModelData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("VehicleModel")]
        [HttpPost]
        public HttpResponseMessage VehiclesModelData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var ModelDetails = GetVehiclesModelDetails(parameter);
                List<string> JModel = new List<string>();
                ModelDetails.ToList().ForEach(c => JModel.Add(c.VehicleModel));
                VehicleModelFilterDto VehicleModel = new VehicleModelFilterDto
                {
                    VehicleModel = JModel
                };
                return Request.CreateResponse(VehicleModel);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "VehiclesModelData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<VehicleModelDto> GetVehiclesModelDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjYearData = GetJsonParameters(parameters.VehiclesYear);
                    string ObjMakeData = GetJsonParameters(parameters.VehiclesMake);
                    var ObjModelData = db.usp_getVehiclesModel(Country, dbString, ObjYearData, ObjMakeData).Select(x => new VehicleModelDto() { VehicleModel = x }).ToList();
                    return ObjModelData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehiclesModelDetails in WHIController" + ex.StackTrace);
                var ObjModelData = Enumerable.Empty<VehicleModelDto>();
                return ObjModelData;
            }
        }

        /// <summary>
        /// The VehiclesModelData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("VehicleEngine")]
        [HttpPost]
        public HttpResponseMessage VehiclesEngineData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var EngineDetails = GetVehiclesEngineDetails(parameter);
                List<string> JEngine = new List<string>();
                EngineDetails.ToList().ForEach(c => JEngine.Add(c.VehicleEngine));
                VehicleEngineFilterDto VehicleEngine = new VehicleEngineFilterDto
                {
                    VehicleEngine = JEngine
                };
                return Request.CreateResponse(VehicleEngine);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "VehiclesEngineData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<VehicleEngineDto> GetVehiclesEngineDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjYearData = GetJsonParameters(parameters.VehiclesYear);
                    string ObjMakeData = GetJsonParameters(parameters.VehiclesMake);
                    string ObjModelData = GetJsonParameters(parameters.VehiclesModel);
                    var ObjEngineData = db.usp_getVehiclesEngine(Country, dbString, ObjYearData, ObjMakeData, ObjModelData).Select(x => new VehicleEngineDto() { VehicleEngine = x }).ToList();
                    return ObjEngineData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetVehiclesEngineDetails in WHIController" + ex.StackTrace);
                var ObjEngineData = Enumerable.Empty<VehicleEngineDto>();
                return ObjEngineData;
            }
        }

        /// <summary>
        /// The GetJsonParameters.
        /// </summary>
        /// <param name="ObjData">The ObjData<see cref="IList{string}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetJsonParameters(IList<string> ObjData)
        {
            StringBuilder sbSql = new StringBuilder();
            if (ObjData.Count > 0)
            {
                for (int i = 0; i < ObjData.Count; i++)
                {
                    if (i != 0)
                        sbSql.Append(";;");
                    sbSql.Append(ObjData[i]);
                }
            }
            return string.IsNullOrEmpty(sbSql.ToString()) ? null : sbSql.ToString();
        }

        /// <summary>
        /// The CategoryData.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("Brand")]
        [HttpPost]
        public HttpResponseMessage BrandData()
        {
            try
            {

                var BrandDetails = GetBrandData();
                List<string> JBrand = new List<string>();
                BrandDetails.ToList().ForEach(c => JBrand.Add(c.Brands));
                BrandFilterDto BrandData = new BrandFilterDto
                {
                    Brands = JBrand
                };
                return Request.CreateResponse(BrandData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "BrandData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehicleYearDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{VehicleYearDto}"/>.</returns>
        public IEnumerable<BrandDto> GetBrandData()
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    var ObjBrandData = db.usp_getBrands(Country, dbString).Select(x => new BrandDto() { Brands = x }).ToList();
                    return ObjBrandData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetBrandDetails in WHIController" + ex.StackTrace);
                var ObjBrandData = Enumerable.Empty<BrandDto>();
                return ObjBrandData;
            }
        }

        /// <summary>
        /// The GetBrandDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("BrandData")]
        [HttpPost]
        public HttpResponseMessage GetBrandDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var BrandDetails = GetBrandViewData(parameters);
                WhiViewDto ModelGlobalData = new WhiViewDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = BrandDetails
                };
                return Request.CreateResponse(ModelGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetBrandDetails in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetModelViewData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{ModelViewDto}"/>.</returns>
        public IEnumerable<WhiDataDto> GetBrandViewData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjBrandDatas = GetJsonParameters(parameters.filterObj.Brands);
                    var ObjBrandData = db.usp_getBrandsdata(Country, dbString, ObjBrandDatas).
                        Select(x => new WhiDataDto()
                        {
                            WhiCustomerPartNumber = x.CustomerPartNumber,
                            WhiPartNumber = x.PartNumber,
                            WhiDescription = x.Description,
                            WhiCategory = x.CategoryName,
                            WhiBrand = x.Brand,
                            WhiAvgPrice = x.AvgPrice,
                            WhiAvgCorePrice = x.AvgCorePrice,
                            WhiPriceDate = x.PriceDate,
                            WhiDistributor = x.Distributor,
                            WhiMedianPrice = x.MedianPrice,
                            WhiMinPrice = x.MinPrice,
                            WhiMaxPrice = x.MaxPrice,
                            WhiStandardDeviationPrice = x.StandardDeviationPrice,
                            WhiVariancePrice = x.VariancePrice,
                            WhiSumPrice = x.SumPrice,
                            WhiTransactionCount = x.TransactionCount,
                            WhiSubCategory = x.SubCategoryName,
                            WhiPartTerminology = x.PartTerminologyName,
                            WhiYearID = x.YearID,
                            WhiMake = x.MakeName,
                            WhiModel = x.ModelName,
                            WhiEngine = x.Engine,
                        }).ToList();

                    Log.Info("GetBrandViewData - Country:" + Country + " dbstring:" + dbString + " output:" + ObjBrandData);
                    parameters.Totalcount = ObjBrandData.Count;
                    var ObjBrandDetails = parameters.offset != 0 ? ObjBrandData.Skip(parameters.offset).Take(parameters.count) : ObjBrandData;
                    return ObjBrandDetails;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetBrandViewData in WHIController" + ex.StackTrace);
                var ObjBrandDetails = Enumerable.Empty<WhiDataDto>();
                return ObjBrandDetails;
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
                CategoryDetails.ToList().ForEach(c => JCategory.Add(c.Category));
                CategoryFilterDto Category = new CategoryFilterDto
                {
                    Category = JCategory
                };
                return Request.CreateResponse(Category);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehicleYearDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{VehicleYearDto}"/>.</returns>
        public IEnumerable<CategoryDataDto> GetCategoryDetails()
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    var ObjCategoryData = db.usp_getCategory(Country, dbString).Select(x => new CategoryDataDto() { Category = x }).ToList();
                    return ObjCategoryData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryDetails in WHIController" + ex.StackTrace);
                var ObjCategoryData = Enumerable.Empty<CategoryDataDto>();
                return ObjCategoryData;
            }
        }

        /// <summary>
        /// The VehiclesMakeData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("SubCategory")]
        [HttpPost]
        public HttpResponseMessage SubCategoryData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var SubCategoryDetails = GetSubCategoryDetails(parameter);
                List<string> JSubCategory = new List<string>();
                SubCategoryDetails.ToList().ForEach(c => JSubCategory.Add(c.SubCategory));
                SubCategoryFilterDto SubCategory = new SubCategoryFilterDto
                {
                    SubCategory = JSubCategory
                };
                return Request.CreateResponse(SubCategory);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "SubCategoryData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<SubCategoryDataDto> GetSubCategoryDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjCategoryData = GetJsonParameters(parameters.CategoryName);
                    var ObjSubCategoryData = db.usp_getSubCategory(Country, dbString, ObjCategoryData).Select(x => new SubCategoryDataDto() { SubCategory = x }).ToList();
                    return ObjSubCategoryData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetSubCategoryDetails in WHIController" + ex.StackTrace);
                var ObjSubCategoryData = Enumerable.Empty<SubCategoryDataDto>();
                return ObjSubCategoryData;
            }
        }

        /// <summary>
        /// The PartTerminologyData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartTerminology")]
        [HttpPost]
        public HttpResponseMessage PartTerminologyData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var PartTerminologyDetails = GetPartTerminologyDetails(parameter);
                List<string> JPartTerminology = new List<string>();
                PartTerminologyDetails.ToList().ForEach(c => JPartTerminology.Add(c.PartTerminology));
                PartTerminologyFilterDto SubCategory = new PartTerminologyFilterDto
                {
                    PartTerminology = JPartTerminology
                };
                return Request.CreateResponse(SubCategory);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "PartTerminologyData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<PartTerminologyDataDto> GetPartTerminologyDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjCategoryData = GetJsonParameters(parameters.CategoryName);
                    string ObjSubCategoryData = GetJsonParameters(parameters.SubCategory);
                    var ObjPartTerminology = db.usp_getPartTerminology(Country, dbString, ObjCategoryData, ObjSubCategoryData).Select(x => new PartTerminologyDataDto() { PartTerminology = x }).ToList();
                    return ObjPartTerminology;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetPartTerminologyDetails in WHIController" + ex.StackTrace);
                var ObjSubCategoryData = Enumerable.Empty<PartTerminologyDataDto>();
                return ObjSubCategoryData;
            }
        }

        /// <summary>
        /// The CategoryBrandData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("CategoryBrand")]
        [HttpPost]
        public HttpResponseMessage CategoryBrandData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameter = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var CategoryBrandDetails = GetCategoryBrandDetails(parameter);
                List<string> JCategoryBrand = new List<string>();
                CategoryBrandDetails.ToList().ForEach(c => JCategoryBrand.Add(c.CategoryBrand));
                CategoryBrandFilterDto CategoryBrand = new CategoryBrandFilterDto
                {
                    CategoryBrand = JCategoryBrand
                };
                return Request.CreateResponse(CategoryBrand);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryBrandData in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetVehiclesMakeDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="IEnumerable{VehicleMakeDto}"/>.</returns>
        public IEnumerable<CategoryBrandDataDto> GetCategoryBrandDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjCategoryData = GetJsonParameters(parameters.CategoryName);
                    string ObjSubCategoryData = GetJsonParameters(parameters.SubCategory);
                    string ObjPartTerminology = GetJsonParameters(parameters.PartTerminology);
                    var ObjCategoryBrand = db.usp_getAAIABrand(Country, dbString, ObjCategoryData, ObjSubCategoryData, ObjPartTerminology).Select(x => new CategoryBrandDataDto() { CategoryBrand = x }).ToList();
                    return ObjCategoryBrand;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryBrandDetails in WHIController" + ex.StackTrace);
                var ObjSubCategoryData = Enumerable.Empty<CategoryBrandDataDto>();
                return ObjSubCategoryData;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("CategoryData")]
        [HttpPost]
        public HttpResponseMessage GetCategoryDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var CategoryDetails = GetCategoryViewData(parameters);
                WhiViewDto WHIGlobalData = new WhiViewDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = CategoryDetails
                };
                return Request.CreateResponse(WHIGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryDetails in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetModelViewData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{ModelViewDto}"/>.</returns>
        public IEnumerable<WhiDataDto> GetCategoryViewData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjCategoryData = GetJsonParameters(parameters.filterObj.CategoryName);
                    string ObjSubCategoryData = GetJsonParameters(parameters.filterObj.SubCategory);
                    string ObjPartTerminologyData = GetJsonParameters(parameters.filterObj.PartTerminology);
                    string ObjBrandData = GetJsonParameters(parameters.filterObj.CategoryBrand);
                    var ObjCategory = db.usp_getCategorydata(Country, dbString, ObjCategoryData, ObjSubCategoryData, ObjPartTerminologyData, ObjBrandData).
                        Select(x => new WhiDataDto()
                        {
                            WhiCustomerPartNumber = x.CustomerPartNumber,
                            WhiPartNumber = x.PartNumber,
                            WhiDescription = x.Description,
                            WhiCategory = x.CategoryName,
                            WhiBrand = x.Brand,
                            WhiAvgPrice = x.AvgPrice,
                            WhiAvgCorePrice = x.AvgCorePrice,
                            WhiPriceDate = x.PriceDate,
                            WhiDistributor = x.Distributor,
                            WhiMedianPrice = x.MedianPrice,
                            WhiMinPrice = x.MinPrice,
                            WhiMaxPrice = x.MaxPrice,
                            WhiStandardDeviationPrice = x.StandardDeviationPrice,
                            WhiVariancePrice = x.VariancePrice,
                            WhiSumPrice = x.SumPrice,
                            WhiTransactionCount = x.TransactionCount,
                            WhiSubCategory = x.SubCategoryName,
                            WhiPartTerminology = x.PartTerminologyName,
                            WhiYearID = x.YearID,
                            WhiMake = x.MakeName,
                            WhiModel = x.ModelName,
                            WhiEngine = x.Engine,
                        }).ToList();
                    Log.Info("GetCategoryViewData - Country:" + Country + " dbstring:" + dbString + " output:" + ObjCategory);
                    parameters.Totalcount = ObjCategory.Count;
                    var ObjCategoryDetails = parameters.offset != 0 ? ObjCategory.Skip(parameters.offset).Take(parameters.count) : ObjCategory;
                    return ObjCategoryDetails;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryViewData in WHIController" + ex.StackTrace);
                var ObjCategoryDetails = Enumerable.Empty<WhiDataDto>();
                return ObjCategoryDetails;
            }
        }

        /// <summary>
        /// The GetCategoryDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("InterChangeData")]
        [HttpPost]
        public HttpResponseMessage GetInterChangeDataDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var InterChangeDataDetails = GetInterChangeViewData(parameters);
                Log.Info("GetInterChangeDataDetails - output:" + InterChangeDataDetails.ToString());
                WhiViewDto WHIGlobalData = new WhiViewDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = InterChangeDataDetails
                };
                return Request.CreateResponse(WHIGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetInterChangeDataDetails in WHIController" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetModelViewData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{ModelViewDto}"/>.</returns>
        public IEnumerable<WhiDataDto> GetInterChangeViewData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetDBContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    string ObjPartNumberData = GetJsonParameters(parameters.filterObj.PartNumber);
                    string ObjsearchScopeData = parameters.filterObj.SearchScope.ToString();
                    var ObjPartNumber = db.usp_getPartNumbersdata(Country, dbString, ObjPartNumberData, ObjsearchScopeData).
                        Select(x => new WhiDataDto()
                        {
                            WhiCustomerPartNumber = x.CustomerPartNumber,
                            WhiPartNumber = x.PartNumber,
                            WhiDescription = x.Description,
                            WhiCategory = x.CategoryName,
                            WhiBrand = x.Brand,
                            WhiAvgPrice = x.AvgPrice,
                            WhiAvgCorePrice = x.AvgCorePrice,
                            WhiPriceDate = x.PriceDate,
                            WhiDistributor = x.Distributor,
                            WhiMedianPrice = x.MedianPrice,
                            WhiMinPrice = x.MinPrice,
                            WhiMaxPrice = x.MaxPrice,
                            WhiStandardDeviationPrice = x.StandardDeviationPrice,
                            WhiVariancePrice = x.VariancePrice,
                            WhiSumPrice = x.SumPrice,
                            WhiTransactionCount = x.TransactionCount,
                            WhiSubCategory = x.SubCategoryName,
                            WhiPartTerminology = x.PartTerminologyName,
                            WhiYearID = x.YearID,
                            WhiMake = x.MakeName,
                            WhiModel = x.ModelName,
                            WhiEngine = x.Engine,
                        }).ToList();
                    Log.Info("GetInterChangeViewData - Country:" + Country + " dbstring:" + dbString );
                    parameters.Totalcount = ObjPartNumber.Count;
                    var ObjPartNumberDetails = parameters.offset != 0 ? ObjPartNumber.Skip(parameters.offset).Take(parameters.count) : ObjPartNumber;
                    return ObjPartNumberDetails;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetInterChangeViewData in WHIController" + ex.StackTrace);
                var ObjPartNumberDetails = Enumerable.Empty<WhiDataDto>();
                return ObjPartNumberDetails;
            }
        }
    }
}
