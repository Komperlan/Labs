namespace Lab5.Application.Models.Users;

public record User(long Id, string Username, UserRole Role, long Score, long Password);