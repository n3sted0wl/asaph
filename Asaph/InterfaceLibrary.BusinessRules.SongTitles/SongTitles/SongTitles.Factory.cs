using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.Shared;

namespace Asaph.InterfaceLibrary.BusinessRules.SongTitles {
    public interface SongTitlesFactory {
        SongTitleBuilder Builder();
        SongTitleWriter Writer();
        SongTitleReader Reader();
        SongTitleDeleter Deleter();
    }

    public interface SongTitleBuilder {
        SongTitle Build(string title);
    }

    public interface SongTitleWriter {
        Task<AsaphOperationResult<SongTitle>> Save(SongTitle songTitle);
        Task<AsaphOperationResult<IEnumerable<SongTitle>>> Save(IEnumerable<SongTitle> songTitles);
    }
    
    public interface SongTitleReader {
        Task<AsaphOperationResult<SongTitle>> SongTitle(Guid recordId);
        Task<AsaphOperationResult<IEnumerable<SongTitle>>> SongTitles(IEnumerable<Guid> recordIds);

        Task<AsaphOperationResult<SongTitleDataSet>> SongTitleDataSets(Guid songTitleRecordId);
        Task<AsaphOperationResult<IEnumerable<SongTitleDataSet>>> SongTitleDataSets(IEnumerable<Guid> songTitleRecordIds);
    }

    public interface SongTitleDeleter {
        Task<AsaphOperationResult> Delete(Guid songRecordId);
        Task<AsaphOperationResult> Delete(IEnumerable<Guid> songRecordIds);
    }

    public interface SongTitleDataSet {
        SongTitle SongTitle { get; }
        IEnumerable<SongArrangement> Arrangements { get; }
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
