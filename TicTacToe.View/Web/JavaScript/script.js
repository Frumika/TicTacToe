"use strict"

document.addEventListener("DOMContentLoaded", () => {
    const board = document.getElementById("board");
    const fields = document.querySelectorAll(".cell");

    fields.forEach((cell) => {
        cell.addEventListener("click", async () => {
            const row = parseInt(cell.dataset.row);
            const col = parseInt(cell.dataset.col);

            const response = await fetch("/api/game/move", {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify({row, col})
            });

            if (response.ok) {
                const gameState = await response.json();

                // Если сервер подтвердил ход, обновляем доску
                if (gameState.moveMade) {
                    updateBoard(gameState.board);
                }
            }
        });
    });


    function updateBoard(boardState) {
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
    }
});
