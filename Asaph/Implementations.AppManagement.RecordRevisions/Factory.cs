using System;

using Asaph.Implementations.AppManagement.RecordRevisions.Implementations;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;

namespace Implementations.AppManagement.RecordRevisions {
    public class RevisionsFactory : AsaphRevisionsFactory {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public RevisionsFactory(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public AsaphRevisionBuilder Builder() => new RevisionsBuilder();

        public AsaphRevisionsReader Reader() => new RevisionsReader(databaseServicesFactory);

        public AsaphRevisionsWriter Writer() => new RevisionsWriter(databaseServicesFactory);
    }
}
