'use strict';

const generatePrimeCollection = (numberOfPrimesRequired) => {
    const primes = [2, 3];

    let i = 5;
    while (primes.length < numberOfPrimesRequired) {
        if (primes.every((p) => i % p)) {
            primes.push(i);
        }

        i += 2;
    }

    return primes;
}

const result = generatePrimeCollection(10001)[10000];

console.log('Project Euler - Problem 7', result);