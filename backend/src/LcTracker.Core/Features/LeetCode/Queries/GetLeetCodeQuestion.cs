using LcTracker.Shared.Handlers;
using LcTracker.Shared.Results;

namespace LcTracker.Core.Features.LeetCode.Queries;

public record LeetCodeQuestion(string Id, string TitleSlug, string Title);

public record GetLeetCodeQuestionQuery(string Url) : IQuery<LeetCodeQuestion>;

public class GetLeetCodeQuestionQueryHandler(ILeetCodeClient client) : IQueryHandler<GetLeetCodeQuestionQuery, LeetCodeQuestion>
{
    public async Task<Result<LeetCodeQuestion>> Handle(GetLeetCodeQuestionQuery request, CancellationToken cancellationToken)
    {
        var slug = ExtractSlug(request.Url);

        var result = await client.GetQuestion.ExecuteAsync(slug, cancellationToken);
        if (result.Data?.Question is null)
        {
            return Errors.NotFound;
        }
        var question = result.Data.Question;

        return new LeetCodeQuestion(question.QuestionId, question.TitleSlug, question.Title);
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
