using System.Collections.Generic;

namespace TeamsNation2024AzureOpenAIBot.AzureOpenAI
{
    public class AzureAIChatResponseCitations
    {
        public List<Citation> citations { get; set; }
        public string intent { get; set; }

        public class Citation
        {
            public string content { get; set; }
            public object id { get; set; }
            public string title { get; set; }
            public string filepath { get; set; }
            public string url { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Metadata
        {
            public string chunking { get; set; }
        }
    }
}
