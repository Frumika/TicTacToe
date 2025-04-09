"use strict"

import {URL} from "../../../0-Common/url.js";

export async function sendUpdate(login, isWin) {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            login: login,
            isWin: isWin
        })
    };

    const response = await fetch(`${url}/update`, requestData);

    if(!response.ok){
        throw new Error("Error while updating");
    }
}