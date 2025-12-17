"use strict"

import {addHead} from "../0-Common/addHead.js";
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
    UsersCount: 10,
    CurrentPage: 0,
    IsLastPage: true,
}


document.addEventListener("DOMContentLoaded", async () => {
    try {
        await addHead({
            title: "Leaderboard",
            styles: ["../2-CSS/leaderboard.css"]
        });

    } catch (err) {
        console.error(err.message)
    }

    listenSortButton();
    listenChevrons();

    Params.IsLastPage = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);
    drawCurrentPage(Params.CurrentPage + 1, Params.IsLastPage);
});


document.addEventListener("sort-click", async () => {
    await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);
});


document.addEventListener("go-prev", async () => {
    Params.CurrentPage--;
    Params.IsLastPage = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);
    drawCurrentPage(Params.CurrentPage + 1, Params.IsLastPage);
});


document.addEventListener("go-next", async () => {
    Params.CurrentPage++;
    Params.IsLastPage = await loadAndRenderStats(Params.SortType, Params.UsersCount, Params.CurrentPage);
    drawCurrentPage(Params.CurrentPage + 1, Params.IsLastPage);
});