using System.Collections.Generic;

namespace TeamsNation2024AzureOpenAIBot.AzureOpenAI
{
    public class AzureAIChatRequestModel
    {
        public List<AzureAIChatRequestDataSource> dataSources { get; set; }
        public List<AzureAIChatMessage> messages { get; set; }
        public string deployment { get; set; }
        public int temperature { get; set; }
        public int top_p { get; set; }
        public int max_tokens { get; set; }
        public object stop { get; set; }
        public bool stream { get; set; }
    }

    public class AzureAIChatRequestDataSource
    {
        public string type { get; set; }
        public AzureAIChatRequestParameters parameters { get; set; }
    }

    public class AzureAIChatMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class AzureAIChatRequestParameters
    {
        public string endpoint { get; set; }
        public string key { get; set; }
        public string indexName { get; set; }
        public string semanticConfiguration { get; set; }
        public string queryType { get; set; }
        public AzureAIChatRequestFieldsMapping fieldsMapping { get; set; }
        public bool inScope { get; set; }
        public string roleInformation { get; set; }
        public string topNDocuments { get; set; }
    }

    public class AzureAIChatRequestFieldsMapping
    {
        public string contentFieldsSeparator { get; set; }
        public List<string> contentFields { get; set; }
        public string filepathField { get; set; }
        public string titleField { get; set; }
        public string urlField { get; set; }
    }
}
