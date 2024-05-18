using LcTracker.Core;
using LcTracker.Core.Features.LeetCode.Functions;
using LcTracker.UnitTests.Shared;
using StrawberryShake;

namespace LcTracker.UnitTests.Core.Features.LeetCode;

public class GetLeetCodeQuestionTests : BaseUnitTest<GetLeetCodeQuestion>
{
    [Theory]
    [InlineData("https://example.com/problems/sample-title/", "sample-title")]
    [InlineData("problems/sample-title/", "sample-title")]
    [InlineData("problems/sample-title", "sample-title")]
    [InlineData("problems/sample-title/details", "sample-title")]
    [InlineData("http:///sample-title/", "sample-title")]
    [InlineData("example.com/problems/sample-title/", "sample-title")]
    [InlineData("https://example.com/problems/sample-title/details/", "sample-title")]
    [InlineData("   https://example.com/problems/ sample-title/ ", "sample-title")]
    [InlineData("sample-title", "sample-title")]
    [InlineData("title", "title")]
    [InlineData("problems/", "problems")]
    public async Task Parses_slug(string input, string validSlug)
    {
        var client = Fixture.Freeze<ILeetCodeClient>();
        var emptyResult = Substitute.For<IOperationResult<IGetQuestionResult>>();
        emptyResult.Data.ReturnsNull();
        client.GetQuestion.ExecuteAsync(Arg.Is<string>(s => !s.Equals(validSlug, StringComparison.Ordinal))).Returns(emptyResult);
        var sut = CreateSut();

        var result = await sut.ExecuteAsync(input, default);

        result.Should().NotBeNull();
    }
}
