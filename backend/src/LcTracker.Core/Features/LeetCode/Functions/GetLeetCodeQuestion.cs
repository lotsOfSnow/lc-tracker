using LcTracker.Shared.Functions;

namespace LcTracker.Core.Features.LeetCode.Functions;

public record LeetCodeQuestion(string Id, string TitleSlug, string Title);

public class GetLeetCodeQuestion(ILeetCodeClient client) : IFunction
{
    public async Task<LeetCodeQuestion?> ExecuteAsync(string url, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        
        var slug = ExtractSlug(url);

        var result = await client.GetQuestion.ExecuteAsync(slug, cancellationToken);
        if (result.Data?.Question is null)
        {
            return null;
        }
        var question = result.Data.Question;

        return new(question.QuestionId, question.TitleSlug, question.Title);
    }

    private static string ExtractSlug(string input)
    {
        const char slash = '/';
        var segments = input.Split(slash).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var result = segments.Length == 0 ? input
                : GetSlugFromSegments(segments);

        return result.Trim();

        static string GetSlugFromSegments(string[] segments)
        {
            var problemsSegmentIndex = Array.IndexOf(segments, "problems");
            return problemsSegmentIndex > -1 && problemsSegmentIndex < segments.Length - 1
                ? segments[problemsSegmentIndex + 1] : segments.Last();

        }
    }
}
