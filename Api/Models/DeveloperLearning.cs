public class DeveloperLearning
{
    public int ResponseId { get; set; }
    public string? Age { get; set; }
    public int? YearsCode { get; set; }
    public string? DevType { get; set; }
    public string? LearnCodeChoose { get; set; }
    public List<string>? LearnCode { get; set; }
    public string? LearnCodeAI { get; set; }
    public List<string>? AILearningMethods { get; set; }

    public string? AIUsage { get; set; }
    public string? AITrust { get; set; }
    public string? AISentiment { get; set; }

    public string? ExperienceLevel { get; set; }

    public bool UsesDocumentation { get; set; }
    public bool UsesAIForLearning { get; set; }
    public bool UsesStackOverflow { get; set; }
}