namespace RPGCombat;

public abstract class BaseCharacter {
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int HealthPoints { get; protected set; }
    public RangeType RangeType { get; private set; }
    public List<Faction> Factions { get; private set; }

    public BaseCharacter(string name, int level, int healthPoints, RangeType rangeType) {
        Name = name;
        Level = level;
        HealthPoints = healthPoints;
        RangeType = rangeType;
        Factions = new List<Faction>();
    }
    public abstract void RetrieveDamage(int damagePoints);
    public abstract void RetrieveHealth(int healthPoints);
    public abstract void JoinFaction(Faction faction);
    public abstract void LeaveFaction(Faction faction);
    public bool TargetIsInRange(int distance) {
        return RangeType switch {
            RangeType.Melee => distance <= 2,
            RangeType.Ranged => distance <= 20
        };
    }
}