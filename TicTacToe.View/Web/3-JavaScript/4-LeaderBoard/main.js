"use strict"

import {renderLeaderboard} from "./1-Modules/3-Ui/renderLeaderboard.js";
import {getStats} from "./1-Modules/2-Api/getStats.js";


document.addEventListener("DOMContentLoaded", async () => {

    let usersData;
    try {
        usersData = await getStats()
    } catch (exception) {
        console.error(exception.error);
    }
    renderLeaderboard(usersData);
});