namespace Bowling;

public class FrameScores {
    public Frame[] Frames { get; set; }
    public int CurrentFrame { get; set; }
    public int Score { get; set; }

    public FrameScores() {
        Frames = Enumerable.Range(0, 10).Select(number => new Frame(number)).ToArray();
    }

    public void Roll(int knockDownPins) {
        var roll = new Roll(knockDownPins);
        var currentFrame = CalculateCurrentFrame();
        currentFrame.AddRoll(roll);
        Score = CalculateScore();
    }

    public bool PlayerEnds() {
        return Frames[CurrentFrame].IsLastFrame()
               && LastFrameIsCompleted();
    }

    private Frame CalculateCurrentFrame() {
        if (!Frames[CurrentFrame].IsCompleted()) {
            return Frames[CurrentFrame];
        }
        CurrentFrame++;
        return Frames[CurrentFrame];
    }

    private int CalculateScore() {
        return Frames.Select((frame, frameIndex) => {
            var frameScore = CalculateFrameScore(frame, frameIndex);
            frame.LastCalculatedScore = frameScore;
            return frameScore;
        }).Sum(score => score);
    }

    private int CalculateFrameScore(Frame frame, int frameIndex) {
        if (frame.IsLastFrame()) return frame.Score();
        return frame.Status switch {
            FrameStatus.Spare => frame.Score() + BonusScore(frameIndex + 1, 0),
            FrameStatus.Strike => frame.Score() + BonusScore(frameIndex + 1, 0) 
                                                + (RollExists(frameIndex + 1, 1)
                                                    ? BonusScore(frameIndex + 1, 1)
                                                    : BonusScore(frameIndex + 2, 0)),
            _ => frame.Score()
        };
    }

    private int BonusScore(int frameIndex, int rollIndex) {
        return RollExists(frameIndex, rollIndex)
            ? Frames[frameIndex].Rolls[rollIndex].KnockDownPins
            : 0;
    }

    private bool RollExists(int frameIndex, int rollIndex) {
        return frameIndex < Frames.Length 
               && Frames[frameIndex].Rolls.Count >= (rollIndex + 1);
    }

    private bool LastFrameIsCompleted() {
        return (Frames[CurrentFrame].Rolls.Count == 3)
            || (Frames[CurrentFrame].Rolls.Count == 2 && Frames[CurrentFrame].Rolls[0].KnockDownPins + Frames[CurrentFrame].Rolls[1].KnockDownPins < 10);
    }
}