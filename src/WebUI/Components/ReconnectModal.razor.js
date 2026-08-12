function getModal()
{
    return document.getElementById("components-reconnect-modal");
}

function getMaxRetries()
{
    return window.blazorReconnectionOptions?.maxRetries ?? 5;
}

// Prevent the modal from being closed by the user, since we want to control when it is closed
document.addEventListener("DOMContentLoaded", () =>
{
    const modal = getModal();

    if (modal)
    {
        modal.addEventListener("cancel", (e) => e.preventDefault());
    }
});

const reconnectModal = document.getElementById("components-reconnect-modal");
reconnectModal.addEventListener("components-reconnect-state-changed", handleReconnectStateChanged);

function handleReconnectStateChanged(event)
{
    const reconnectModal = getModal();

    if (!reconnectModal)
    {
        return;
    }

    const state = event.detail.state;

    if (state === "show")
    {
        if (!reconnectModal.open)
        {
            reconnectModal.showModal();
        }
    }
    else if (state === "retrying")
    {
        if (!reconnectModal.open)
        {
            reconnectModal.showModal();
        }
    }
    else if (state === "hide")
    {
        if (reconnectModal.open)
        {
            reconnectModal.close();
        }
    }
    else if (state === "failed")
    {
        document.addEventListener("visibilitychange", retryWhenDocumentBecomesVisible);
    }
    else if (state === "rejected")
    {
        location.reload();
    }
}

async function retry()
{
    document.removeEventListener("visibilitychange", retryWhenDocumentBecomesVisible);

    try
    {
        const successful = await Blazor.reconnect();

        if (!successful)
        {
            location.reload();
        }
    }
    catch (err)
    {
        console.error("Reconnection attempt failed:", err);
        document.addEventListener("visibilitychange", retryWhenDocumentBecomesVisible);
    }
}

async function resume()
{
    try
    {
        const successful = await Blazor.resumeCircuit();

        if (!successful)
        {
            location.reload();
        }
    }
    catch
    {
        const reconnectModal = getModal();

        if (reconnectModal)
        {
            reconnectModal.classList.replace("components-reconnect-paused", "components-reconnect-resume-failed");
        }
    }
}

async function retryWhenDocumentBecomesVisible()
{
    if (document.visibilityState === "visible")
    {
        document.removeEventListener("visibilitychange", retryWhenDocumentBecomesVisible);

        await retry();
    }
}

// Bind button clicks to the retry and resume functions
document.addEventListener("click", (e) =>
{
    const button = e.target instanceof Element ? e.target.closest("button") : null;

    if (button?.id === "components-reconnect-button")
    {
        retry();
    }
    else if (button?.id === "components-resume-button")
    {
        resume();
    }
});
