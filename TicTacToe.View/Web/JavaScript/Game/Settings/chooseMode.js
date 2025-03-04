"use strict"

document.addEventListener("DOMContentLoaded", function () {

    const modePvp = document.getElementById("mode-pvp");
    const modePve = document.getElementById("mode-pve");

    function selectMode(selectedMode) {
        modePvp.querySelector(".elem__circle").style.background = "transparent";
        modePve.querySelector(".elem__circle").style.background = "transparent";

        selectedMode.querySelector(".elem__circle").style.background = "white";
    }

    selectMode(modePvp);

    [modePvp, modePve].forEach(mode => {
        mode.querySelector(".elem__circle").addEventListener("click", () => selectMode(mode));
        mode.querySelector(".elem__text").addEventListener("click", () => selectMode(mode));
    });
})

