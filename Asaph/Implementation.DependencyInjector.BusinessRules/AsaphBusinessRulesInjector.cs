using System;

using Asaph.Implementation.SongTitles;
using Asaph.InterfaceLibrary.BusinessRules;
using Asaph.InterfaceLibrary.BusinessRules.SongTitles;

using Microsoft.Extensions.Configuration;

namespace Asaph.Implementation.DependencyInjector.BusinessRules {
    public class AsaphBusinessRulesInjector : BusinessRulesInjector {
        private readonly IConfiguration configuration;

        public AsaphBusinessRulesInjector(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public SongTitlesFactory SongTitlesFactory() => new SongTitlesBusinessRulesFactory();
    }
}
