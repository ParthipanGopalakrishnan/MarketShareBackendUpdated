using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using log4net;

namespace MarketShare.AuthData
{
    public class AuthData
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public MarketShareEntities GetContext()
        {
            try
            {
                var identity = (ClaimsIdentity) System.Threading.Thread.CurrentPrincipal.Identity;
                var claims = identity.Claims;
                string db = string.Empty;
                var Country = claims.First(c => c.Type == "country")?.Value;
                var Currency = claims.First(c => c.Type == "currency")?.Value;
                db = claims.First(c => c.Type == "db").Value;
                WebConfigurationManager.AppSettings["Country"] = HttpContext.Current.Request.Headers["Country"] != null ? HttpContext.Current.Request.Headers.GetValues("Country").FirstOrDefault() : Country;
                WebConfigurationManager.AppSettings["Currency"] = HttpContext.Current.Request.Headers["Currency"] != null ? HttpContext.Current.Request.Headers.GetValues("Currency").FirstOrDefault() : Currency;
                WebConfigurationManager.AppSettings["dbstring"] = Regex.Replace(Regex.Replace(db, "CPM_", ""), "_DEV", "");
                var webConfConnString = ConfigurationManager.ConnectionStrings["MarketShareEntities"].ConnectionString;
                var connectionString = Regex.Replace(webConfConnString, "CPM_.[0-9]+_DEV", db);
                var dbs = new MarketShareEntities(connectionString);
                dbs.Configuration.ProxyCreationEnabled = false;
                dbs.Configuration.LazyLoadingEnabled = true;
                return dbs;
            }
            catch (Exception ex)
            {
                Log.Error("CurrentPrincipal Error " + ex.StackTrace);
                return null;
            }
        }
        public KapowDBEntities GetDBContext()
        {
            try
            {
                var identity = (ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity;
                var claims = identity.Claims;
                string db = string.Empty;
                var Country = claims.First(c => c.Type == "country")?.Value;
                db = claims.First(c => c.Type == "db").Value;
                WebConfigurationManager.AppSettings["Country"] = HttpContext.Current.Request.Headers["Country"] != null ? HttpContext.Current.Request.Headers.GetValues("Country").FirstOrDefault() : Country;
                WebConfigurationManager.AppSettings["dbstring"] = Regex.Replace(Regex.Replace(db, "CPM_", ""), "_DEV", "");
                var webConfConnString = ConfigurationManager.ConnectionStrings["KapowDBEntities"].ConnectionString;
                var dbs = new KapowDBEntities(webConfConnString);
                dbs.Configuration.ProxyCreationEnabled = false;
                dbs.Configuration.LazyLoadingEnabled = true;
                return dbs;
            }
            catch (Exception ex)
            {
                Log.Error("CurrentPrincipal Error " + ex.StackTrace);
                return null;
            }
        }
        public string GetUsername()
        {
            try
            {
                var identity = (ClaimsIdentity) System.Threading.Thread.CurrentPrincipal.Identity;
                var claims = identity.Claims;
                var name = claims.First(c => c.Type == "userName").Value;
                return name;
            }
            catch (Exception ex)
            { 
                Log.Error("userName not set in Claim "+ex.StackTrace);
                return "";
            }
        }
        public string GetUserId()
        {
            try
            {
                var identity = (ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity;
                var claims = identity.Claims;
                var id = claims.First(c => c.Type == "userID").Value;
                return id;
            }
            catch (Exception ex)
            {
                Log.Error("userName not set in Claim " + ex.StackTrace);
                return "";
            }
        }


    }
}