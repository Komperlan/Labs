namespace Itmo.CSharpMicroservices.Lab1.Task2;

public sealed record RequestModel(string Method, byte[] Data);