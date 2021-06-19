using Asaph.Implementation.SongTitles.Implementations;
using Asaph.InterfaceLibrary.BusinessRules.SongTitles;

namespace Asaph.Implementation.SongTitles {
    public class SongTitlesBusinessRulesFactory : SongTitlesFactory {
        public SongTitleBuilder Builder() => new DefaultSongTitleBuilder();
    }
}
