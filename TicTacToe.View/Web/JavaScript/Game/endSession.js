"use strict"


window.addEventListener("beforeunload", () => {
    const sessionId = sessionStorage.getItem("sessionId");
    if (!sessionId) return;

    console.log(sessionId);

    const url = `${API_BASE_URL}/api/game/end?sessionId=${sessionId}`;
    navigator.sendBeacon(url);
});