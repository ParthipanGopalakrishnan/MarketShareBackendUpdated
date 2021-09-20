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
    /// Defines the <see cref="WHIPriceDataController" />.
    /// </summary>
    [RoutePrefix("api/WHIPriceData")]
    public class WHIPriceDataController : ApiController
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
        /// The GetWHIPartDetailsWithAggPrice.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PartDetailsWithAggPrice")]
        [HttpPost]
        public HttpResponseMessage GetWHIPartDetailsWithAggPrice(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                WHIParameter parameters = JsonConvert.DeserializeObject<WHIParameter>(jsonString);
                var PartNumberDetails = GetWHIPartDetailWithAggPrice(parameters);               
                
                return Request.CreateResponse(PartNumberDetails);
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + "GetWHIPartDetailsWithAggPrice" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetWHIPartDetailWithAggPrice.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{WHIParameter}"/>.</param>
        /// <returns>The <see cref="IEnumerable{WHIPartDetailsWithAggPrice}"/>.</returns>
        public IEnumerable<WHIPartDetailsWithAggPrice> GetWHIPartDetailWithAggPrice(WHIParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    Log.Info("GetWHIPartDetailWithAggPrice - Country:" + Country);
                    //var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjPartData = (from rd in db.ReferenceDatas
                                       join dpa in db.DistributorPriceAggregations on rd.PartID equals dpa.RefId
                                       join co in db.Countries on dpa.CountryId equals co.Id                                       
                                       where parameters.RefID.Contains(rd.PartID) && co.CountryCode.Equals(Country)
                                       select (new WHIPartDetailsWithAggPrice()
                                       {
                                           PartId = rd.PartID,
                                           PartDescription = rd.PartDescription,
                                           PartNumber = rd.Partnumber,
                                           Brand = rd.Brand,
                                           Wholesaler = rd.Wholesaler,
                                           TransactionCount = dpa.TransactionCount,
                                           mn = dpa.MinimumPrice,
                                           mx = dpa.MaximumPrice,
                                           avg = dpa.AveragePrice,
                                           me = dpa.MedianPrice
                                       })).ToList();
                    return ObjPartData;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " GetWHIPartDetailWithAggPrice " + ex.StackTrace);
                var ObjPartData = Enumerable.Empty<WHIPartDetailsWithAggPrice>();
                return ObjPartData;
            }
        }

        /// <summary>
        /// The GetWHIPriceDataByRefID.
        /// </summary>
        /// <param name="Model">The Model<see cref="object"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        [Route("PriceDataByRefID")]
        [HttpPost]
        public HttpResponseMessage GetWHIPriceDataByRefID(object Model)
        {
            try
            {
                var jsonString = Model?.ToString();
                WHIParameter parameters = JsonConvert.DeserializeObject<WHIParameter>(jsonString);
                var PartNumberDetails = GetWHIPricesByRefID(parameters);
                WHIMULPriceDataDto PartGlobalData = new WHIMULPriceDataDto
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
                Log.Error(_authData.GetUsername() + "GetWHIPriceDataByRefID" + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// The GetPartNumberData.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="List{WHIParameter}"/>.</param>
        /// <returns>The <see cref="IEnumerable{WHIMULPriceData}"/>.</returns>
        public IEnumerable<WHIMULPriceData> GetWHIPricesByRefID(WHIParameter parameters)
        {
            try
            {
                using (var db = _authData.GetContext())
                {
                    string Country = WebConfigurationManager.AppSettings["Country"];
                    Log.Info("GetWHIPricesByRefID - Country:" + Country);
                    //var context = db.ModelViewPartNumbers.Where(c => c.CountryStr == Country).ToList();
                    var ObjPartData = (from dpa in db.DistributorPriceAggregations
                                       join dp1 in db.DistributorPrices on dpa.RefId equals dp1.RefID 
                                       join co in db.Countries on dp1.CountryId equals co.Id
                                       where dpa.CountryId == dp1.CountryId
                                       where parameters.RefID.Contains(dpa.RefId) && co.CountryCode.Equals(Country)                                       
                                       select (new WHIMULPriceData()
                                       {                                           
                                           PriceValue = dp1.PriceValue,
                                           PriceDate = dp1.PriceDate,
                                           CountryId = dp1.CountryId,
                                           CurrencyId = dp1.CurrencyId
                                       })).ToList();
                    parameters.Totalcount = ObjPartData.Count;
                    var ObjPartGriddata = parameters.offset != 0 ? ObjPartData.Skip(parameters.offset).Take(parameters.count) : ObjPartData;
                    return ObjPartGriddata;
                }
            }
            catch (Exception ex)
            {
                Log.Error(_authData.GetUsername() + " GetWHIPricesByRefID " + ex.StackTrace);
                var ObjPartGriddata = Enumerable.Empty<WHIMULPriceData>();
                return ObjPartGriddata;
            }
        }

    }
}
