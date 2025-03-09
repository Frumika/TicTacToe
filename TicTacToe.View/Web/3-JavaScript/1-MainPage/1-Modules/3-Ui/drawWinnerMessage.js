"use strict"


import {UpdateMode} from "./updateGameInfo.js";

export function drawWinnerMessage(winner, updateMode) {

    let winnerBlock = document.querySelector(".board-container__winner-info");
    let winnerText = document.querySelector(".winner-info__text");


    if (winner !== "Undefined") {

        if (updateMode === UpdateMode.MakeMove) {
            setTimeout(() => {
                winnerBlock.style.visibility = "visible"
            }, 200)
        }
        else{
            winnerBlock.style.visibility = "visible";
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

export function hiddenWinner() {
    let winnerBlock = document.querySelector(".board-container__winner-info");
    winnerBlock.style.visibility = "hidden"
}