using Telegram.Bot.Types;
using TestAzureAPI.Models;
using Telegram.Bot.Types.ReplyMarkups;
using static TestAzureAPI.Models.CommandsTag;

namespace TestAzureAPI.Commands {
    public class StartCommand : Command {
        public override string Name => "start";

        public override void Execute(Message msg) {
            var botClient = Bot.AsyncGetBotClient().Result;

            botClient.SendTextMessageAsync(
                msg.Chat.Id,                       // Chat id, в який чат відправляємо
                "Привіт, я твій бот знайомств :)", // Повідомлення
                replyMarkup: new ReplyKeyboardMarkup(
                    new KeyboardButton[] {
                        new KeyboardButton(commandTag["register"])
                    }
                    )
            );

        }
    }
}
