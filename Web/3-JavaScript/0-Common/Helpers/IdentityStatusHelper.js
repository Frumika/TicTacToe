"use strict"

import {IdentityStatus} from "../Enums/IdentityStatus.js";


export class IdentityStatusHelper {

    static getCode(result) {
        if (!result || typeof result !== "object") {
            return IdentityStatus.UnknownFormat;
        }

        const code = result.code;
        if (typeof code !== "string") {
            return IdentityStatus.UnknownFormat;
        }

        const values = Object.values(IdentityStatus);
        if (!values.includes(code)) {
            return IdentityStatus.UnknownCode;
        }

        return code;
    }

    static is(result, ...expected) {
        const code = this.getCode(result);
        return expected.includes(code);
    }

    static isSuccess(result) {
        return this.getCode(result) === IdentityStatus.Success;
    }

    static isFailure(result) {
        return !this.isSuccess(result);
    }
}
