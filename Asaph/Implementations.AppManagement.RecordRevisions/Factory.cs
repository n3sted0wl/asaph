using System;

using Asaph.Implementations.AppManagement.RecordRevisions.Implementations;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;

namespace Implementations.AppManagement.RecordRevisions {
    public class DatabaseRevisionsFactory : AsaphRevisionsFactory {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public DatabaseRevisionsFactory(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public AsaphRevisionBuilder Builder() => new RevisionsBuilder();

        public AsaphRevisionsReader Reader() => new DatabaseRevisionsReader(databaseServicesFactory);

        public AsaphRevisionsWriter Writer() => new DatabaseRevisionsWriter(databaseServicesFactory);
    }

    internal static class DocumentDbCollectionInfo {
        public static string DATABASE_NAME => "History";
        public static string TABLE_NAME => "RecordRevisions";
    }

    public class DocumentDbRevisionsFactory : AsaphRevisionsFactory {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public DocumentDbRevisionsFactory(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public AsaphRevisionBuilder Builder() => new RevisionsBuilder();

        public AsaphRevisionsReader Reader() => new DocumentDbReader(databaseServicesFactory);

        public AsaphRevisionsWriter Writer() => new DocumentDbWriter(databaseServicesFactory);
    }
}
