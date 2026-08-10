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
        },
        reconnectionHandler:
        {
            onConnectionDown: () => dispatchReconnectEvent("show"),
            onConnectionUp: () => dispatchReconnectEvent("hide"),
            onReconnecting: (attemptNumber) =>
            {
                if (attemptNumber === 0)
                {
                    dispatchReconnectEvent("show");
                }
                else
                {
                    dispatchReconnectEvent("retrying");
                    updateSecondsToNextAttempt(5);
                }
            },
            onReconnected: () => dispatchReconnectEvent("hide"),
            onReconnectFailed: () => dispatchReconnectEvent("failed"),
            onCircuitPaused: () => dispatchReconnectEvent("paused")
        }
    }
});

function dispatchReconnectEvent(state)
{
    const modal = document.getElementById("components-reconnect-modal");
    if (modal)
    {
        const event = new CustomEvent("components-reconnect-state-changed", {
            detail: { state }
        });
        modal.dispatchEvent(event);
    }
}

function updateSecondsToNextAttempt(seconds)
{
    const badge = document.getElementById("components-seconds-to-next-attempt");
    if (badge)
    {
        badge.textContent = seconds;

        let remaining = seconds;
        const interval = setInterval(() =>
        {
            remaining--;
            if (remaining > 0)
            {
                badge.textContent = remaining;
            }
            else
            {
                clearInterval(interval);
            }
        }, 1000);
    }
}
