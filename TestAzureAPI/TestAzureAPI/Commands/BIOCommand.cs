using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAzureAPI.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestAzureAPI.Commands {
    public class BIOCommand : Command {
        public override string Name => "bio";

        public override void Execute(Message msg) {
            UsersState.userStates[msg.From.Id] = State.BIOWaiting;
            var botClient = Bot.AsyncGetBotClient().Result;
            botClient.SendTextMessageAsync(
                msg.Chat.Id,
                "Вкажи коротку інформацію про себе: ім'я, мету реєстрації і т.і.\nЦю інформацію побачать першою :)"
            );

        }
    }
}
