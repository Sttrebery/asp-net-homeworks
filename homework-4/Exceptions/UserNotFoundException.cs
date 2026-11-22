namespace homework_4.Exceptions;

public class UserNotFoundException(int Id) : Exception($"No user with id: {Id}");