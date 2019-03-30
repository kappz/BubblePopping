public interface IMultiplayerView
{
    void StartGame();
    void updatePlayerScore(int score);
    void ExitGame();
    void updateOpponentScore(int score);
}