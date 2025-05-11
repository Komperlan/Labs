using Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class UserMessage
{
    public bool IsRead { get; private set; } = false;

    public Message InnerMessage { get; }

    public UserMessage(Message message)
    {
        InnerMessage = message;
    }

    public MessageReadConditionResultType ChangeReadStatus()
    {
        if (IsRead)
            return new MessageReadConditionResultType(false, InnerMessage);

        IsRead = true;

        return new MessageReadConditionResultType(true, InnerMessage);
    }
}