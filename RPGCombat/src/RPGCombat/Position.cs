namespace RPGCombat;

public record Position(int gameSpacialDistance) {
    public int CalculateGameDistanceWith(Position targetPosition) {
        return targetPosition.gameSpacialDistance - gameSpacialDistance;
    }
}