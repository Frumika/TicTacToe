"use strict"

import {renderLeaderboard} from "./1-Modules/3-Ui/renderLeaderboard.js";
import {getStats} from "./1-Modules/2-Api/getStats.js";

let SortType = {
    ByMatches: "ByMatches",
    ByWins: "ByWins",
    ByLosses: "ByLosses"
}

let LeaderboardSetting = {
    SortType: SortType.ByMatches,
    CurrentPage: 0,
}


document.addEventListener("DOMContentLoaded", async () => {

    let usersData;
    try {
        usersData = await getStats(LeaderboardSetting.SortType, LeaderboardSetting.CurrentPage);
    } catch (exception) {
        console.error(exception.error);
    }
    renderLeaderboard(usersData);
});