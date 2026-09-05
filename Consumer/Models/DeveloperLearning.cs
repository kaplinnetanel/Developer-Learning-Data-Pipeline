using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Models;

public class DeveloperLearning
{
    public string ResponseId { get; set; } = string.Empty;
    public string? Age { get; set; }
    public double? YearsCode { get; set; }
    public string? DevType { get; set; }
    public string? LearnCodeChoose { get; set; }

    public List<string>? LearnCode { get; set; }
    public string? LearnCodeAI { get; set; }
    public List<string>? AILearnHow { get; set; }

    public string? AISelect { get; set; }
    public string? AIAcc { get; set; }
    public string? AISent { get; set; }

    public string? ExperienceLevel { get; set; }

    public bool UsesDocumentation { get; set; }
    public bool UsesAIForLearning { get; set; }
    public bool UsesStackOverflow { get; set; }
}
