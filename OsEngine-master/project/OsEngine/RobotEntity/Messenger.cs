using OsEngine.RobotEnums;

namespace OsEngine.RobotEntity
{
    public class Messenger
    {
        private static readonly Messenger __instance = new Messenger();

        public static Messenger Instance
        {
            get => __instance;
        }

        public void SendMessage(MessageType type, object? message = null)
        {
            Message?.Invoke(type, message);
        }

        public delegate void MessageDelegate(MessageType type, object? message);
        public event MessageDelegate? Message;
    }
}