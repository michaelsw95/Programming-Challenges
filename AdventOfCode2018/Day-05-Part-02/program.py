import re

def findReactionPair(dataString):
    pairIndex = -1

    for index, character in enumerate(dataString):
        charToCompareAgainst = ""
        if (character.isupper()):
            charToCompareAgainst = character.lower()
        else:
            charToCompareAgainst = character.upper()

        if (index < len(dataString) - 1 and dataString[index + 1] == charToCompareAgainst):
            pairIndex = index
            break
        elif (index > 0 and dataString[index - 1] == charToCompareAgainst):
            pairIndex = index - 1
            break
    
    return pairIndex

def iterateReactionPairRemoval(stringData):
    while (len(stringData) > 0):
        indexOfReactionPair = findReactionPair(stringData)

        if (indexOfReactionPair == -1):
            break;

        stringData = removeCharacterAt(indexOfReactionPair, stringData)

    return len(stringData)

def removeCharacterAt(indexOfCharacterPair, str):
    return str[:indexOfCharacterPair] + str[indexOfCharacterPair+2:]

def main():
    inputFile = open("input.txt", "r")
    rawStringData = inputFile.read()
    stringData = rawStringData.strip()

    charset = [chr(i) for i in range(ord('a'),ord('z')+1)]
    
    minChar = ""
    minValue = 0

    for char in charset:
        stringAfterCharRemoval = re.sub("(?i)" + char, "", stringData)  
        countAfterReaction = iterateReactionPairRemoval(stringAfterCharRemoval)

        if (countAfterReaction < minValue or minChar == ""):
            minChar = char
            minValue = countAfterReaction

    print("Advent of Code - Day 05 - Part 02: {0} {1}".format(minChar, minValue))

if __name__ == "__main__":
    main()