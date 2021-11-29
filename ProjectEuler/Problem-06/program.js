'use strict';

const sumOfAllIntegersBetween = (lower, upper) => {
    let sum = 0;

    for (let i = lower; i <= upper; i++) {
        sum += i;
    }

    return sum;
}

const squareSumOfAllIntegersBetween = (lower, upper) => {
    let sum = 0;

    for (let i = lower; i <= upper; i++) {
        sum += Math.pow(i, 2);
    }

    return sum;
}

const result = Math.pow(sumOfAllIntegersBetween(0, 100), 2) - squareSumOfAllIntegersBetween(0, 100);

console.log('Project Euler - Problem 6:', result);