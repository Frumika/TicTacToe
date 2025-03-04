"use strict"


document.addEventListener("DOMContentLoaded", () => {
    const resetButton = document.querySelector(".main-container__button");

    resetButton.addEventListener("click", async () => {
        const sessionId = sessionStorage.getItem("sessionId");

        if (!sessionId) {
            console.error("Session ID not found.");
            return;
        }

        const API_BASE_URL = "http://localhost:5026";
        try {
            const response = await fetch(`${API_BASE_URL}/api/game/reset`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(sessionId)
            });

            if (!response.ok) {
                console.error("Failed to reset session:", await response.text());
                return;
            }

            console.log("Session reset successfully.");
            location.reload();  // Перезагружаем страницу

        } catch (error) {
            console.error("Error resetting session:", error);
        }
    });
});
