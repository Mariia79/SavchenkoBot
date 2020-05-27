using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAzureAPI.Models {
    public static class ApiSettings {
        public static string BotName { get; }
            = "AcquaintanceBot";

        public static string BotToken { get; }
            = "718036925:AAFexZxgc2QIYlwxu99L8raKn7CI7FdFFL0";

        public static string URL { get; }
            = @"https://acquintancebot-9314.azurewebsites.net/";
        public static string BadWordsAPIURL { get; }
            = @"https://neutrinoapi.net/bad-word-filter";
        public static string DBServerName { get; }
            = @"acquintancebotapi.database.windows.net";
        public static string Catalog { get; }
            = @"acquintance_bot_db";
        public static string DBUsername { get; }
            = @"maria";
        public static string DBPassword { get; }
            = @"Q1w2e3r4t5";
    }
}
