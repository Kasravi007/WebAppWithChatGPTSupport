
using OpenAI.Chat;
using System.Text;
public class ChatGPTBugFixService : IChatGPTBugFixService
{
    private readonly ChatClient _chatClient;
    private readonly ILogger<ChatGPTBugFixService> _logger;

    public ChatGPTBugFixService(IConfiguration configuration, ILogger<ChatGPTBugFixService> logger)
    {
        var apiKey = configuration["OpenAI:ApiKey"];
        var _model = configuration["OpenAI:Model"] ?? "gpt-3.5-turbo"; // Default fallback
        _chatClient = new ChatClient(_model, apiKey);
        _logger = logger;
    }

    public async Task<BugFixResponse> AnalyzeBugAsync(BugFixRequest request)
    {
        try
        {
            var prompt = BuildBugAnalysisPrompt(request);

            var messages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage("You are an expert .NET developer who specializes in debugging and fixing code issues. Provide detailed analysis and solutions."),
                ChatMessage.CreateUserMessage(prompt)
            };

            var chatCompletion = await _chatClient.CompleteChatAsync(messages);

            var response = chatCompletion.Value.Content[0].Text;

            return new BugFixResponse
            {
                Analysis = response,
                Timestamp = DateTime.UtcNow,
                Status = "Success"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error analyzing bug with ChatGPT");
            return new BugFixResponse
            {
                Analysis = "Error occurred while analyzing the bug",
                Status = "Error",
                ErrorMessage = ex.Message
            };
        }
    }

    private string BuildBugAnalysisPrompt(BugFixRequest request)
    {
        var prompt = new StringBuilder();
        prompt.AppendLine("Please analyze the following bug and provide a solution:");
        prompt.AppendLine($"Bug Description: {request.BugDescription}");
        prompt.AppendLine($"Error Message: {request.ErrorMessage}");
        prompt.AppendLine($"Stack Trace: {request.StackTrace}");
        prompt.AppendLine($"Code Context: {request.CodeContext}");
        prompt.AppendLine($"Technology Stack: {request.TechStack}");
        prompt.AppendLine();
        prompt.AppendLine("Please provide:");
        prompt.AppendLine("1. Root cause analysis");
        prompt.AppendLine("2. Step-by-step solution");
        prompt.AppendLine("3. Code fix if applicable");
        prompt.AppendLine("4. Prevention strategies");

        return prompt.ToString();
    }
}