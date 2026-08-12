window.blazorReconnectionOptions =
{
    maxRetries: 2,
    retryIntervalMilliseconds: (previousAttempts, maxRetries) =>
    {
        if (previousAttempts >= maxRetries)
        {
            return null;
        }

        return 2000;
    }
};

Blazor.start({
    circuit:
    {
        reconnectionOptions: window.blazorReconnectionOptions
    }
});
