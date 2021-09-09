using Asaph.InterfaceLibrary.BusinessRules.SongTitles;
using System.Diagnostics.CodeAnalysis;

using Asaph.Tests.Shared;

using Xunit;
using Xunit.Abstractions;

namespace Asaph.Tests.BusinessRules.General.SongTitles {
    [ExcludeFromCodeCoverage]
    public class Builder : GlobalTestsBaseClass {
        public Builder(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Fact]
        public void SongTitlesCanBeBuiltInMemory() {
            SongTitlesFactory titlesFactory = SongTitlesAppInjector().SongTitlesFactory();
            string demoSongTitle =
                Configuration.GetSection("song_title_tests").GetSection("builder").GetSection("title").Value;
            Assert.False(string.IsNullOrWhiteSpace(demoSongTitle), "Test Song Title not configured");
            SongTitle newSongTitle = titlesFactory.Builder().Build(demoSongTitle);
            Assert.False(string.IsNullOrWhiteSpace(newSongTitle.RecordId.ToString()));
            Assert.Equal(newSongTitle.Title, demoSongTitle);
        }
    }
}
