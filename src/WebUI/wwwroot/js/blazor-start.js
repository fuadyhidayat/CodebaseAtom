window.blazorReconnectionOptions =
{
    maxRetries: 5,
    retryIntervalMilliseconds: (previousAttempts, maxRetries) =>
    {
        if (previousAttempts >= maxRetries)
        {
            return null;
        }

        return 3000;
    }
};

Blazor.start({
    circuit:
    {
        reconnectionOptions: window.blazorReconnectionOptions
    }
});
