namespace MarketShare.Controllers
{
    using log4net;
    using MarketShare.Models.MarketShare;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Configuration;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="AdvancedController" />.
    /// </summary>
    [RoutePrefix("api/AdvancedDashboard")]
    public class AdvancedController : ApiController
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
        /// The GetMarketShareSummaryDetails.
        /// </summary>
        /// <returns>The <see cref="System.Web.Mvc.JsonResult"/>.</returns>
        [Route("DetailGrid")]
        [HttpPost]
        public System.Web.Mvc.JsonResult GetMarketShareSummaryDetails()
        {
            try
            {

                var SummaryDetails = GetPartsPotentialSum();
                return new System.Web.Mvc.JsonResult { Data = SummaryDetails, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetPartsPotentialSum.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{AdvancedPotentialSummaryDto}"/>.</returns>
        public IEnumerable<AdvancedPotentialSummaryDto> GetPartsPotentialSum()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var ObjPartsSummary = db.PartsPotentialCategories.Where(c => c.CountryStr == Country ).Select(x => new AdvancedPotentialSummaryDto() { PartSummaryId = x.ID, PartCategoryName = x.CategoryName, PartDatabasePercentage = x.Database_, PartCount = x.PartCount, MarketSharePercentage = x.MarketShare_, RevCoverage = x.RevCoverage, PartCountryStr = x.CountryStr, PartCurrencyStr = x.CurrencyStr }).ToList();
                    return ObjPartsSummary;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var ObjPartsSummary = Enumerable.Empty<AdvancedPotentialSummaryDto>();
                return ObjPartsSummary;
            }
        }

        /// <summary>
        /// The BrandDistributionDetails.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("DetailBrand")]
        [HttpPost]
        public HttpResponseMessage BrandDistributionDetails()
        {
            try
            {
                var BrandDistribution = GetBrandDistributionDetails();
                return Request.CreateResponse(BrandDistribution);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetBrandDistributionDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{AdvancedDistributionDto}"/>.</returns>
        public IEnumerable<AdvancedDistributionDto> GetBrandDistributionDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var BrandDist = db.PartsPotentialVIOCoverageBrands.Where(c => c.CountryStr == Country).Select(x => new AdvancedDistributionDto() { PartdistName = x.Brand, PartdistPercentage = x.Distribution_, PartdistVIOCoverage = x.VIOCoverage, PartdistId = x.ID, PartdistCountryStr = x.CountryStr, PartdistCurrencyStr = x.CurrencyStr }).ToList();
                    return BrandDist;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var BrandDist = Enumerable.Empty<AdvancedDistributionDto>();
                return BrandDist;
            }
        }

        /// <summary>
        /// The AgeDistributionDetails.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("DetailAge")]
        [HttpPost]
        public HttpResponseMessage AgeDistributionDetails()
        {
            try
            {
                var AgeDistribution = GetAgeDistributionDetails();
                return Request.CreateResponse(AgeDistribution);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetAgeDistributionDetails.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{AdvancedPotentialAgeDto}"/>.</returns>
        public IEnumerable<AdvancedPotentialAgeDto> GetAgeDistributionDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var Agelist = db.PartsPotentialVIOCoverageAges.Where(c => c.CountryStr == Country).Select(x => new AdvancedPotentialAgeDto() { PartAgeDistLevel = x.DistributionLevel, PartAgeDistPercentage = x.AGEDistribution_, PartAgeId = x.ID, PartAgeCountryStr = x.CountryStr, PartAgeCurrencyStr = x.CurrencyStr }).ToList();
                    return Agelist;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var Agelist = Enumerable.Empty<AdvancedPotentialAgeDto>();
                return Agelist;
            }
        }

        /// <summary>
        /// The AdvanceSumTotalDetails.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("AdvanceSumTotal")]
        [HttpPost]
        public HttpResponseMessage AdvanceSumTotalDetails()
        {
            try
            {
                var AdvanceSumTotals = GetAdvanceSumTotalDetails();
                return Request.CreateResponse(AdvanceSumTotals);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " AdvanceSumTotalDetails: " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetCategorySumDetails.
        /// </summary>
        /// <returns>The <see cref="AdvanceSumTotalDto"/>.</returns>
        public IEnumerable<AdvanceSumTotalDto> GetAdvanceSumTotalDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string DashboardCategory = "Advance";
                    var Agelist = db.PartsPotentialDashboards.Where(c => c.CountryStr == Country && c.Dashboard == DashboardCategory).Select(x => new AdvanceSumTotalDto() { PartSumId = x.ID, PartDashboard = x.Dashboard, PartVIOCoverage = x.VIOCoverage, PartMarketPotential = x.MarketPotential, PartGroup = x.PartGroup, PartNumber = x.PartNumber, PartVehicleCount = x.VehicleCount, PartRevCoverage = x.RevenueCoverage, PartMarketSharePercentage = x.MarketShare, PartCountryStr = x.CountryStr, PartCurrencyStr = x.CurrencyStr }).ToList();
                    return Agelist;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " GetAdvanceSumTotalDetails: " + ex.StackTrace);
                var Agelist = Enumerable.Empty<AdvanceSumTotalDto>();
                return Agelist;
            }
        }


    }
}
