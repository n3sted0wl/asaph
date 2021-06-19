using Asaph.Implementation.SongTitles.Implementations;
using Asaph.InterfaceLibrary.BusinessRules.SongTitles;

namespace Asaph.Implementation.SongTitles {
    public class SongTitlesBusinessRulesFactory : SongTitlesFactory {
        public SongTitleBuilder Builder() => new DefaultSongTitleBuilder();

        public SongTitleDeleter Deleter() {
            throw new System.NotImplementedException();
        }

        public SongTitleReader Reader() {
            throw new System.NotImplementedException();
        }

        public SongTitleWriter Writer() {
            throw new System.NotImplementedException();
        }
    }
}
