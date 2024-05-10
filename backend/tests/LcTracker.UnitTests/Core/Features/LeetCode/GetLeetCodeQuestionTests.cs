using FluentAssertions;
using LcTracker.Core;
using LcTracker.Core.Features.LeetCode.Queries;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StrawberryShake;

namespace LcTracker.UnitTests.Core.Features.LeetCode;

public class GetLeetCodeQuestionTests
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
    public async Task Returns(string input, string validSlug)
    {
        var client = Substitute.For<ILeetCodeClient>();
        var emptyResult = Substitute.For<IOperationResult<IGetQuestionResult>>();
        emptyResult.Data.ReturnsNull();
        client.GetQuestion.ExecuteAsync(Arg.Is<string>(s => !s.Equals(validSlug, StringComparison.Ordinal))).Returns(emptyResult);

        var sut = new GetLeetCodeQuestionQueryHandler(client);

        var result = await sut.Handle(new(input), default);

        result.IsSuccess.Should().BeTrue();
    }
}
