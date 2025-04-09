"use strict"

import {listenSortButton} from "./1-Modules/4-Events/listenSortButtons.js";
import {loadAndRenderStats} from "./1-Modules/1-Core/loadAndRenderStats.js";
import {drawCurrentPage} from "./1-Modules/3-Ui/drawCurrnetPage.js";
import {listenChevrons} from "./1-Modules/4-Events/listenChevrons.js";


export let SortType = {
    ByMatches: "ByMatches",
    ByWins: "ByWins",
    ByLosses: "ByLosses"
}

export let Params = {
    SortType: SortType.ByMatches,
    UsersCount: 2,
    CurrentPage: 0,
    IsLastPage: true,
}


document.addEventListener("DOMContentLoaded", async () => {
    listenSortButton();
    listenChevrons();

    let isLast = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);

    drawCurrentPage(Params.CurrentPage + 1, isLast);

    console.log(`IS LAST: ${isLast}`);

    Params.IsLastPage = isLast;
});


document.addEventListener("sort-click", async () => {
    await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);
});


document.addEventListener("go-prev", async () => {
    Params.CurrentPage--;

    let isLast = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);

    Params.IsLastPage = isLast;

    drawCurrentPage(Params.CurrentPage + 1, isLast);
});


document.addEventListener("go-next", async () => {
    Params.CurrentPage++;
    let isLast = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);

    Params.IsLastPage = isLast;

    drawCurrentPage(Params.CurrentPage + 1, isLast);
});