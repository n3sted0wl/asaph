using Asaph.InterfaceLibrary.BusinessRules.SongTitles;
using Asaph.InterfaceLibrary.RecordRevisions;

namespace Asaph.InterfaceLibrary.BusinessRules.Injectors {
    public interface AppManagementInjector {
        AsaphRevisionsFactory RevisionsFactory();
    }

    public interface SongTitlesAppInjector {
        SongTitlesFactory SongTitlesFactory();
    }
}
