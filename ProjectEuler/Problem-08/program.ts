import * as fs from 'fs'; 

const main = (): void => {
    const inputData = readFromFile("./input.txt");
    const inputDataAsNumberArray = parseStringGrid(inputData);
    const largestProduct = findLargestProductOfSeries(inputDataAsNumberArray, 13);

    console.log('Project Euler - Problem 8:', largestProduct);
}

const readFromFile = (filePath: string): string[] => fs.readFileSync(filePath)
    .toString()
    .split("\n");

const parseStringGrid = (inputData: string[]): number[] => {
    const listOfNumbers: number[] = [];
    
    inputData.forEach(line => {
        line.split('')
            .filter(isNumeric)
            .forEach(letter => {
                const parsedLetter = Number.parseInt(letter);
                
                listOfNumbers.push(parsedLetter);
            });
    });

    return listOfNumbers;
}

const isNumeric = (candidate: string): boolean => {
    return /^-{0,1}\d+$/.test(candidate);
}

const findLargestProductOfSeries = (grid: number[], seriesSize: number) => {
    let largestSeriesProduct = 0;

    for (let i = 0; i < grid.length; i++) {
        let seriesProduct = grid[i];
        
        for (let j = i + 1; j < i + seriesSize; j++) {
            seriesProduct *= grid[j];
        }

        if (seriesProduct > largestSeriesProduct) {
            largestSeriesProduct = seriesProduct;
        }
    }

    return largestSeriesProduct;
}

main();