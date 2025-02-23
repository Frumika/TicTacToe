"use strict"

document.addEventListener("DOMContentLoaded", () => {
    const sessionId = sessionStorage.getItem("sessionId") || crypto.randomUUID();
    sessionStorage.setItem("sessionId", sessionId);

    const fields = document.querySelectorAll(".board__field");

    fields.forEach((field) => {
        field.addEventListener("click", async () => {
            const row = parseInt(field.dataset.row);
            const column = parseInt(field.dataset.column);

            console.log(`Response: ${row} : ${column}`)

            const API_BASE_URL = "http://localhost:5026";
            const response = await fetch(`${API_BASE_URL}/api/game/move`, {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({sessionId, row, column})
            });

            const data = await response.json();
            console.log(data);
        });
    });


    /*function updateBoard(boardState) {
        boardState.forEach((row, rowIndex) => {
            row.forEach((cellValue, colIndex) => {
                const cell = document.querySelector(
                    `.cell[data-row="${rowIndex}"][data-col="${colIndex}"]`
                );

                if (cellValue === 1) cell.textContent = "X";
                else if (cellValue === 2) cell.textContent = "O";
                else cell.textContent = "";

            });
        });
    }*/
});
