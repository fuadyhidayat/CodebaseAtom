Blazor.start({
    circuit:
    {
        reconnectionOptions:
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
        }
    }
});
