using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http.Filters;
using log4net;
using Microsoft.IdentityModel.Tokens;
using PPCAnalytics.Authentication;

namespace MarketShare.Authentication
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var request = context.Request;
                var authorization = request.Headers.Authorization;

                if (authorization == null || authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Authentication data is not provided.", request);
                    return;
                }

                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Authentication data is incomplete.", request);
                    return;
                }

                var token = authorization.Parameter;
                //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJ3d3cuZXVjb24uY29tIiwiaWF0IjoxNjE1NDY3MjM4LCJleHAiOjE2MTgwNTkyMzgsImF1ZCI6Ind3dy5ldWNvbi5jb20iLCJzdWIiOiJQUENERU1PIiwiZGIiOiJDUE1fQzhfREVWIiwiY291bnRyeSI6IlVTIiwiY3VycmVuY3kiOiJVU0QiLCJ1c2VySUQiOjMzNCwidXNlck5hbWUiOiJCYWxhIFJhanUifQ.icdHR3H58G6uC76jaGoP7T_2NgRRx62rkXSHwoRGRck";
                var principal = await AuthenticateJwtToken(token);

                if (principal == null)
                    context.ErrorResult = new AuthenticationFailureResult("Authentication data is invalid.", request);
                else
                    context.Principal = principal;
            }
            catch (Exception ex)
            {
               Log.Error(ex.Message);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Bearer");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            IPrincipal user = ValidateToken(token);
            if ( user != null)
            {

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        private static ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    Log.Error("JWT is missing.");
                    return null;
                }

                var secret = WebConfigurationManager.AppSettings["Secret"];
                var symmetricKey = Convert.FromBase64String(secret);
                var audience = WebConfigurationManager.AppSettings["Audience"];
                var issuer = WebConfigurationManager.AppSettings["Issuer"];

                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audience,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                if (!(principal?.Identity is ClaimsIdentity identity))
                    return null;
                else
                    return !identity.IsAuthenticated ? null : principal;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
    }
}