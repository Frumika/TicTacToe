"use strict"

const API_BASE_URL = "http://localhost:5026";


document.addEventListener("DOMContentLoaded", async () => {
    const modePvp = document.getElementById("mode-pvp");

    function getSelectedMode() {
        return modePvp.querySelector(".elem__circle").style.background === "white" ? "PvP" : "PvE";
    }

    async function send() {
        let sessionId = sessionStorage.getItem("sessionId");

        if (!sessionId) {
            sessionId = crypto.randomUUID();
            sessionStorage.setItem("sessionId", sessionId);
        }

        const requestData = {
            sessionId: sessionId,
            gameMode: getSelectedMode()
        }

        const response = await fetch(`${API_BASE_URL}/api/game/settings`, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(requestData)
        });

        if (!response.ok) {
            console.log("Settings not confirmed")
        } else {
            location.reload();
        }
    }

    document.getElementById("confirm-button").addEventListener("click", send);
});