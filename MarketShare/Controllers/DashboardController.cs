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
    /// Defines the <see cref="DashboardController" />.
    /// </summary>
    [RoutePrefix("api/StandardDashboard")]
    public class DashboardController : ApiController
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
        /// <returns>The <see cref="IEnumerable{PartsPotentialSummaryDto}"/>.</returns>
        public IEnumerable<PartsPotentialSummaryDto> GetPartsPotentialSum()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var ObjPartsSummary = db.PartsPotentialStandardCategories.Where(c => c.CountryStr == Country).Select(x => new PartsPotentialSummaryDto() { PartSummaryId = x.ID, PartCategoryName = x.CategoryName, PartMarketPotential = x.MarketPotential, PartDatabasePercentage = x.MarketPotentialDatabase_, PartAgeCountryStr = x.CountryStr, PartAgeCurrencyStr = x.CurrencyStr }).ToList();
                    return ObjPartsSummary;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var ObjPartsSummary = Enumerable.Empty<PartsPotentialSummaryDto>();
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
        /// <returns>The <see cref="IEnumerable{PartsPotentialDistributionDto}"/>.</returns>
        public IEnumerable<PartsPotentialDistributionDto> GetBrandDistributionDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var BrandDist = db.PartsPotentialVIOCoverageBrandGlobals.Where(c => c.CountryStr == Country).Select(x => new PartsPotentialDistributionDto() { PartdistName = x.Brand, PartdistPercentage = x.Distribution_, PartdistVIOCoverage = x.VIOCoverage, PartdistId = x.ID, PartdistCountryStr = x.CountryStr, PartdistCurrencyStr = x.CurrencyStr }).ToList();
                    return BrandDist;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var BrandDist = Enumerable.Empty<PartsPotentialDistributionDto>();
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
        /// <returns>The <see cref="IEnumerable{PartsPotentialAgeDto}"/>.</returns>
        public IEnumerable<PartsPotentialAgeDto> GetAgeDistributionDetails()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    var Agelist = db.PartsPotentialVIOCoverageAgeGlobals.Where(c => c.CountryStr == Country).Select(x => new PartsPotentialAgeDto() { PartAgeDistLevel = x.DistributionLevel, PartAgeDistPercentage = x.AGEDistribution_, PartAgeId = x.ID, PartAgeCountryStr = x.CountryStr, PartAgeCurrencyStr = x.CurrencyStr }).ToList();
                    return Agelist;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " " + ex.StackTrace);
                var Agelist = Enumerable.Empty<PartsPotentialAgeDto>();
                return Agelist;
            }
        }
    }
}
