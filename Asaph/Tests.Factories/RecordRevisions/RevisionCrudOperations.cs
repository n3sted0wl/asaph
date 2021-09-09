using Asaph.InterfaceLibrary.RecordRevisions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Asaph.InterfaceLibrary.RecordRevisions.RevisionModels;
using Asaph.InterfaceLibrary.Shared;
using Asaph.Tests.Shared;

using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Factories.RecordRevisions {
    [ExcludeFromCodeCoverage]
    public class RevisionCrudOperations : GlobalTestsBaseClass {
        public RevisionCrudOperations(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Theory]
        [InlineData("TEST_SYSTEM", "TEST_TYPE", "ref_id_1", "id_1", 1, 24.3d)]
        [InlineData("TEST_SYSTEM", "TEST_TYPE", "ref_id_2", "id_2", 2, 224.3d)]
        [InlineData("TEST_SYSTEM", "TEST_TYPE", "ref_id_3", "id_3", 3, 124.3d)]
        public void DemoCrudOperationsForARevisionModel(
            string userId, string type, string referenceId, string modelId, int integerValue, decimal decimalValue) {
            DateTime dateTime = DateTime.Today;
            RevModel_Demo modelToSave = new() {
                ID = modelId,
                DateTime = dateTime,
                IntegerValue = integerValue,
                DecimalValue = decimalValue
            };

            AsaphRevisionsFactory revisionsFactory = AsaphAppsInjectionManager().RevisionsFactory();
            AsaphRevisionBuilder builder = revisionsFactory.Builder();
            AsaphRevisionsWriter writer = revisionsFactory.Writer();
            AsaphRevisionsReader reader = revisionsFactory.Reader();

            Guid tenantId = Guid.NewGuid();
            RecordRevision<RevModel_Demo> revisionRecordToSave =
                builder.Build(
                    tenantId: tenantId,
                    userId: userId,
                    type: type,
                    referenceId: referenceId,
                    dateTime: dateTime,
                    recordData: modelToSave);
            Task<AsaphOperationResult> saveRequest = writer.Save(revisionRecordToSave);
            AsaphOperationResult saveResult = saveRequest.Result;
            Assert.True(saveResult.HasSuccessStatus(), saveResult.Message);
            Task<AsaphOperationResult<IEnumerable<RecordRevision<RevModel_Demo>>>> readRequest =
                reader.Revisions<RevModel_Demo>(
                    tenantId: tenantId,
                    typeId: type,
                    referenceId: referenceId);
            AsaphOperationResult<IEnumerable<RecordRevision<RevModel_Demo>>> readResult = readRequest.Result;
            Assert.True(readResult.HasSuccessStatus(), readResult.Message);
            IEnumerable<RecordRevision<RevModel_Demo>> revisionRecords = readResult.Payload;
            Assert.NotEmpty(revisionRecords);
            Assert.Contains(revisionRecords, record => record.Guid.Equals(revisionRecordToSave.Guid));
            revisionRecords.ToList().ForEach(revision => {
                Assert.True(revision.TenantId.Equals(tenantId));
                Assert.True(revision.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
                Assert.True(revision.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
                Assert.True(revision.ReferenceId.Equals(referenceId, StringComparison.OrdinalIgnoreCase));
                Assert.True(revision.RevisionDateTime.Equals(dateTime));

                Assert.True(revision.RecordData.ID.Equals(modelId));
                Assert.True(revision.RecordData.IntegerValue.Equals(integerValue));
                Assert.True(revision.RecordData.DecimalValue.Equals(decimalValue));
                Assert.True(revision.RecordData.DateTime.Equals(dateTime));
            });
        }
    }
}
