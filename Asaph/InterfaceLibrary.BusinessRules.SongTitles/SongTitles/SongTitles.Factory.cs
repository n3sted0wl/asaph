using System;

namespace Asaph.InterfaceLibrary.BusinessRules.SongTitles {
    public interface SongTitlesFactory {
        SongTitleBuilder Builder();
    }

    public interface SongTitleBuilder {
        SongTitle Build(string title);
    }

    public interface SongTitle {
        Guid RecordId { get; }
        string Title { get; set; }
    }

    public interface SongArrangement {
        Guid RecordId { get; }
        Guid SongRecordId { get; }
    }
}
