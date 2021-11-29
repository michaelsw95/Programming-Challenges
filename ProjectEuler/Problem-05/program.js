'use strict';

const firstNumberWhichCanBeDividedByAllNumbersUpTo = (numberToDivideUpTo) => {
    let candidateNumber = 2;
    let allNumbersDivideEvenly = true;

    do {
        allNumbersDivideEvenly = true;

        for (let i = 2; i <= numberToDivideUpTo; i++) {
            if (candidateNumber % i !== 0) {
                allNumbersDivideEvenly = false;
                candidateNumber += 1;

                break;
            }
        }
    }
    while(!allNumbersDivideEvenly)

    return candidateNumber;
}

const result = firstNumberWhichCanBeDividedByAllNumbersUpTo(20);

console.log('Project Euler - Problem 5:', result);