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
            SongTitle firstSongTitle  = songTitlesFactory.Builder().Build("First Song Title");
            SongTitle secondSongTitle = songTitlesFactory.Builder().Build("Second Song Title");
            SongTitle thirdSongTitle  = songTitlesFactory.Builder().Build("Third Song Title");
            SongTitle fourthSongTitle = songTitlesFactory.Builder().Build("Fourth Song Title");
            SongTitle fifthSongTitle  = songTitlesFactory.Builder().Build("Fifth Song Title");
            SongTitle sixthSongTitle  = songTitlesFactory.Builder().Build("Sixth Song Title");
            Assert.NotEqual(firstSongTitle.RecordId, secondSongTitle.RecordId);

            List<Task<AsaphOperationResult<SongTitle>>> individualSaveResults = new() {
                songTitlesFactory.Writer().Save(firstSongTitle),
                songTitlesFactory.Writer().Save(secondSongTitle)
            };

            List<Task<AsaphOperationResult<IEnumerable<SongTitle>>>> batchSaveResults = new() {
                songTitlesFactory.Writer().Save(new List<SongTitle> { 
                    thirdSongTitle, 
                    fourthSongTitle
                }),
                songTitlesFactory.Writer().Save(new List<SongTitle> { 
                    fifthSongTitle, 
                    sixthSongTitle
                }),
            };

            List<Task<AsaphOperationResult<SongTitle>>> failedIndividualSaveResults = 
                individualSaveResults.Where(task => task.Result.HasFailureStatus).ToList();
            Assert.Equal(2, individualSaveResults.Count);
            Assert.Empty(failedIndividualSaveResults);

            List<Task<AsaphOperationResult<IEnumerable<SongTitle>>>> failedBatchSaveResults =
                batchSaveResults.Where(task => task.Result.HasFailureStatus).ToList();
            Assert.Equal(2, batchSaveResults.Count);
            Assert.Empty(failedBatchSaveResults);
        }
    }
}
