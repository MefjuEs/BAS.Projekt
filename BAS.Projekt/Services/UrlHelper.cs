using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BAS.AppServices;

namespace BAS.Projekt.Services
{
    public class UrlHelper : BAS.Services.IUrlHelper
    {
        private readonly IUrlHelper urlHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppConfig appConfig;

        public UrlHelper(IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor, AppConfig appConfig)
        {
            this.urlHelper = urlHelper;
            this.httpContextAccessor = httpContextAccessor;
            this.appConfig = appConfig;
        }

        public string CreateClientUrl()
        {
            //to do generate urls for client app
            return string.Empty;
        }
    }
}
