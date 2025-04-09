"use strict"

import {URL} from "../../../0-Common/url.js";

export async function getStats(sortType, count ,page) {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            skipModifier: page,
            usersCount: count,
            stringStatisticType: sortType
        })
    };

    const response = await fetch(`${url}/statistics`, requestData);
    if (!response.ok) {
        throw new Error("Error while getting stats");
    }

    const responseData = await response.json();

    return responseData;
}