// Set up event handlers
const reconnectModal = document.getElementById("components-reconnect-modal");
reconnectModal.addEventListener("components-reconnect-state-changed", handleReconnectStateChanged);

const retryButton = document.getElementById("components-reconnect-button");
retryButton.addEventListener("click", retry);

const resumeButton = document.getElementById("components-resume-button");
resumeButton.addEventListener("click", resume);

document.addEventListener("components-reconnect-state-changed", handleReconnectStateChanged);

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

function handleReconnectStateChanged(event)
{
    const reconnectModal = getModal();

    if (!reconnectModal)
    {
        return;
    }

    const state = event.detail.state;
    const currentAttempt = event.detail.attempt || 1;
    const maxRetries = getMaxRetries();

    if (state === "show" || state === "retrying")
    {
        if (!reconnectModal.open)
        {
            const attemptElem = document.getElementById("components-reconnect-attempt-count");

            if (attemptElem)
            {
                attemptElem.textContent = `Attempt ${currentAttempt} of ${maxRetries}`;
            }

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
        // Reconnect will asynchronously return:
        // - true to mean success
        // - false to mean we reached the server, but it rejected the connection (e.g., unknown circuit ID)
        // - exception to mean we didn't reach the server (this can be sync or async)
        const successful = await Blazor.reconnect();

        if (!successful)
        {
            // We have been able to reach the server, but the circuit is no longer available.
            // We'll reload the page so the user can continue using the app as quickly as possible.
            const resumeSuccessful = await Blazor.resumeCircuit();

            if (!resumeSuccessful)
            {
                location.reload();
            }
            else
            {
                reconnectModal.close();
            }
        }
    }
    catch (err)
    {
        // We got an exception, server is currently unavailable
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

// Bind button clicks  to the retry and resume functions
document.addEventListener("click", (e) =>
{
    if (e.target && e.target.id === "components-reconnect-button")
    {
        retry();
    }
    else if (e.target && e.target.id === "components-resume-button")
    {
        resume();
    }
});
