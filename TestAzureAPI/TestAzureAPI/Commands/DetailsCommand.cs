using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAzureAPI.Models;
using Telegram.Bot.Types;

namespace TestAzureAPI.Commands {
    public class DetailsCommand : Command {
        public override string Name => throw new NotImplementedException();

        public override void Execute(Message msg) {
            UsersState.userStates[msg.From.Id] = State.DetailsWaiting;
            var botClient = Bot.AsyncGetBotClient().Result;
            botClient.SendTextMessageAsync(
                msg.Chat.Id,
                "Вкажи деталі про себе, те що знадобиться тому, хто відкрив твій профіль"
            );

        }
    }
}
