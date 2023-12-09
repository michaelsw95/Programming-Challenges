var hands = File.ReadAllLines("./input.txt")
    .Select(ParseHand)
    .ToList();

hands.Sort(CompareHands);

var runningHandTotal = 0;
for (int i = 0; i < hands.Count; i++)
{
    runningHandTotal += hands[i].Bid * (i + 1);
}

Console.WriteLine($"Day 7 - Part - 1: {runningHandTotal}");

Hand ParseHand(string input)
{
    var inputParts = input.Split(' ');

    var type = GetCardType(inputParts[0]);
    
    return new Hand(inputParts[0], type, int.Parse(inputParts[1]));
}

CardType GetCardType(string cards)
{
    var cardValues = cards.ToCharArray();

    if (!cardValues.Contains('J'))
        return GetCalculatedCardType(cardValues);
    
    var cardType = GetCalculatedCardType(cardValues);

    var otherCardValues = cardValues.Where(value => value != 'J');

    foreach (var other in otherCardValues)
    {
        var updatedValues = cardValues.Select(value => value == 'J' ? other : value).ToArray();

        var updatedType = GetCalculatedCardType(updatedValues);

        if (updatedType < cardType)
        {
            cardType = updatedType;
        }
    }

    return cardType;
}

int CompareHands(Hand handOne, Hand handTwo)
{
    if (handOne.Type != handTwo.Type)
    {
        return (int)handOne.Type > (int)handTwo.Type ? -1 : 1;
    }

    var cardValues = new char[]
    {
        'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'
    };
    
    for (int i = 0; i < handOne.Cards.Length; i++)
    {
        var handOneCardIndex = Array.IndexOf(cardValues, handOne.Cards[i]);
        var handTwoCardIndex = Array.IndexOf(cardValues, handTwo.Cards[i]);

        if (handOneCardIndex == handTwoCardIndex)
        {
            continue;
        }

        return handOneCardIndex > handTwoCardIndex ? 1 : -1;
    }

    return 0;
}

CardType GetCalculatedCardType(char[] cardValues)
{
    var groups = cardValues.GroupBy(card => card).Select(cards => cards.ToArray()).ToArray();

    var typeChecks = new Dictionary<Func<bool>, CardType>
    {
        { () => groups.Length == 1 && groups[0].Length == 5, CardType.FiveOfAKind },
        { () => groups.Length == 2 && Enumerable.Any<char[]>(groups, cards => cards.Length == 4), CardType.FourOfAKind },
        { () => groups.Length == 2 && Enumerable.Any<char[]>(groups, cards => cards.Length == 3), CardType.FullHouse },
        { () => groups.Length == 3 && Enumerable.Any<char[]>(groups, cards => cards.Length == 3), CardType.ThreeOfAKind },
        { () => groups.Length == 3, CardType.TwoPair },
        { () => groups.Length == 4 && Enumerable.Any<char[]>(groups, cards => cards.Length == 2), CardType.OnePair }
    };

    foreach (var check in typeChecks.Where(check => check.Key()))
    {
        return check.Value;
    }

    return CardType.HighCard;
}

record Hand(string Cards, CardType Type, int Bid);

enum CardType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}
