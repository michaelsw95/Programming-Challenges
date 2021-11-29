'use strict';

const sumOfAllMultiples = (multiple, upperLimit, currentSum = 0, currentIteration = 0) => {
    if (upperLimit === currentIteration) {
        return currentSum;
    }

    const newSum = currentIteration % multiple === 0 ?
        currentSum + currentIteration : currentSum;

    return sumOfAllMultiples(multiple, upperLimit, newSum, currentIteration + 1);
}

const result = sumOfAllMultiples(3, 1000) + sumOfAllMultiples(5, 1000);

console.log('Project Euler - Problem 1: ' + result);
