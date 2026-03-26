let registration = null;

async function registerSW() {
    if (!('serviceWorker' in navigator)) return;

    registration = await navigator.serviceWorker.register('/service-worker.js');
    console.log("✅ Service Worker Registered");

    // Check immediately
    await detectUpdate();

    // Auto check every 15 seconds
    setInterval(detectUpdate, 15000);
}


async function detectUpdate() {
    if (!registration) return;

    try {
        await registration.update();   // 🔄 Ask browser to check server for new SW

        if (registration.waiting) {   // ✅ New SW downloaded but waiting
            console.log("🚀 New version detected");
            window.dispatchEvent(new Event("pwa-update-available"));
        }
    }
    catch (err) {
        console.warn("SW update failed:", err);
    }
}


registerSW();

window.registerForUpdateAvailableNotification = (caller, methodName) => {
    window.addEventListener("pwa-update-available", () => {
        caller.invokeMethodAsync(methodName);
    });
};

window.forceAppUpdate = async () => {
    if (!registration?.waiting) return;

    console.log("⚡ Activating new version...");
    registration.waiting.postMessage({ type: 'SKIP_WAITING' });

    setTimeout(() => {
        window.location.reload(true);
    }, 500);
};


