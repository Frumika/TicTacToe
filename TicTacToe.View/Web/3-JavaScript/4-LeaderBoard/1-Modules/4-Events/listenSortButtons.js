"use strict"


import {Params, SortType} from "../../main.js";

export function listenSortButton() {
    const buttons = document.querySelectorAll(".sort-button");

    buttons.forEach(button => {
        button.addEventListener("click", () => {

            switch (button.id) {
                case "by-matches": {
                    Params.SortType = SortType.ByMatches;
                    break;
                }
                case "by-wins": {
                    Params.SortType = SortType.ByWins;
                    break;
                }
                default: {
                    Params.SortType = SortType.ByLosses;
                }
            }

            const sortClickEvent = new Event("sort-click");
            document.dispatchEvent(sortClickEvent);
        })
    })
}