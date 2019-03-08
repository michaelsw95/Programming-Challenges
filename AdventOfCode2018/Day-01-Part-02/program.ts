import * as fs from 'fs'; 

const plus: string = "+";
const minus: string = "-";

const main = (): void => {
    const frequencies = readFrequenciesFromFile("./input.txt");
    let firstDuplicate = findFirstDuplicateFrequency(frequencies);

    console.log(`Day 1 - Part 2: ${firstDuplicate}`);
}

const findFirstDuplicateFrequency = (frequencies: string[]): number => {
    const previousFrequencies = new Set<number>();
    let currentFrequency = 0;
    let duplicate: number;

    while (!duplicate) {
        for (const frequency of frequencies) {
            const currentFrequencySymbol = frequency.charAt(0);
            const currentFrequencyValue = Number.parseInt(frequency.substr(1, frequency.length));
            
            if (currentFrequencySymbol === plus) {
                currentFrequency += currentFrequencyValue;
            } else if (currentFrequencySymbol === minus) {
                currentFrequency -= currentFrequencyValue;
            }
    
            if (previousFrequencies.has(currentFrequency)) {
                duplicate = currentFrequency;
                break;
            }
    
            previousFrequencies.add(currentFrequency);
        }
    }

    return duplicate;
}

const readFrequenciesFromFile = (filepath: string): string[] => {
    const frequencies = fs.readFileSync(filepath)
        .toString()
        .split("\n");

    return frequencies;
}

main();