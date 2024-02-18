// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.22.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TeamsNation2024AzureOpenAIBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replytext = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replytext, replytext), cancellationToken);
        }

        //protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    var answer = await QuestionSearch.AnswerQuestion(turnContext.Activity.Text);
        //    await turnContext.SendActivityAsync(MessageFactory.Text(answer.AnswerText, answer.AnswerText), cancellationToken);
        //}

        //protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    var answer = await QuestionSearch.AnswerQuestion(turnContext.Activity.Text);
        //    var reply = new StringBuilder(answer.AnswerText);

        //    if (answer.Citations?.citations?.Count > 0)
        //    {

        //        reply.AppendLine("\n\n&nbsp;\n\n 🔗 Session Links \n\n&nbsp;\n\n");
        //        foreach (var citation in answer.Citations.citations)
        //        {
        //            reply.AppendLine($"{citation.title} - {citation.url} \r\n");                  
        //        }
        //    }

        //    await turnContext.SendActivityAsync(MessageFactory.Text(reply.ToString(), reply.ToString()), cancellationToken);
        //}

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
