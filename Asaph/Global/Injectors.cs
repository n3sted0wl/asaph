﻿using Asaph.Implementation.DependencyInjector.BusinessRules;
using Asaph.InterfaceLibrary.BusinessRules;

using Microsoft.Extensions.Configuration;

namespace Asaph.Global {
    public class AsaphInjectionManager {
        protected readonly IConfiguration Configuration;

        /// <summary>
        /// Initialize ONCE per consuming application
        /// </summary>
        /// <param name="configuration">Configuration overrides from consuming application</param>
        public AsaphInjectionManager(IConfiguration configuration) {
            IConfigurationRoot mergedConfiguration =
                new ConfigurationBuilder()
                .AddJsonFile($"appsettings.global.json", false, true)
                .AddConfiguration(configuration)
                .Build();
            this.Configuration = mergedConfiguration;
        }

        public BusinessRulesInjector BusinessRulesInjector() => new AsaphBusinessRulesInjector(configuration: Configuration);
    }
}
