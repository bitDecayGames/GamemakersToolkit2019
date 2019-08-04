public class GameOverStatus
{
    public bool win;
    public GameOverReason reason;

    public GameOverStatus(GameOverReason reason) {
        this.reason = reason;
        win = reason == GameOverReason.SKEWERED_ALL_INGREDIENTS;
    }
}

public enum GameOverReason {
    SKEWERED_ALL_INGREDIENTS,
    DIDNT_SKEWER_ALL_INGREDIENTS,
    SKEWERD_YOURSELF,
    SMOOSHED_INGREDIENTS,
    TOUCHED_INGREDIENT,
    DROPPED_INGREDIENT_IN_WATER,
}