using System;
using System.Collections.Generic;

using Asaph.InterfaceLibrary.Shared;

namespace Asaph.InterfaceLibrary.RecordRevisions {
    /// <summary>High-level record revisions factory</summary>
    public interface AsaphRevisionsFactory {
        AsaphRevisionBuilder Builder();
        AsaphRevisionsWriter Writer();
        AsaphRevisionsReader Reader();
    }

    public interface AsaphRevisionBuilder {
        /// <summary>Build a record revision that can be saved</summary>
        /// <typeparam name="Model">Requires a parameterless constructor for decoding</typeparam>
        RecordRevision<Model> Build<Model>(
            Guid tenantId, string userId, string type, DateTime dateTime, Model model) where Model : class;
    }

    public interface AsaphRevisionsWriter {
        AsaphOperationResult Save<Model>(RecordRevision<Model> revision) where Model : class;
        AsaphOperationResult Save<Model>(IEnumerable<RecordRevision<Model>> revisions) where Model : class;
    }

    public interface AsaphRevisionsReader {
        AsaphOperationResult<IEnumerable<RecordRevision<Model>>> Revisions<Model>(
            Guid tenantId, string typeId, string referenceId) where Model : class;
        AsaphOperationResult<IEnumerable<RecordRevision<Model>>> Revisions<Model>(
            Guid tenantId, string typeId, IEnumerable<string> referenceIds) where Model : class;
    }

    public interface RecordRevision<Model> where Model : class {
        Guid Guid { get; }
        Guid TenantId { get; }

        string Type { get; }
        string ReferenceId { get; }

        string UserId { get; }
        DateTime RevisionDateTime { get; }

        Model RecordData { get; }
    }
}
