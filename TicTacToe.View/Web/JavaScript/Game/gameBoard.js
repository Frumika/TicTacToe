"use strict"


document.addEventListener("DOMContentLoaded", async () => {
    const API_BASE_URL = "http://localhost:5026";


    const sessionId = sessionStorage.getItem("sessionId") || crypto.randomUUID();
    sessionStorage.setItem("sessionId", sessionId);

    const fields = document.querySelectorAll(".board__field");

    await startSession();
    await loadBoardState();

    fields.forEach((field) => {
        field.addEventListener("click", async () => {
            const row = parseInt(field.dataset.row);
            const column = parseInt(field.dataset.column);

            console.log(`Response: ${row} : ${column}`)

            const response = await fetch(`${API_BASE_URL}/api/game/move`, {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({sessionId, row, column})
            });

            const data = await response.json();
            console.log(data);

            // Обновляем доску
            updateBoard(data.board);

            // Отображаем победителя, если он есть
            setTimeout(() => {
                if (data.winner !== "Undefined") alert(`Winner: ${data.winner}`);
            }, 100);
        });
    });

    async function startSession() {
        await fetch(`${API_BASE_URL}/api/game/start`, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(sessionId)
        });
    }

// Функция для загрузки состояния доски с сервера
    async function loadBoardState() {
        const response = await fetch(`${API_BASE_URL}/api/game/state`, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(sessionId)
        });
        const data = await response.json();
        console.log("Loaded board state:", data);

        // Обновляем доску
        updateBoard(data.board);
    }


    function updateBoard(board) {
        board.forEach((row, rowIndex) => {
            row.forEach((field, colIndex) => {
                const fieldElement = document.querySelector(`[data-row="${rowIndex}"][data-column="${colIndex}"]`);

                if (fieldElement) {
                    fieldElement.textContent = field.item === "Cross" ? "X" : field.item === "Zero" ? "O" : "";
                    fieldElement.style.fontSize = "100px";
                    fieldElement.style.color = "white";
                }
            });
        });
    }

});



