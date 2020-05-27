using System;
using System.Data.SqlClient;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static TestAzureAPI.Models.CommandsTag;
using TestAzureAPI.Models;
using TestAzureAPI.Services;

namespace TestAzureAPI.Commands {
    public class RegisterCommand : Command {
        public override string Name => "register";

        public override void Execute(Message msg) {
            var botClient = Bot.AsyncGetBotClient().Result;
            string messageText = @"Ти зареєструвався. Використовуй інші команди, щоб змінити інформацію про себе.";

            bool result = DBService.AddUser(msg.From.Id, msg.Chat.Id, out string messageError);
            UsersState.userStates.Add(msg.From.Id, State.Normal);
            if (result) {
                botClient.SendTextMessageAsync(
                        msg.Chat.Id,
                        messageText,
                        replyMarkup: new ReplyKeyboardMarkup(
                     new KeyboardButton[][] {
                    new KeyboardButton[]{
                    new KeyboardButton(commandTag["bio"]),
                    new KeyboardButton(commandTag["details"]),
                    new KeyboardButton(commandTag["age"])
                    },
                    new KeyboardButton[] {
                    new KeyboardButton(commandTag["subtopics"])
                    }
                    },
                    resizeKeyboard: true)
                        );
                return;
            }
            botClient.SendTextMessageAsync(
                       msg.Chat.Id,
                       messageError
                   );
        }
    }
}
