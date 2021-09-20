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
    /// Defines the <see cref="CategoryController" />.
    /// </summary>
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
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
        /// The CategoryGridDetails.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("CategoryGrid")]
        [HttpPost]
        public HttpResponseMessage CategoryGridDetails(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var CategoryDetails = GetCategoryGridData(parameters);
                CategoryGridDataDto CategoryGlobalData = new CategoryGridDataDto
                {
                    totalCount = parameters.Totalcount,
                    offset = parameters.offset,
                    count = parameters.count,
                    data = CategoryDetails
                };
                return Request.CreateResponse(CategoryGlobalData);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryGridDetails" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategoryGridData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{LoadParameters}"/>.</param>
        /// <returns>The <see cref="IEnumerable{CategoryViewDto}"/>.</returns>
        public IEnumerable<CategoryModelDto> GetCategoryGridData(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjCategoryData = (from x in context
                                           where parameters.filterObj.CategoryName.Count == 0 || parameters.filterObj.CategoryName.Contains(x.Category)
                                           select (new CategoryModelDto
                                           {
                                               CategoryName = x.Category,
                                               CategoryPartNumber = x.PartNumber,
                                               CategoryYear = x.Year,
                                               CategoryMake = x.Make,
                                               CategoryModel = x.Model,
                                               CategorySubModel = x.SubModel,
                                               CategoryVIODemand = x.VIODemand,
                                               CategoryMarketPotential = x.MarketPotential,
                                               CategoryCustomerSales = x.Customer_12M_Sales,
                                               CategoryCustomerDistribution = x.Customer_12M_Sales_Distribution
                                           })).OrderBy(x => x.CategoryPartNumber).ToList();
                    parameters.Totalcount = ObjCategoryData.Count;
                    var ObjCategoryGridData = parameters.offset != 0 ? ObjCategoryData.Skip(parameters.offset).Take(parameters.count) : ObjCategoryData;
                    return ObjCategoryGridData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategoryGridData" + ex.StackTrace);
                var ObjCategoryData = Enumerable.Empty<CategoryModelDto>();
                return ObjCategoryData;
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
                CategoryModel ObjCategory = new CategoryModel
                {
                    CategoryName = JCategory
                };
                return Request.CreateResponse(ObjCategory);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategoryData in Category" + ex.StackTrace);
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
                    var ObjCategoryData = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).GroupBy(e => e.Category).Select(x => new CategoryDto() { CategoryName = x.Key }).ToList();
                    return ObjCategoryData;
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
        /// The CategorySumData.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("CategoryTotal")]
        [HttpPost]
        public HttpResponseMessage CategorySumData(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                LoadParameter parameters = JsonConvert.DeserializeObject<LoadParameter>(jsonString);
                var CategoryDetails = GetCategorySumDetails(parameters);
                return Request.CreateResponse(CategoryDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "CategorySumData" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategorySumDetails.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="LoadParameter"/>.</param>
        /// <returns>The <see cref="CategoryTotalDto"/>.</returns>
        public IEnumerable<CategoryTotalDto> GetCategorySumDetails(LoadParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjModelTotalData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            group a by new {  a.Year, a.Make, a.Model, a.SubModel, a.VIODemand } into g
                                            select new CategoryModelDto
                                            {
                                                //CategoryName = g.Key.Category,
                                                CategoryVIODemand = g.Key.VIODemand,
                                            };
                    var ObjModelMarketPotentialData = from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            group a by new { a.Year, a.Make, a.Model, a.SubModel,  a.MarketPotential } into g
                                            select new CategoryModelDto
                                            {
                                                CategoryMarketPotential = g.Key.MarketPotential,
                                            };
                    var ObjModelPartData = (from a in context
                                            where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                            select new CategoryModelDto
                                            {
                                                //CategoryName = a.Category,
                                                CategoryPartNumber = a.PartNumber
                                            });
                    var ObjModelVechileData = from a in context
                                              where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                              group a by new { a.Year, a.Make, a.Model, a.SubModel } into g
                                              select new CategoryModelDto
                                              {
                                                  //CategoryName = g.Key.Category,
                                                  CategoryYear = g.Key.Year,
                                                  CategoryMake = g.Key.Make,
                                                  CategoryModel = g.Key.Model,
                                                  CategorySubModel = g.Key.SubModel
                                              };

                    var ObjModelCustSalesData = from a in context
                                                where (parameters.CategoryName.Count == 0) || (parameters.CategoryName.Contains(a.Category))
                                                group a by new {  a.PartNumber, a.Customer_12M_Sales } into g
                                                select new CategoryModelDto
                                                {
                                                    //CategoryName = g.Key.Category,
                                                    CategoryPartNumber = g.Key.PartNumber,
                                                    CategoryCustomerSales = g.Key.Customer_12M_Sales,
                                                };
                    var ObjModelSumData = ObjModelTotalData.Concat(ObjModelMarketPotentialData).Concat(ObjModelPartData).Concat(ObjModelVechileData).Concat(ObjModelCustSalesData);
                    var ObjModleAggregateData = from a in ObjModelSumData
                                                group a by 0 into g
                                                select new CategoryTotalDto
                                                {
                                                    CategoryVIODemand = g.Sum(x => x.CategoryVIODemand),
                                                    CategoryMarketPotential = g.Sum(x => x.CategoryMarketPotential),
                                                    CateogryPartCount = g.Where(x => x.CategoryPartNumber != null).Select(x => x.CategoryPartNumber).Distinct().Count(),
                                                    CategoryVehicle = g.Where(x => x.CategoryYear != null && x.CategoryMake != null && x.CategoryModel != null && x.CategorySubModel != null).Select(x => new {  x.CategoryYear, x.CategoryMake, x.CategoryModel, x.CategorySubModel }).Count(),
                                                    CategoryCustomerSales = g.Sum(x => x.CategoryCustomerSales),
                                                    CategoryMarketShare = g.Sum(x => x.CategoryCustomerSales) > g.Sum(x => x.CategoryMarketPotential) ? 100 : ((decimal)g.Sum(x => x.CategoryCustomerSales) / (decimal)g.Sum(x => x.CategoryMarketPotential)) * 100
                                                };
                    return ObjModleAggregateData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetCategorySumDetails" + ex.StackTrace);
                var ObjCategoryData = Enumerable.Empty<CategoryTotalDto>();
                return ObjCategoryData;
            }
        }
    }
}
