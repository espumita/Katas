namespace PokerHands;

public record Hand(string playerName, List <Card> cards) {
    public HandRank Rank() {
        var groupsOfSuits = LookForSuitRepetition();
        if (IsStraight() && IsFlush(groupsOfSuits)) {
            return IsRoyalStraight()
                ? HandRank.RoyalFlush 
                : HandRank.StraightFlush;
        }
        var groupsOfValues = LookForValueRepetition();
        if (IsFourOfAKind(groupsOfValues)) return HandRank.FourOfAKind;
        if (IsThreeOfAKind(groupsOfValues) && IsPair(groupsOfValues)) return HandRank.FullHouse;
        if (IsFlush(groupsOfSuits)) return HandRank.Flush;
        if (IsStraight()) return HandRank.Straight;
        if (IsThreeOfAKind(groupsOfValues)) return HandRank.ThreeOfAKind;
        if (IsTwoPairs(groupsOfValues)) return HandRank.TwoPairs;
        return IsPair(groupsOfValues) 
            ? HandRank.Pair 
            : HandRank.HighCard;
    }

    private IEnumerable<IGrouping<Suit, Card>> LookForSuitRepetition() {
        return cards.GroupBy(card => card.suit);
    }

    private bool IsStraight() {
        var ascendingOrderedHand = cards.OrderByDescending(card => card.value).Reverse().ToList();
        for (int i = 0; i < ascendingOrderedHand.Count() - 1; i++) {
            if (((int) ascendingOrderedHand[i+1].value) != ((int) ascendingOrderedHand[i].value + 1)) return false;
        }
        return true;
    }

    private static bool IsFlush(IEnumerable<IGrouping<Suit, Card>> groupsOfSuits) {
        return groupsOfSuits.Count() == 1;
    }

    private bool IsRoyalStraight() {
        var orderedHand = cards.OrderByDescending(card => card.value).ToList();
        return orderedHand.First().value == CardValue.Ace;
    }

    private IEnumerable<IGrouping<CardValue, Card>> LookForValueRepetition() {
        return cards.GroupBy(card => card.value);
    }

    private static bool IsFourOfAKind(IEnumerable<IGrouping<CardValue, Card>> groupsOfValues) {
        var fourOfAKind = groupsOfValues.Where(group => group.Count() == 4);
        return fourOfAKind.Count() == 1;
    }

    private static bool IsThreeOfAKind(IEnumerable<IGrouping<CardValue, Card>> groupsOfValues) {
        var threeOfAKind = groupsOfValues.Where(group => group.Count() == 3);
        return threeOfAKind.Count() == 1;
    }

    private static bool IsPair(IEnumerable<IGrouping<CardValue, Card>> groupsOfValues) {
        var pairs = groupsOfValues.Where(group => group.Count() == 2);
        return pairs.Count() == 1;
    }

    private static bool IsTwoPairs(IEnumerable<IGrouping<CardValue, Card>> groupsOfValues) {
        var pairs = groupsOfValues.Where(group => group.Count() == 2);
        return pairs.Count() == 2;
    }
}