"use strict"

import {getUserLogin} from "../2-Api/getUserInfo.js";
import {sendUpdate} from "../2-Api/sendUpdate.js";
import {GameSettings} from "./gameSettings.js";

export async function CollectAndSendUpdate(winner) {

    console.log(GameSettings.GameMode);

    if (GameSettings.GameMode === "PvE" && winner !== "Undefined") {
        switch (winner) {
            case "Cross": {
                try {
                    const login = await getUserLogin();

                    if (login) await sendUpdate(login, true);

                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }
            default: {
                try {
                    const login = await getUserLogin();

                    if (login) await sendUpdate(login, false);

                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }
        }
    }
}