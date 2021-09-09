using Newtonsoft.Json;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Models {
    internal static class RevisionDataEncoder {
        public static string Encode<Model>(Model data) =>
            JsonConvert.SerializeObject(value: data);

        public static Model Decode<Model>(string encodedData) =>
            JsonConvert.DeserializeObject<Model>(value: encodedData);
    }
}
