'use strict';

const sumOfEvenFibonacciNumbers = (lastFibNumber, currentFibNumber, sum, maxValue) => {
    const nextFibNumber = lastFibNumber + currentFibNumber;

    if (nextFibNumber > maxValue) {
        return sum;
    }

    const newSum = nextFibNumber % 2 === 0 ?
        nextFibNumber + sum : sum;

    return sumOfEvenFibonacciNumbers(currentFibNumber, nextFibNumber, newSum, maxValue);
}
  
const result = sumOfEvenFibonacciNumbers(1, 1, 0, 4000000);

console.log('Project Euler - Problem 2: ' + result);
