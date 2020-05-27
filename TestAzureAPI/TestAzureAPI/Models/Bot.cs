using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TestAzureAPI.Commands;

namespace TestAzureAPI.Models {
    public static class Bot {
        private static TelegramBotClient botClient;
        public static List<Command> Commands;

        public static async Task<TelegramBotClient> AsyncGetBotClient() {
            if (botClient != null)
                return botClient;
            Commands = new List<Command>();
            //Commands ...
            Commands.Add(new StartCommand());
            Commands.Add(new RegisterCommand());
            CommandsTag.commandTag.Add("register", "Зареєструватись");
            Commands.Add(new BIOCommand());
            CommandsTag.commandTag.Add("bio", "Опис мене");
            Commands.Add(new DetailsCommand());
            CommandsTag.commandTag.Add("details", "Деталі про мене");
            CommandsTag.commandTag.Add("age", "Мій вік");
            CommandsTag.commandTag.Add("subtopics", "Теми");
            botClient = new TelegramBotClient(ApiSettings.BotToken);
            await botClient.SetWebhookAsync(ApiSettings.URL + "api/update");
            return botClient;
        }
    }
}
