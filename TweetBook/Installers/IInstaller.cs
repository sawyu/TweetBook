﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace TweetBook.Installers
{
    public interface IInstaller
    {
       void InstallerServices(IServiceCollection services,IConfiguration configuration);
    }
}
