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
    /// Defines the <see cref="WHIAppConfigController" />.
    /// </summary>
    [RoutePrefix("api/WHIAppConfigData")]
    public class WHIAppConfigController : ApiController
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
        /// The GetWHISearchBy.
        /// </summary>        
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("WHISearchBy")]
        [HttpPost]
        public HttpResponseMessage GetWHISearchBy()
        {
            try
            {
                var PartNumberDetails = GetWHISearchByData();
                WHIAppSearchByConfigDto WHISearchBy = new WHIAppSearchByConfigDto
                {
                    SearchBy = PartNumberDetails
                };
                return Request.CreateResponse(WHISearchBy);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetWHISearchBy" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetWHISearchByData.
        /// </summary>        
        /// <returns>The <see cref="IEnumerable{WHIAppSearchByConfig}"/>.</returns>
        public IEnumerable<WHIAppSearchByConfig> GetWHISearchByData()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    Log.Info("GetWHISearchByData - Country:" + Country + " dbstring:" + dbString);
                    //var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjPartData = (from ac in db.WHIAppConfigs                                     
                                       where ac.DBString.Equals(dbString) && ac.Condition.Equals("WHISearchBy")
                                       select (new WHIAppSearchByConfig()
                                       {
                                           WHIAppConfigData = ac.ConditionValue
                                       })).ToList();
                    return ObjPartData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " GetWHISearchByData " + ex.StackTrace);
                var ObjPartData = Enumerable.Empty<WHIAppSearchByConfig>();
                return ObjPartData;
            }
        }

        /// <summary>
        /// The GetWHIAppGridColumns.
        /// </summary>        
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("WHIAppGridColumns")]
        [HttpPost]
        public HttpResponseMessage GetWHIAppGridColumns()
        {
            try
            {
                var PartNumberDetails = GetWHIAppGridColumnsData();
                WHIAppGridColumnsConfigDto WHIAppGridColumn = new WHIAppGridColumnsConfigDto
                {
                    gridColumns = PartNumberDetails
                };
                return Request.CreateResponse(WHIAppGridColumn);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetWHIAppGridColumns" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetWHIAppGridColumnsData.
        /// </summary>        
        /// <returns>The <see cref="IEnumerable{WHIAppGridColumnsConfig}"/>.</returns>
        public IEnumerable<WHIAppGridColumnsConfig> GetWHIAppGridColumnsData()
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    string dbString = WebConfigurationManager.AppSettings["dbstring"];
                    Log.Info("GetWHIAppGridColumnsData - Country:" + Country + " dbstring:" + dbString);
                    //var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjPartData = (from ac in db.WHIAppConfigs
                                       where ac.DBString.Equals(dbString) && ac.Condition.Equals("WHIAppGridColumns")
                                       select (new WHIAppGridColumnsConfig()
                                       {
                                           WHIAppGridColumnsConfigData = ac.ConditionValue
                                       })).ToList();
                    return ObjPartData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " GetWHIAppGridColumnsData " + ex.StackTrace);
                var ObjPartData = Enumerable.Empty<WHIAppGridColumnsConfig>();
                return ObjPartData;
            }
        }

    }
}
