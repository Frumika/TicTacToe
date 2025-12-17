"use strict"

import {URL} from "../../../0-Common/url.js";
import {IdentityStatusHelper} from "../../../0-Common/Helpers/IdentityStatusHelper.js";

export async function getStats(sortType, count, page) {
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
    const result = await response.json();

    if (IdentityStatusHelper.isFailure(result)) {
        throw new Error(result.message)
    }

    return result;
}