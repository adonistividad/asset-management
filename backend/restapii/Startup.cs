using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(restapii.Startup))]

namespace restapii
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //-- https://stackoverflow.com/questions/36285253/enable-cors-for-web-api-2-and-owin-token-authentication
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);


            //ConfigureAuth(app);
            //services.AddTransient<ICountryService, CountryService>();
        }
 
    }
}
