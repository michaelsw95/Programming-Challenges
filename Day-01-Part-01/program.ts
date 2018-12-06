import * as fs from 'fs'; 

const plus: string = "+";
const minus: string = "-";

const main = (): void => {
    const frequencies = readFrequenciesFromFile("./input.txt");
    let totalFrequency = computeFrequencyTotal(frequencies);

    console.log(`Day 1 - Part 1: ${totalFrequency}`);
}

const computeFrequencyTotal = (frequencies: string[]): number => {
    let totalFrequency = 0;

    for (const frequency of frequencies) {
        const currentFrequencySymbol = frequency.charAt(0);
        const currentFrequencyValue = Number.parseInt(frequency.substr(1, frequency.length));
        
        if (currentFrequencySymbol === plus) {
            totalFrequency += currentFrequencyValue;
        } else if (currentFrequencySymbol === minus) {
            totalFrequency -= currentFrequencyValue;
        }
    }

    return totalFrequency;
}

const readFrequenciesFromFile = (filepath: string): string[] => {
    const frequencies = fs.readFileSync(filepath)
        .toString()
        .split("\n");

    return frequencies;
}

main();