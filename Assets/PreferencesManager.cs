using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager
{

    private const string GAMES_PLAYED_KEY = "games_played";

    private const string BUBBLES_POPPED_KEY = "bubbles_popped";

    private const string BUBBLE_POP_HIGH_SCORE_KEY = "bubble_high_score";

    private const string HARPOON_BUBBLES_POPPED_KEY = "harpoons_landed";

    private const string HARPOON_HIGH_SCORE_KEY = "harpoon_high_score";

    private const string BUG_ZAP_HIGH_SCORE_KEY = "bugzap_high_score";

    private const string BUGS_ZAPPED_KEY = "bugs_zapped";

    private int[] bubbleAchievementArray = new int[] { 5, 50, 100, 1000 };

    private int[] bugsAchievementArray = new int[] { 5, 50, 100, 1000 };

    private int[] harpoonAchievementArray = new int[] { 5, 50, 100, 1000 };

    private int[] gamesPlayedArray = new int[] { 5, 50, 100, 1000 };



    public int GetGamesPlayed()
    {
        return PlayerPrefs.GetInt(GAMES_PLAYED_KEY, 0);
    }

    public int GetBubblesPopped()
    {
        return PlayerPrefs.GetInt(BUBBLES_POPPED_KEY, 0);
    }

    public int GetBubblePopHighScore()
    {
        return PlayerPrefs.GetInt(BUBBLE_POP_HIGH_SCORE_KEY, 0);
    }

    public int GetBugsZapped()
    {
        return PlayerPrefs.GetInt(BUGS_ZAPPED_KEY, 0);
    }

    public int GetBugZapHighScore()
    {
        return PlayerPrefs.GetInt(BUG_ZAP_HIGH_SCORE_KEY, 0);
    }

    public int GetHarpoonsLanded()
    {
        return PlayerPrefs.GetInt(HARPOON_BUBBLES_POPPED_KEY, 0);
    }

    public int GetHarpoonHighScore()
    {
        return PlayerPrefs.GetInt(HARPOON_HIGH_SCORE_KEY, 0);
    }


    public void UpdateBubblesPopped()
    {
        if (GetBubblesPopped() == 0)
        {
            PlayerPrefs.SetInt(BUBBLES_POPPED_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(BUBBLES_POPPED_KEY, GetBubblesPopped() + 1);
            if (ReachedBubbleAchievement(GetBubblesPopped()))
            {
                UnlockBubbleAchievement(GetBubblesPopped());
            }
        }
    }

    public void UpdateBubblePopHighScore(int score) {
        if (score > GetBubblePopHighScore()) {
            PlayerPrefs.SetInt(BUBBLE_POP_HIGH_SCORE_KEY, score);
            updateBubblePopLeaderboard(score);
        }
    }

    public void UpdateBugsZapped()
    {
        if (GetBugsZapped() == 0)
        {
            PlayerPrefs.SetInt(BUGS_ZAPPED_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(BUGS_ZAPPED_KEY, GetBugsZapped() + 1);
            if (ReachedBugZapAchievement(GetBugsZapped()))
            {
                UnlockBugsZappedAchievement(GetBubblesPopped());
            }
        }
    }

    public void UpdateBugZapHighScore(int score)
    {
        if (score > GetBugZapHighScore())
        {
            PlayerPrefs.SetInt(BUG_ZAP_HIGH_SCORE_KEY, score);
            updateBugZapLeaderboard(score);
        }
    }

    public void UpdateHarpoonPops()
    {
        if (GetHarpoonsLanded() == 0)
        {
            PlayerPrefs.SetInt(HARPOON_BUBBLES_POPPED_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(HARPOON_BUBBLES_POPPED_KEY, GetBugsZapped() + 1);
            if (ReachedHarpoonAchievement(GetHarpoonsLanded()))
            {
                UnlockHarpoonAchievement(GetHarpoonsLanded());
            }
        }
    }

    public void UpdateHarpoonHighScore(int score)
    {
        if (score > GetHarpoonHighScore())
        {
            PlayerPrefs.SetInt(HARPOON_HIGH_SCORE_KEY, score);
            updateHarpoonLeaderboard(score);
        }
    }

    public void UpdateGamesPlayed()
    {
        if (GetGamesPlayed() == 0)
        {
            PlayerPrefs.SetInt(GAMES_PLAYED_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(GAMES_PLAYED_KEY, GetGamesPlayed() + 1);
            if (ReachedGamesPlayedAchievement(GetGamesPlayed()))
            {
                UnlockGamesPlayedAchievement(GetGamesPlayed());
            }
        }
    }

    private bool ReachedBubbleAchievement(int numBubblesPopped)
    {
        foreach (int num in bubbleAchievementArray)
        {
            if (num == numBubblesPopped)
            {
                return true;
            }
        }
        return false;
    }

    private bool ReachedBugZapAchievement(int numBugsZapped)
    {
        foreach (int num in bugsAchievementArray)
        {
            if (num == numBugsZapped)
            {
                return true;
            }
        }
        return false;
    }

    private bool ReachedHarpoonAchievement(int numHarpoonsLanded)
    {
        foreach (int num in harpoonAchievementArray)
        {
            if (num == numHarpoonsLanded)
            {
                return true;
            }
        }
        return false;
    }

    private bool ReachedGamesPlayedAchievement(int numGamesPlayed)
    {
        foreach (int num in gamesPlayedArray)
        {
            if (num == numGamesPlayed)
            {
                return true;
            }
        }
        return false;
    }

    private void UnlockBubbleAchievement(int numBubblesPopped)
    {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UnlockBubbleAchievement(numBubblesPopped);
    }

    private void UnlockGamesPlayedAchievement(int numGamesPlayed)
    {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UnlockGamesPlayedAchievement(numGamesPlayed);
    }

    private void UnlockBugsZappedAchievement(int numBugsZapped)
    {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UnlockBugZapAchievement(numBugsZapped);
    }

    private void UnlockHarpoonAchievement(int numHarpoonsLanded)
    {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UnlockBugZapAchievement(numHarpoonsLanded);
    }

    private void updateBubblePopLeaderboard(int score) {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UpdateBubblePopLeaderboard(score);
    }

    private void updateBugZapLeaderboard(int score) {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UpdateBugZapLeaderboard(score);
    }

    private void updateHarpoonLeaderboard(int score) {
        PlayGamesClient playGamesClient;
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.UpdateHarpoonLeaderboard(score);
    }
}
