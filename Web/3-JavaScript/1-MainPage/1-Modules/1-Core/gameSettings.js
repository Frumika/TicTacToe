"use strict"


export let GameSettings = {
    GameMode: "",
    BotMode: ""
}

export function setBaseGameMode() {
    const PvP = document.getElementById("mode-pvp");
    const PvE = document.getElementById("mode-pve");

    const PvpCircle = PvP.querySelector(".elem__circle");
    const PveCircle = PvE.querySelector(".elem__circle");

    PvpCircle.style.background = "white";
    PveCircle.style.background = "black";

    GameSettings.GameMode = "PvP";

    const BotMode = document.querySelector(".params__bot-mode");
    BotMode.style.display = "none";

}


export function setBaseBotMode() {
    const Easy = document.getElementById("bot-easy");
    const Medium = document.getElementById("bot-medium");
    const Hard = document.getElementById("bot-hard");

    const EasyCircle = Easy.querySelector(".elem__circle");
    const MediumCircle = Medium.querySelector(".elem__circle");
    const HardCircle = Hard.querySelector(".elem__circle");

    EasyCircle.style.background = "black";
    MediumCircle.style.background = "white";
    HardCircle.style.background = "black";

    GameSettings.BotMode = "Medium";
}