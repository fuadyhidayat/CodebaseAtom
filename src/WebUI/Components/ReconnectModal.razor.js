let currentAttemptCount = 0;

function getModal()
{
    return document.getElementById("components-reconnect-modal");
}

function getMaxRetries()
{
    return window.blazorReconnectionOptions?.maxRetries ?? 5;
}

function updateAttemptText(attempt)
{
    const maxRetries = getMaxRetries();
    const attemptElem = document.getElementById("components-reconnect-attempt-count");

    if (attemptElem)
    {
        // Mencegah teks menampilkan angka melampaui maxRetries
        const displayAttempt = Math.min(attempt, maxRetries);
        attemptElem.textContent = `Attempt ${displayAttempt} of ${maxRetries}`;
    }
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
        currentAttemptCount = 1;
        updateAttemptText(currentAttemptCount);

        if (!reconnectModal.open)
        {
            reconnectModal.showModal();
        }

        requestAnimationFrame(() =>
        {
            updateAttemptText(currentAttemptCount);
        });
    }
    else if (state === "retrying")
    {
        if (reconnectModal.open)
        {
            currentAttemptCount++;
        }
        else
        {
            currentAttemptCount = 1;
            reconnectModal.showModal();
        }

        requestAnimationFrame(() =>
        {
            updateAttemptText(currentAttemptCount);
        });
    }
    else if (state === "hide")
    {
        currentAttemptCount = 0;

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
