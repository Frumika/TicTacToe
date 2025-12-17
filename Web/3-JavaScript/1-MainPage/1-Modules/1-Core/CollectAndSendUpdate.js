"use strict"

import {getUserData} from "../2-Api/getUserData.js";
import {sendUpdate} from "../2-Api/sendUpdate.js";
import {GameSettings} from "./gameSettings.js";
import {END_GAME_TYPES} from "../../../0-Common/Helpers/endGameTypes.js";

export async function CollectAndSendUpdate(winner) {

    console.log(GameSettings.GameMode);

    if (GameSettings.GameMode === "PvE" && winner !== "Undefined") {
        switch (winner) {
            case "Cross": {
                try {
                    const userData = await getUserData();
                    const login = userData.login;

                    console.log(`Login: ${login}`);

                    if (login) await sendUpdate(login, END_GAME_TYPES.WIN);
                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }
            case "Zero": {
                try {
                    const userData = await getUserData();
                    const login = userData.login;

                    if (login) await sendUpdate(login, END_GAME_TYPES.LOSE);
                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }
            case "Draw": {
                try {
                    const userData = await getUserData();
                    const login = userData.login;

                    if (login) await sendUpdate(login, END_GAME_TYPES.DRAW);
                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }

            default: {
                try {
                    const userData = await getUserData();
                    const login = userData.login;
                    if (login) await sendUpdate(login, END_GAME_TYPES.UNDEFINED);
                } catch (error) {
                    console.error("Error While send update request");
                }

                break;
            }
        }
    }
}