const iterateCandidatePythagoreanTriplets = () => {
    const isPythagoreanTriplet = (i, j, k) => {
        if (i + j + k === 1000) {
            return Math.pow(i, 2) + Math.pow(j, 2) === Math.pow(k, 2);
        }

        return false;
    }

    for (let i = 0; i < 333; i++) {
        for (let j = i; j < i + 333; j++) {
            for (let k = j; k < j + 333; k++) {
                if (isPythagoreanTriplet(i, j, k)) {
                    return i * j * k;
                }
            }
        }
    } 
}

const result = iterateCandidatePythagoreanTriplets();

console.log('Project Euler - Problem 9:', result);