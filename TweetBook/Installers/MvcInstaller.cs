using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Data;
using System.Text.Json.Serialization;

namespace TweetBook
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services,IConfiguration configuration)
        {
            /*var json = JsonSerializer.Serialize(new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Tweetbook API", Version = "v1" });
            });
            */

        }
    }
}
