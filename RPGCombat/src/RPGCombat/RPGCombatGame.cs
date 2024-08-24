namespace RPGCombat;

public class RPGCombatGame {
    private List<PositionedCharacter> charactersInCombatArea;

    public RPGCombatGame() {
        charactersInCombatArea = new List<PositionedCharacter>();
    }

    public void Spawn(BaseCharacter character, Position position) {
        charactersInCombatArea.Add(new PositionedCharacter(character, position));
    }

    public void DealDamage(BaseCharacter origin, BaseCharacter target, int damagePoints) {
        if (IsTheSame(origin, target)) return;
        if (AreAllies(origin, target)) return;
        if (!TargetIsInRange(origin, target)) return;
        if (IsNPCObject(origin)) return;
        var calculatedDamagePoints = CalculateDamagePoints(origin, target, damagePoints);
        target.RetrieveDamage(calculatedDamagePoints);
    }

    public void Heal(BaseCharacter origin, BaseCharacter target, int healthPoints) {
        if (IsTheSame(origin, target) || AreAllies(origin, target)) {
            target.RetrieveHealth(healthPoints);
        };
    }

    public void JoinFaction(BaseCharacter character, Faction faction) {
        character.JoinFaction(faction);
    }

    public void LeaveFaction(BaseCharacter character, Faction faction) {
        character.LeaveFaction(faction);
    }

    private static bool IsTheSame(BaseCharacter origin, BaseCharacter target) {
        return origin.Name == target.Name;
    }

    private bool AreAllies(BaseCharacter origin, BaseCharacter target) {
        return origin.Factions.Any(faction => target.Factions.Contains(faction));
    }

    private bool TargetIsInRange(BaseCharacter origin, BaseCharacter target) {
        var originPosition = PositionFrom(origin);
        var targetPosition = PositionFrom(target);
        var distance = originPosition.CalculateGameDistanceWith(targetPosition);
        return origin.TargetIsInRange(distance);
    }

    private static bool IsNPCObject(BaseCharacter origin) {
        return origin.GetType() == typeof(NPC);
    }

    private Position PositionFrom(BaseCharacter character) {
        return charactersInCombatArea
            .Single(p => p.character.Name.Equals(character.Name))
            .position;
    }

    private int CalculateDamagePoints(BaseCharacter origin, BaseCharacter target, int damagePoints) {
        if (target.Level - origin.Level >= 5) return (int) (damagePoints * 0.5);
        if (origin.Level - target.Level >= 5) return (int) (damagePoints * 1.5);
        return damagePoints;
    }
}