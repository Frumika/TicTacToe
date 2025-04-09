"use strict"


export function listenChevrons() {

    const leftChevron = document.getElementById("pagination-left");
    const rightChevron = document.getElementById("pagination-right");

    let chevronEvent;

    leftChevron.addEventListener("click", () => {
        chevronEvent = new Event("go-prev");
        document.dispatchEvent(chevronEvent);
    });

    rightChevron.addEventListener("click", () => {
        chevronEvent = new Event("go-next");
        document.dispatchEvent(chevronEvent);
    });


}