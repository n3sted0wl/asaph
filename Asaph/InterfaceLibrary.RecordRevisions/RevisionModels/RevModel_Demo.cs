using System;

namespace Asaph.InterfaceLibrary.RecordRevisions.RevisionModels {
    public class RevModel_Demo {
        public string ID { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public int IntegerValue { get; set; } = 0;
        public decimal DecimalValue { get; set; } =0;
    }
}
