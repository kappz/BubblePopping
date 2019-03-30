using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Application;
using System;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour, IMenuView {

    private PlayGamesClient menuClient;

    public GameObject signInScreen;

    public GameObject mainMenuScreen;

    public GameObject difficultyScreen;

    public GameObject gameSelectionScreen;

    public GameObject multiplayerModeSelectionScreen;

    public GameObject multiplayerSelectionScreen;

    public GameObject loadingPopUp;

    private enum MultiplayerOption { PVP, GROUP };
    private enum GameDifficulty { EASY, MEDIUM, HARD };
    private enum MultiplayerGameType { BUBBLEPOP, HARPOON, BUG };

    GameDifficulty difficulty;
    MultiplayerOption multiplayerChoice;
    MultiplayerGameType gameType;

    // Use this for initialization
    void Start () {
        initComponents();
        menuClient = PlayGamesClient.GetInstance();
        menuClient.AttachMenuView(this);
    }

    private void initComponents() {
    }

    public void SignInButtonClicked(){
        Debug.Log("Sign In button clicked!");
        menuClient.SignIntoPlayGames();
    }

    public void SignOutButtonClicked() {
        Debug.Log("Sign Out button clicked!");
        menuClient.SignOutOfPlayGames();
    }

    public void SignInSucceeded(string userName) {
        signInScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void SinglePlayerButtonClicked()
    {
        mainMenuScreen.SetActive(false);
        difficultyScreen.SetActive(true);
    }

    public void LeaderboardButtonClicked() {
        menuClient.ShowLeaderboards();
    }

    public void AchievementButtonClicked() {
        menuClient.ShowAchievements();
    }

    public void SignInFailed()
    {
        Debug.Log("Failed to sign user in to Play Games");
    }


    public void EasyModeClicked() {
        difficulty = GameDifficulty.EASY;
        ShowGameSelection();
    } 

    public void MediumModeClicked() {
        difficulty = GameDifficulty.MEDIUM;
        ShowGameSelection();
    }

    public void HardModeClicked() {
        difficulty = GameDifficulty.HARD;
        ShowGameSelection();
    }

    public void ShowMultiplayerSelection() {
        mainMenuScreen.SetActive(false);
        multiplayerSelectionScreen.SetActive(true);
    }

    private void ShowGameSelection() {
        difficultyScreen.SetActive(false);
        gameSelectionScreen.SetActive(true);
    }


    public void SignOutSucceeded()
    {
        throw new NotImplementedException();
    }

    public void PvpButtonSelected() {
        multiplayerChoice = MultiplayerOption.PVP;
        multiplayerSelectionScreen.SetActive(false);
        multiplayerModeSelectionScreen.SetActive(true);
    }

    public void GroupButtonSelected()
    {
        multiplayerChoice = MultiplayerOption.GROUP;
        multiplayerSelectionScreen.SetActive(false);
        multiplayerModeSelectionScreen.SetActive(true);
    }

    public void BubblePopModeSelected() {
        switch (difficulty)
        {
            case GameDifficulty.EASY:
                SceneManager.LoadScene("Game_BubblePopEasy", LoadSceneMode.Single);
                break;
            case GameDifficulty.MEDIUM:
                SceneManager.LoadScene("Game_BubblePopMedium", LoadSceneMode.Single);
                break;
            case GameDifficulty.HARD:
                SceneManager.LoadScene("Game_BubblePopHard", LoadSceneMode.Single);
                break;
        }
    }

    public void HarpoonModeSelected()
    {
        switch (difficulty)
        {
            case GameDifficulty.EASY:
                SceneManager.LoadScene("HarpoonGame_EASY", LoadSceneMode.Single);
                break;
            case GameDifficulty.MEDIUM:
                SceneManager.LoadScene("HarpoonGame_NORMAL", LoadSceneMode.Single);
                break;
            case GameDifficulty.HARD:
                SceneManager.LoadScene("HarpoonGame_HARD", LoadSceneMode.Single);
                break;
        }
    }

    public void BugZapModeSelected() {
        switch (difficulty)
        {
            case GameDifficulty.EASY:
                SceneManager.LoadScene("BugZapEasy", LoadSceneMode.Single);
                break;
            case GameDifficulty.MEDIUM:
                SceneManager.LoadScene("BugZapMedium", LoadSceneMode.Single);
                break;
            case GameDifficulty.HARD:
                SceneManager.LoadScene("BugZapHard", LoadSceneMode.Single);
                break;
        }
    }

    public void BubblePopMultiplayerSelected() {
        loadingPopUp.SetActive(true);
        gameType = MultiplayerGameType.BUBBLEPOP;
        if (multiplayerChoice == MultiplayerOption.PVP) {
            menuClient.StartMultiplayerPvP();
        }
        if (multiplayerChoice == MultiplayerOption.GROUP){
            menuClient.StartMultiplayerGroup();
        }
    }

    public void HarpoonMultiplayerSelected()
    {
        loadingPopUp.SetActive(true);
        gameType = MultiplayerGameType.HARPOON;
        if (multiplayerChoice == MultiplayerOption.PVP)
        {
            menuClient.StartMultiplayerPvP();
        }
        if (multiplayerChoice == MultiplayerOption.GROUP)
        {
            menuClient.StartMultiplayerGroup();
        }
    }

    public void BugZapMultiplayerSelected()
    {
        loadingPopUp.SetActive(true);
        gameType = MultiplayerGameType.BUG;
        if (multiplayerChoice == MultiplayerOption.PVP)
        {
            menuClient.StartMultiplayerPvP();
        }
        if (multiplayerChoice == MultiplayerOption.GROUP)
        {
            menuClient.StartMultiplayerGroup();
        }
    }


    public bool ShowInvitation()
    {
        throw new NotImplementedException();
    }

    public void ShowInvitation(string displayName)
    {
        throw new NotImplementedException();
    }

    public void EnterMultiplayer()
    {
        switch (gameType)
        {
            case MultiplayerGameType.BUBBLEPOP:
                SceneManager.LoadScene("Game_BubblePopMultiplayer", LoadSceneMode.Single);
                break;
            case MultiplayerGameType.HARPOON:
                SceneManager.LoadScene("HarpoonGame_Multi", LoadSceneMode.Single);
                break;
            case MultiplayerGameType.BUG:
                SceneManager.LoadScene("ZapMultiplayer", LoadSceneMode.Single);
                break;
        }
    }
}
