"use strict"


import {GameMode, BotMode} from "../4-Events/listenGameSettings.js";


export function drawGameMode(gameMode) {
    const PvP = document.getElementById("mode-pvp");
    const PvE = document.getElementById("mode-pve");

    const PvpCircle = PvP.querySelector(".elem__circle");
    const PveCircle = PvE.querySelector(".elem__circle");

    const BotMode = document.querySelector(".params__bot-mode");

    if (gameMode === GameMode.PvP) {
        PvpCircle.style.background = "white";
        PveCircle.style.background = "black";

        BotMode.style.display = "none";

    } else {
        PvpCircle.style.background = "black";
        PveCircle.style.background = "white";
        BotMode.style.display = "flex";
    }
}


export function drawBotMode(botMode) {
    const Easy = document.getElementById("bot-easy");
    const Medium = document.getElementById("bot-medium");
    const Hard = document.getElementById("bot-hard");

    const EasyCircle = Easy.querySelector(".elem__circle");
    const MediumCircle = Medium.querySelector(".elem__circle");
    const HardCircle = Hard.querySelector(".elem__circle");


    if (botMode === BotMode.Easy) {
        EasyCircle.style.background = "white";
        MediumCircle.style.background = "black";
        HardCircle.style.background = "black";

    } else if (botMode === BotMode.Medium) {
        EasyCircle.style.background = "black";
        MediumCircle.style.background = "white";
        HardCircle.style.background = "black";
    } else {
        EasyCircle.style.background = "black";
        MediumCircle.style.background = "black";
        HardCircle.style.background = "white";
    }
}