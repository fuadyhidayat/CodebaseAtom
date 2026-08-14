namespace DecoratorConsoleApp;

public interface IService
{
    public Task Handle();
}

public sealed class SomeService() : IService
{
    public async Task Handle()
    {
        Console.WriteLine("This is Some Service");

        await Task.CompletedTask;
    }
}

public sealed class LoggingDecorator(IService innerService) : IService
{
    public async Task Handle()
    {
        Console.WriteLine("Logging Before");

        await innerService.Handle();

        Console.WriteLine("Logging After");

        await Task.CompletedTask;
    }
}
