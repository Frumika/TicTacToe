"use strict"


export function drawClearBoard() {
    const fields = document.querySelectorAll(".board__field");

    fields.forEach((field) => {
        field.textContent = "";
    });
}