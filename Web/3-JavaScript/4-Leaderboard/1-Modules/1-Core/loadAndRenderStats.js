"use strict"

import {getStats} from "../2-Api/getStats.js";
import {renderLeaderboard} from "../3-Ui/renderLeaderboard.js";

export async function loadAndRenderStats(sort, count, page) {
    let usersData = {
        isLastPage: undefined,
        users: []
    };

    try {
        usersData = await getStats(sort, count, page);
    } catch (exception) {
        console.error(exception.message);
    }

    console.log(usersData);
    console.log(usersData.users)

    renderLeaderboard(usersData.users);

    return usersData.isLastPage;
}