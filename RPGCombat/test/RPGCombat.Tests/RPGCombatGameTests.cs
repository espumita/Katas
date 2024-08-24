using FluentAssertions;

namespace RPGCombat.Tests;

public class RPGCombatGameTests {
    private readonly Position aPosition = new Position(0);
    private readonly Position aPositionWith2MetersOfDistance = new Position(2);
    private readonly Position aPositionWithMoreThan2MetersOfDistance = new Position(3);
    private readonly Position aPositionWith20MetersOfDistance = new Position(20);
    private readonly Position aPositionWithMoreThan20MetersOfDistance = new Position(21);

    [Test]
    public void game_start_with_characters_full_life() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        
        game.Spawn(aCharacter, aPosition);

        aCharacter.Level.Should().Be(1);
        aCharacter.HealthPoints.Should().Be(1000);
        aCharacter.Alive.Should().BeTrue();
    }

    [Test]
    public void characters_can_deal_damage_to_other_characters() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(950);
    }

    [Test]
    public void characters_die_when_retrieve_too_much_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);

        game.DealDamage(aCharacter, anotherCharacter, 2000);

        anotherCharacter.HealthPoints.Should().Be(0);
        anotherCharacter.Alive.Should().BeFalse();
    }

    [Test]
    public void characters_cannot_heal_another_character_too_much_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 500, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);

        game.Heal(aCharacter, anotherCharacter, 200);

        anotherCharacter.HealthPoints.Should().Be(500);
    }

    [Test]
    public void characters_can_only_heal_themselves() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 800, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);

        game.Heal(aCharacter, aCharacter, 50);

        aCharacter.HealthPoints.Should().Be(850);
    }

    [Test]
    public void characters_cannot_heal_more_than_maximum_health_points() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 500, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);

        game.Heal(aCharacter, aCharacter, 2000);

        aCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void characters_cannot_deal_damage_to_themself() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);

        game.DealDamage(aCharacter, aCharacter, 50);

        aCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void if_target_is_5_or_more_levels_above_the_attacker_damage_is_reduced_by_50_percent() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 6, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(975);
    }

    [Test]
    public void if_target_is_5_or_more_levels_below_the_attacker_damage_is_increased_by_50_percent() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 6, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(925);
    }

    [Test]
    public void melee_are_in_range_to_deal_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPositionWith2MetersOfDistance);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(950);
    }

    [Test]
    public void melee_not_in_range_do_not_deal_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPositionWithMoreThan2MetersOfDistance);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void range_are_in_range_to_deal_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Ranged);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPositionWith20MetersOfDistance);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(950);
    }

    [Test]
    public void range_are_not_in_range_to_deal_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Ranged);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPositionWithMoreThan20MetersOfDistance);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void characters_can_belong_to_different_factions_can_deal_damage_each_other() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);
        game.JoinFaction(aCharacter, Faction.TheSilverDawn);
        game.JoinFaction(anotherCharacter, Faction.TheIronCovenant);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(950);
    }

    [Test]
    public void characters_that_belong_to_common_faction_are_alies_and_cannot_fight() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);
        game.JoinFaction(aCharacter, Faction.TheSilverDawn);
        game.JoinFaction(anotherCharacter, Faction.TheSilverDawn);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void characters_that_leave_faction_can_fight_again() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 1000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);
        game.JoinFaction(aCharacter, Faction.TheSilverDawn);
        game.JoinFaction(anotherCharacter, Faction.TheSilverDawn);
        game.LeaveFaction(anotherCharacter, Faction.TheSilverDawn);

        game.DealDamage(aCharacter, anotherCharacter, 50);

        anotherCharacter.HealthPoints.Should().Be(950);
    }

    [Test]
    public void characters_that_belong_to_common_faction_are_alies_can_health_each_orther() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anotherCharacter = new Character("Player2", 1, 500, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anotherCharacter, aPosition);
        game.JoinFaction(aCharacter, Faction.TheSilverDawn);
        game.JoinFaction(anotherCharacter, Faction.TheSilverDawn);

        game.Heal(aCharacter, anotherCharacter, 200);

        anotherCharacter.HealthPoints.Should().Be(700);
    }

    [Test]
    public void characters_can_deal_damage_to_non_characters_things() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anNPC = new NPC("Tree", 1, 2000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anNPC, aPosition);

        game.DealDamage(aCharacter, anNPC, 50);

        anNPC.HealthPoints.Should().Be(1950);
        anNPC.Destroyed.Should().BeFalse();
    }

    [Test]
    public void npcs_cannot_deal_damage() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anNPC = new NPC("Tree", 1, 2000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anNPC, aPosition);

        game.DealDamage(anNPC, aCharacter, 50);

        aCharacter.HealthPoints.Should().Be(1000);
    }

    [Test]
    public void npcs_cannot_be_healed() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anNPC = new NPC("Tree", 1, 1800, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anNPC, aPosition);

        game.Heal(aCharacter, anNPC, 200);

        anNPC.HealthPoints.Should().Be(1800);
    }

    [Test]
    public void when_npcs_health_is_zero_they_are_destroyed() {
        var game = new RPGCombatGame();
        var aCharacter = new Character("Player1", 1, 1000, RangeType.Melee);
        var anNPC = new NPC("Tree", 1, 2000, RangeType.Melee);
        game.Spawn(aCharacter, aPosition);
        game.Spawn(anNPC, aPosition);

        game.DealDamage(aCharacter, anNPC, 2000);

        anNPC.HealthPoints.Should().Be(0);
        anNPC.Destroyed.Should().BeTrue();
    }
}