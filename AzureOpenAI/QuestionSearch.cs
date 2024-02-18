using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace TeamsNation2024AzureOpenAIBot.AzureOpenAI
{
    public static class QuestionSearch
    {
        public static async Task<Answer> AnswerQuestion(string question)
        {
            try
            {

                var requestMessage = new AzureAIChatRequestModel()
                {
                    deployment = "[Azure Open AI Deployment Name]",
                    temperature = 1,
                    top_p = 1,
                    max_tokens = 800,
                    stop = null,
                    stream = false,
                    dataSources = new List<AzureAIChatRequestDataSource>()
                {
                    new AzureAIChatRequestDataSource()
                    {
                        type = "AzureCognitiveSearch",
                        parameters = new AzureAIChatRequestParameters()
                        {
                            endpoint = "[Azure Search Endpoint]",
                            key = "[Azure Search Key]",
                            indexName = "[Azure Search Index Name]",
                            semanticConfiguration = "",
                            topNDocuments = "5",
                            queryType = "simple",                            
                            fieldsMapping = new AzureAIChatRequestFieldsMapping()
                            {
                                contentFieldsSeparator = "\\n",
                                contentFields = new List<string>(){"List","Of","Content","Fields","From","Azure","Search","Service" },
                                titleField = "title", //change these if needed to match the values in Azure Search Service
                                filepathField = "id", //change these if needed to match the values in Azure Search Service
                                urlField = "url"      //change these if needed to match the values in Azure Search Service
                            },
                            inScope = false,
                            roleInformation = "[Add Prompt]"
                        }
                    }
                },
                    messages = new List<AzureAIChatMessage>()
                    { new AzureAIChatMessage()
                    {
                        role = "user",
                        content = question
                    }}

                };

                //TODO: replace [INSTANCE_NAME] and [DEPLOYMENT_NAME] in the URL below
                var httpRequest = (HttpWebRequest)WebRequest.Create("https://[INSTANCE_NAME].openai.azure.com/openai/deployments/[DEPLOYMENT_NAME]/extensions/chat/completions?api-version=2023-06-01-preview");
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers.Add("api-key", "[Azure OpenAI API Key]");
                httpRequest.Accept = "application/json";

                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestMessage));
                httpRequest.ContentLength = postBytes.Length;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                var httpResponse = await httpRequest.GetResponseAsync();

                if (((HttpWebResponse)httpResponse).StatusCode == HttpStatusCode.OK)
                {
                    var responseString = String.Empty;
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseString = streamReader.ReadToEnd();
                    }

                    //this is the actual answer text.
                    AzureAIChatResponseModel response = JsonConvert.DeserializeObject<AzureAIChatResponseModel>(responseString.ToString());
                    var responseMessages = response.choices[0].messages;

                    //this is the citations for that answer
                    var otherContent = responseMessages.First(p => p.role == "tool").content;
                    AzureAIChatResponseCitations citations = JsonConvert.DeserializeObject<AzureAIChatResponseCitations>(otherContent.ToString());


                    var answer = new Answer()
                    {
                        AnswerText = responseMessages.First(p => p.role == "assistant").content,
                        Citations = citations                       
                    };
                                       
                    return answer;
                }
                return new Answer() { AnswerText = "Sorry, I don't know." };
            }


            catch (Exception ex)
            {
                return new Answer() { AnswerText = $"Sorry, I went wrong: {ex.Message}" };
            }
        }
    }
}
