using Microsoft.AspNetCore.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace MALON_GLOBAL_WEBAPP.Helper
{
    public class AuthenticationFilter : System.Web.Http.AuthorizeAttribute, IAuthenticationFilter
    {
        public AuthenticationFilter()
        {

        }

        public bool AllowMultiple 
        { 
            get { return false; } 
        }
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            if(authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization Header", request);
                return;
            }

            if(authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authorization Scheme", request);
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                return;
            }
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }

        public class AuthenticationFailureResult : IHttpActionResult
        {
            public string _ReasonPhrase;
            public HttpRequestMessage _Request { get; set; }
            public AuthenticationFailureResult(string ReasonPhrase, HttpRequestMessage request)
            {
                _ReasonPhrase = ReasonPhrase;
                _Request = request;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }

            private HttpResponseMessage Execute()
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                responseMessage.RequestMessage = _Request;
                responseMessage.ReasonPhrase = _ReasonPhrase;
                return responseMessage;
            }
           
        }

    }
}
