using BehaviorConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped(typeof(ILogicBehavior<,>), typeof(LoggingBehavior<,>));
services.AddScoped(typeof(ILogicBehavior<,>), typeof(PerformanceBehavior<,>));
//services.AddScoped<AdditionLogic>();
//services.AddScoped<ILogic<AdditionInput, AdditionOutput>>(serviceProvider =>
//    new LogicPipeline<AdditionInput, AdditionOutput>(
//        serviceProvider.GetRequiredService<AdditionLogic>(),
//        serviceProvider.GetServices<ILogicBehavior<AdditionInput, AdditionOutput>>()
//    ));
services.AddLogicWithPipeline<AdditionLogic, AdditionInput, AdditionOutput>();

using var serviceProvider = services.BuildServiceProvider();

var input = new AdditionInput()
{
    Number1 = 1,
    Number2 = 2
};

var additionLogic = serviceProvider.GetRequiredService<ILogic<AdditionInput, AdditionOutput>>();
var output = await additionLogic.Handle(input);

Console.WriteLine($"Program.cs: Addition result: {output.Result}");
