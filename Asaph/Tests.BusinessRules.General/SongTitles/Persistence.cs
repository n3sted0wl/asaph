using Asaph.InterfaceLibrary.BusinessRules.SongTitles;
using System.Diagnostics.CodeAnalysis;

using Asaph.Tests.Shared;
using InterfaceLibrary.Shared;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Asaph.Tests.BusinessRules.General.SongTitles {
    [ExcludeFromCodeCoverage]
    public class Persistence : GlobalTestsBaseClass {
        public Persistence(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Fact]
        public void SongTitlesCanBeWrittenAndRead() {
            SongTitlesFactory songTitlesFactory = AsaphInjectionManager().BusinessRulesInjector().SongTitlesFactory();
            SongTitle firstSongTitle = songTitlesFactory.Builder().Build("First Song Title");
            SongTitle secondSongTitle = songTitlesFactory.Builder().Build("Second Song Title");
            Assert.NotEqual(firstSongTitle.RecordId, secondSongTitle.RecordId);

            List<Task<AsaphOperationResult<SongTitle>>> saveResults = new() {
                songTitlesFactory.Writer().Save(firstSongTitle),
                songTitlesFactory.Writer().Save(secondSongTitle)
            };

            List<Task<AsaphOperationResult<SongTitle>>> failedSaveResults = 
                saveResults.Where(task => task.Result.HasFailureStatus).ToList();
            Assert.Empty(failedSaveResults);
        }
    }
}
