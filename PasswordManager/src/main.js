import { zxcvbn, zxcvbnOptions } from "@zxcvbn-ts/core";
import * as common from "@zxcvbn-ts/language-common";
import * as langEn from "@zxcvbn-ts/language-en";

const commonDict = common.dictionary;
const langEnDict = langEn.dictionary;

zxcvbnOptions.setOptions({
    translations: langEn.translations,
    graphs: common.adjacencyGraphs,
    dictionary: { ...commonDict, ...langEnDict },
});

window.getPasswordStrength = (password) => {
    // this will load the dictionaries... vite really doesn't want to load them.
    const _ = Object.keys(common.dictionary).length + Object.keys(langEn.dictionary).length;
    const result = zxcvbn(password);
    return {
        score: result.score,
        feedback: result.feedback,
        crackTime: result.crackTimesDisplay?.offlineSlowHashing1e4PerSecond,
    };
};