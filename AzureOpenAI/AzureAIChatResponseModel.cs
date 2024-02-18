using System.Collections.Generic;

namespace TeamsNation2024AzureOpenAIBot.AzureOpenAI
{
    public class AzureAIChatResponseModel
    {
       
            public string id { get; set; }
            public string model { get; set; }
            public int created { get; set; }
            public string @object { get; set; }
            public List<AzureAIChatResponseChoice> choices { get; set; }
        }

        public class AzureAIChatResponseChoice
        {
            public int index { get; set; }
            public List<AzureAIChatMessage> messages { get; set; }
        }
    }

