using Itmo.ObjectOrientedProgramming.Lab3.Adressee;
using Itmo.ObjectOrientedProgramming.Lab3.Adressee.FilterMessages;
using Itmo.ObjectOrientedProgramming.Lab3.Adressee.MessagesLogs;
using Itmo.ObjectOrientedProgramming.Lab3.Adressee.Messengers;
using Itmo.ObjectOrientedProgramming.Lab3.Adressee.Users;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Render;
using Itmo.ObjectOrientedProgramming.Lab3.Topics;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MessageSystemTest
{
    [Fact]
    public void Message_ShouldReturnFalse_WhenUserReceivedMessageAndNotRead()
    {
        var list = new List<IAdressee>();
        var userMessages = new List<UserMessage>();
        var user = new AdresseeUser("Dryu", userMessages);
        list.Add(user);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        Assert.False(user.Messages[message.ID].IsRead);
    }

    [Fact]
    public void Message_ShouldReturnTrue_WhenUserReceivedMessageAndChangeStatusFirstTry()
    {
        var list = new List<IAdressee>();
        var userMessages = new List<UserMessage>();
        var user = new AdresseeUser("Dryu", userMessages);
        list.Add(user);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        user.GetMessage(message.ID);

        Assert.True(user.Messages[message.ID].IsRead);
    }

    [Fact]
    public void Message_ShouldReturnFalse_WhenUserReceivedMessageAndStatusIsReaded()
    {
        var list = new List<IAdressee>();
        var userMessages = new List<UserMessage>();
        var user = new AdresseeUser("Dryu", userMessages);
        list.Add(user);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        user.GetMessage(message.ID);

        Assert.False(user.Messages[message.ID].ChangeReadStatus().IsSuccess);
    }

    [Fact]
    public void Message_ShouldFiltred_WhenImportantLessDegreeOfFiltration()
    {
        var list = new List<IAdressee>();

        var mock = new Mock<IAdressee>();

        var adresee = new FilterMessage(4, mock.Object);

        list.Add(adresee);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        mock.Verify(adressee => adressee.AddMessage(It.IsAny<Message>()), Times.Never());
    }

    [Fact]
    public void Log_ShouldRead_WhenMessageIsAdd()
    {
        var list = new List<IAdressee>();

        var mock = new Mock<IAdressee>();

        var adresee = new LogCreator(mock.Object);

        list.Add(adresee);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        mock.Verify(adressee => adressee.AddMessage(It.IsAny<Message>()), Times.Once());
    }

    [Fact]
    public void Messenger_ShouldReadInConsoleMessageText_WhenAllGood()
    {
        var list = new List<IAdressee>();

        var mock = new Mock<IRenderAble>();

        var mess = new Messenger(mock.Object);

        var userMessages = new List<UserMessage>();

        var user = new AdresseeUser("Dryu", userMessages);
        list.Add(user);

        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        mess.Render();

        mock.Verify(ad => ad.Render(It.IsAny<Message>()), Times.Once());
    }

    [Fact]
    public void AdresseeUser_ShouldReceiveOneMessage_WhenMessageSendToUserWithFilterAndWithout()
    {
        var list = new List<IAdressee>();

        var mock = new Mock<IAdressee>();

        var adresee = new FilterMessage(4, mock.Object);

        list.Add(adresee);
        list.Add(mock.Object);
        var topic = new Topic("aboba", list);
        var message = new Message("GG", "WP", 1);

        topic.SendMessage(message);

        mock.Verify(adressee => adressee.AddMessage(It.IsAny<Message>()), Times.Once());
    }
}