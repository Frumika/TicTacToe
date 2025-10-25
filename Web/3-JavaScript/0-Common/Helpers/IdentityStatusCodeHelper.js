"use strict"

import {IdentityStatusCode} from "../Enums/IdentityStatusCode.js";


export class IdentityStatusCodeHelper {

    static getCode(result) {
        if (!result || typeof result !== "object") {
            return IdentityStatusCode.UnknownFormat;
        }

        const code = result.code;
        if (typeof code !== "string") {
            return IdentityStatusCode.UnknownFormat;
        }

        const values = Object.values(IdentityStatusCode);
        if (!values.includes(code)) {
            return IdentityStatusCode.UnknownCode;
        }

        return code;
    }

    static is(result, ...expected) {
        const code = this.getCode(result);
        return expected.includes(code);
    }

    static isSuccess(result) {
        return this.getCode(result) === IdentityStatusCode.Success;
    }

    static isFailure(result) {
        return !this.isSuccess(result);
    }
}
