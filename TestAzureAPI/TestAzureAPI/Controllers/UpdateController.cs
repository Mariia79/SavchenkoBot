using Microsoft.AspNetCore.Mvc;
using TestAzureAPI.Models;
using static TestAzureAPI.Models.UsersState;
using static TestAzureAPI.Models.CommandsTag;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Linq;

namespace TestAzureAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase {
        static int MessagesCountFromStart = 0;
        static ReplyKeyboardMarkup fullKeyboard = new ReplyKeyboardMarkup(
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
                    resizeKeyboard: true
           );
        static string log = "";
        // GET: api/update
        [HttpGet]
        public async Task<ActionResult<string>> Get() {
            return Ok($"Messages: {MessagesCountFromStart}\n" + $"Log: {log}");
        }

        // POST: api/update
        [HttpPost]
        public async void Post([FromBody] Update update) {
            Message msg = update.Message;
            int userId = msg.From.Id;
            TelegramBotClient botClient = Models.Bot.AsyncGetBotClient().Result;
            MessagesCountFromStart++;
            bool isCommand = false;

            foreach (var command in Bot.Commands) {
                if (command.Contains(msg.Text)) {
                    log += $"Command {command.Name} executing... ";
                    command.Execute(msg);
                    log += $"Command {command.Name} executed\n";
                    isCommand = true;
                    break;
                } else if (commandTag[command.Name] == msg.Text) {
                    command.Execute(msg);
                    isCommand = true;
                    break;
                }
            }
            if (!isCommand && userStates.ContainsKey(userId)) {
                switch (userStates[userId]) {
                    case State.BIOWaiting:
                        if (Services.DBService.AddUserBIO(userId, msg.Text, out string messageErrBio)) {
                            await botClient.SendTextMessageAsync(
                                msg.Chat.Id,
                                "Опис себе змінено!"
                            );
                            userStates[userId] = State.Normal;
                        } else {
                            await botClient.SendTextMessageAsync(
                                msg.Chat.Id,
                                messageErrBio
                            );
                            userStates[userId] = State.Normal;
                        }
                        break;
                    case State.DetailsWaiting:
                        if (Services.DBService.AddUserDetails(userId, msg.Text, out string messageErrDetails)) {
                            await botClient.SendTextMessageAsync(
                                msg.Chat.Id,
                                "Деталі змінено!"
                            );
                            userStates[userId] = State.Normal;
                        } else {
                            await botClient.SendTextMessageAsync(
                                msg.Chat.Id,
                                messageErrDetails
                            );
                            userStates[userId] = State.Normal;
                        }
                        break;
                    case State.AgeWaiting:
                        if (Services.DBService.AddUserAge(userId, int.Parse(msg.Text), out string messageErrAge)) {
                            await botClient.SendTextMessageAsync(
                               msg.Chat.Id,
                               "Вік змінено!"
                           );
                        } else
                            await botClient.SendTextMessageAsync(
                                msg.Chat.Id,
                                messageErrAge
                            );
                        userStates[userId] = State.Normal;
                        break;
                    case State.SubtopicsFilling:
                        break;
                    default:
                        break;
                }
            }

        }

        // PUT: api/update
        [HttpPut("/topics")]
        public async void PutTopics([FromBody] string value) {

        }

        // PUT: api/update
        [HttpPut("/topics/{id}/subtopics")]
        public async void PutSubtopics(int id, [FromBody] string value) {

        }

        // DELETE: api/update
        [HttpDelete("subtopic/{id}")]
        public async void Delete(int id) {
        }
    }
}
