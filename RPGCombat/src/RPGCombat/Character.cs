namespace RPGCombat;

public class Character : BaseCharacter {

    public bool Alive { get; private set; }

    public Character(string name, int level, int healthPoints, RangeType rangeType) : base(name, level, healthPoints, rangeType) {
        this.Alive = true;
    }

    public override void RetrieveDamage(int damagePoints) {
        if (damagePoints >= HealthPoints) {
            HealthPoints = 0;
            Alive = false;
        }
        else {
            HealthPoints -= damagePoints;
        }
    }

    public override void RetrieveHealth(int healthPoints) {
        if (healthPoints >= HealthPoints) {
            HealthPoints = 1000;
        } else {
            HealthPoints += healthPoints;
        }
    }

    public override void JoinFaction(Faction faction) {
        Factions.Add(faction);
    }

    public override void LeaveFaction(Faction faction) {
        Factions.Remove(faction);
    }
}