"use strict"


export function drawCurrentPage(page, isLast) {
    const number = document.querySelector(".pagination__number");
    number.textContent = "Page_" + page;

    console.log(`INFO: ${page} : ${isLast}`)

    const leftChevron = document.getElementById("pagination-left");
    const rightChevron = document.getElementById("pagination-right");

    if (isLast) rightChevron.style.visibility = "hidden";
    else rightChevron.style.visibility = "visible";

    if (page === 1) leftChevron.style.visibility = "hidden";
    else leftChevron.style.visibility = "visible";
}