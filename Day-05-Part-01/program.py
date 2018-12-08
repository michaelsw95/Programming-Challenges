def findReactionPair(dataString):
    pairIndex = -1

    for index, character in enumerate(dataString):
        characterToCompare = ""
        if (character.isupper()):
            characterToCompare = character.lower()
        else:
            characterToCompare = character.upper()

        if (characterMatchesToTheRight(dataString, index, characterToCompare)):
            pairIndex = index
            break
        elif (characterMatchesToTheLeft(dataString, index, characterToCompare)):
            pairIndex = index - 1
            break
    
    return pairIndex

def characterMatchesToTheRight(dataString, index, character):
    return index < len(dataString) - 1 and dataString[index + 1] == character
    
def characterMatchesToTheLeft(dataString, index, character):
    return index > 0 and dataString[index - 1] == character

def removeCharacterAt(indexOfCharacterPair, str):
    return str[:indexOfCharacterPair] + str[indexOfCharacterPair+2:]

def main():
    inputFile = open("input.txt", "r")
    rawStringData = inputFile.read()
    stringData = rawStringData.strip()

    while (len(stringData) > 0):
        indexOfReactionPair = findReactionPair(stringData)

        if (indexOfReactionPair == -1):
            break;

        stringData = removeCharacterAt(indexOfReactionPair, stringData)

    print("Advent of Code - Day 05 - Part 01: {0}".format(len(stringData)))

if __name__ == "__main__":
    main()