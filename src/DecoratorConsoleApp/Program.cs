using CobaConsoleApp;

IService someService = new SomeService();
someService = new LoggingDecorator(someService);

await someService.Handle();
