using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.ResultTypes;

public class MessageReadConditionResultType
{
    public bool IsSuccess { get; }

    public Message ReadedMessage { get; }

    public MessageReadConditionResultType(bool isSuccess, Message readedMessage)
    {
        IsSuccess = isSuccess;
        ReadedMessage = readedMessage;
    }
}
