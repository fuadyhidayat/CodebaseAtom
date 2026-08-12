window.blazorReconnectionOptions =
{
    maxRetries: 3,
    retryIntervalMilliseconds: (previousAttempts, maxRetries) =>
    {
        if (previousAttempts >= maxRetries)
        {
            return null;
        }

        return 5000;
    }
};

Blazor.start({
    circuit:
    {
        reconnectionOptions: window.blazorReconnectionOptions
    }
});
