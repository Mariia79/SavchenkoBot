using Telegram.Bot.Types;

namespace TestAzureAPI.Commands {
    public abstract class Command {
        public abstract string Name { get; }

        public abstract void Execute(Message msg);

        public bool Contains(string msgText) {
            return msgText.Contains(Name);
        }
    }
}
