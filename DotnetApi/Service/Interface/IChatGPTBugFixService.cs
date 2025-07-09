
public interface IChatGPTBugFixService
{
    Task<BugFixResponse> AnalyzeBugAsync(BugFixRequest request);
}
