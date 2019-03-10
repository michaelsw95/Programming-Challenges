'use strict';

const largestPalindromeProduct = (lowBound, upperBound) => {
    const isPalindrome = (number) =>
        number.toString().split('').reverse().join('') === number.toString();

    let largestPalindrome = 0;
    
    for (let i = upperBound; i > lowBound; i--) {
        for (let j = upperBound; j > lowBound; j--) {
            const product = i * j;

            if (product > largestPalindrome && isPalindrome(product)) {
                largestPalindrome = product;
            }
        }
    }

    return largestPalindrome;
}

const result = largestPalindromeProduct(100, 999);

console.log('Project Euler - Problem 4: ' + result);