using System;

namespace RPGCombat;

public class NPC : BaseCharacter {

    public bool Destroyed { get; private set; }
    public NPC(string name, int level, int healthPoints, RangeType rangeType) : base(name, level, healthPoints, rangeType) {
        Destroyed = false;
    }

    public override void RetrieveDamage(int damagePoints) {
        if (damagePoints >= HealthPoints) {
            HealthPoints = 0;
            Destroyed = true;
        } else {
            HealthPoints -= damagePoints;
        }
    }

    public override void RetrieveHealth(int healthPoints) { }
    
    public override void JoinFaction(Faction faction) { }
    
    public override void LeaveFaction(Faction faction) { }
}