'use strict';

const largestPrimeFactor = (number) => {  
    const maxPrimeRequired = Math.floor(Math.sqrt(number));
    const primes = generatePrimesUpTo(maxPrimeRequired);

    let largestPrime = 0;
    for (let i = primes.length - 1; i >= 0; i--) {
        if (number % primes[i] === 0) {
            largestPrime = primes[i];
            break;
        }
    }

    return largestPrime;
}

const generatePrimesUpTo = (maxPrimeRequired) => {
    const primes = [2, 3];

    for (let i = 5; i <= maxPrimeRequired; i += 2) {
        if (primes.every((p) => i % p)) {
            primes.push(i);
        }
    }

    return primes;
}
  
const result = largestPrimeFactor(600851475143);

console.log('Project Euler - Problem 3: ', result);