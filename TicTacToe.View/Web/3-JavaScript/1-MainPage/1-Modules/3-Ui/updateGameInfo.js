"use strict"


import {getGameState} from "../2-Api/getGameState.js";

// Функции
export {updateBoard, updateGameState};

// Перечисление
export {UpdateMode};



// Перечисление вариантов обновления доски
const UpdateMode = {
    MakeMove: 0,
    UpdateAllFields: 1,
}


// Обновление доски
function updateBoard(board, updateMode) {
    board.forEach((row, rowIndex) => {
        row.forEach((field, colIndex) => {
            const fieldElement = document.querySelector(`[data-row="${rowIndex}"][data-column="${colIndex}"]`);

            if (fieldElement) {
                fieldElement.style.fontSize = "100px";
                fieldElement.style.color = "white";

                if (field.item === "Cross") {
                    fieldElement.textContent = "X";
                } else if (field.item === "Zero") {

                    if (updateMode === UpdateMode.MakeMove) {
                        setTimeout(() => {
                            fieldElement.textContent = "O"
                        }, 200)
                    } else {
                        fieldElement.textContent = "O";
                    }
                } else {
                    fieldElement.textContent = "";
                }
            }
        });
    });
}


// Получение запроса от сервера и обновление доски
async function updateGameState(updateMode) {
    let boardData = null;

    try {
        boardData = await getGameState();
    } catch (error) {
        throw new Error(error.message);
    }

    if (boardData && boardData.board) updateBoard(boardData.board, updateMode);
    else throw new Error("Failed to load board data.");
}

