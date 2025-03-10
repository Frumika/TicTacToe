"use strict"


import {UpdateMode} from "./updateGameInfo.js";

export function drawWinnerMessage(winner, updateMode) {

    let winnerBlock = document.querySelector(".board-container__winner-info");
    let winnerText = document.querySelector(".winner-info__text");


    if (winner !== "Undefined") {

        if (updateMode === UpdateMode.MakeMove) {
            setTimeout(() => {
                winnerBlock.style.display = "flex"
            }, 150)
        }
        else{
            winnerBlock.style.display = "flex";
        }

        switch (winner) {
            case "Cross": {
                winnerText.textContent = "Cross is Win!";
                break;
            }
            case "Zero": {
                winnerText.textContent = "Zero is Win!";
                break;
            }

            default: {
                winnerText.textContent = "Draw";
            }
        }
    }
}

export function hiddenWinnerMessage() {
    let winnerBlock = document.querySelector(".board-container__winner-info");
    winnerBlock.style.display = "none"
}