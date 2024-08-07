namespace Bowling;

public class Frame {
    public int Number { get; set; }
    public List<Roll> Rolls { get; set; }
    public FrameStatus Status { get; set; }
    public int LastCalculatedScore { get; set; }

    public Frame(int number) {
        Number = number;
        Rolls = new List<Roll>();
    }

    public bool IsCompleted() {
        if (IsLastFrame()) return false;
        return Status != FrameStatus.InProgress;
    }

    public bool IsLastFrame() {
        return Number == 9;
    }

    public void AddRoll(Roll roll) {
        Rolls.Add(roll);
        if (IsAStrike(roll)) {
            Status = FrameStatus.Strike;
            return;
        }
        if (IsASpare()) {
            Status = FrameStatus.Spare;
            return;
        }
        if (AllRollsAreCompleted()) {
            Status = FrameStatus.Completed;
            return;
        }
        Status = FrameStatus.InProgress;
    }

    public int Score() {
        return Rolls.Sum(roll => roll.KnockDownPins);
    }

    private bool IsAStrike(Roll roll) {
        return Rolls.Count == 1 && roll.KnockDownPins == 10;
    }

    private bool IsASpare() {
        return Rolls.Count == 2 && Rolls.Sum(roll => roll.KnockDownPins) == 10;
    }

    private bool AllRollsAreCompleted() {
        return Rolls.Count == 2;
    }
}