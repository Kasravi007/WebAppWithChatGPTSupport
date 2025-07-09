public class BugFixRequest
{
    public string BugDescription { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public string CodeContext { get; set; }
    public string TechStack { get; set; }
    public string ExpectedBehavior { get; set; }
    public string ActualBehavior { get; set; }
}