using BehaviorConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped(typeof(ILogicBehavior<,>), typeof(LoggingBehavior<,>));
services.AddScoped(typeof(ILogicBehavior<,>), typeof(PerformanceBehavior<,>));
services.AddScoped<AdditionLogic>();
services.AddLogicWithPipeline<AdditionLogic, AdditionInput, AdditionOutput>();

using var serviceProvider = services.BuildServiceProvider();
var additionLogic = serviceProvider.GetRequiredService<ILogic<AdditionInput, AdditionOutput>>();

var input = new AdditionInput()
{
    Number1 = 1,
    Number2 = 2
};

var output = await additionLogic.Handle(input);

Console.WriteLine($"Program.cs: Addition result: {output.Result}");
