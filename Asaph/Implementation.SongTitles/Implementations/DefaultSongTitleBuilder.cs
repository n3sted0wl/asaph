using System;

using Asaph.InterfaceLibrary.BusinessRules.SongTitles;

namespace Asaph.Implementation.SongTitles.Implementations {
    internal class DefaultSongTitleBuilder : SongTitleBuilder {
        public SongTitle Build(string title) {
            SongTitleFromBuilder songTitle = new() {
                RecordId = Guid.NewGuid(),
                Title = title,
            };
            return songTitle;
        }

        internal class SongTitleFromBuilder : SongTitle {
            public Guid RecordId { get; set; }
            public string Title { get; set; }
        }
    }
}
