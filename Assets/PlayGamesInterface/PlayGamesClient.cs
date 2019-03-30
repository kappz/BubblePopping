using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using Application;
using System;
using System.Collections.Generic;

public class PlayGamesClient : MonoBehaviour, RealTimeMultiplayerListener
{

    private Invitation pendingInvitation;

    private IMenuView menuView;

    private IMultiplayerView multiplayerView;

    private ISingleplayerView singleplayerView;

    private MultiplayerGame multiplayerGame;

    private GameType multiplayerGameType;


    // increment defintions for achievements related to each array
    private Dictionary<int, string> bubblePopAchievements = new Dictionary<int, string>
    {
        { 5, GPGSIds.achievement_a_bubbly_beginning },
        { 50, GPGSIds.achievement_bubble_apprentice },
        { 100, GPGSIds.achievement_bubble_captain},
        { 1000, GPGSIds.achievement_bubble_ninja}
    };

    private Dictionary<int, string> bugZapAchievements = new Dictionary<int, string>
    {
        { 5, GPGSIds.achievement_harnessing_the_electric },
        { 50, GPGSIds.achievement_professional_zapper },
        { 100, GPGSIds.achievement_exterminator},
        { 1000, GPGSIds.achievement_zeus}
    };

    private Dictionary<int, string> harpoonAchievements = new Dictionary<int, string>
    {
        { 5, GPGSIds.achievement_cabin_boy },
        { 50, GPGSIds.achievement_boatswain },
        { 100, GPGSIds.achievement_commander},
        { 1000, GPGSIds.achievement_commodore}
    };

    private Dictionary<int, string> gamesPlayedAchievements = new Dictionary<int, string>
    {
        { 5, GPGSIds.achievement_newbie },
        { 50, GPGSIds.achievement_warmed_up },
        { 100, GPGSIds.achievement_expert},
        { 1000, GPGSIds.achievement_master_of_all_trades}
    };

    private static PlayGamesClient _instance;

    // create singleton available to all scenes/instances
    void Awake()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;

        // set this to true for verbose logging
        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .WithInvitationDelegate(OnInviteReceivedCallback)
            .Build();
                                       
        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public static PlayGamesClient GetInstance() { return _instance; }

    // Use this for initialization
    void Start()
    {
        // keeps this object persistent between scenes
        DontDestroyOnLoad(this.gameObject);

    }

    // LOGIN CALLS //

    private void SignIntoPlayGamesSilent()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {

            PlayGamesPlatform.Instance.Authenticate((success) => 
            {
                if (success)
                {
                    menuView.SignInSucceeded(PlayGamesPlatform.Instance.localUser.userName);
                }
            }, true);
        }
        else
        {
            Debug.Log("Already signed in to Google Play Games. No need to " +
                      "call this again");
        }
    }

    internal void UnlockBubbleAchievement(int numBubblesPopped)
    {
        PlayGamesPlatform.Instance.ReportProgress(bubblePopAchievements[numBubblesPopped], 100.0f, (bool success) => {});
    }

    internal void UnlockBugZapAchievement(int numBugsZapped)
    {
        PlayGamesPlatform.Instance.ReportProgress(bugZapAchievements[numBugsZapped], 100.0f, (bool success) => {});
    }

    internal void UnlockHarpoonAchievement(int numBubblesPopped)
    {
        PlayGamesPlatform.Instance.ReportProgress(harpoonAchievements[numBubblesPopped], 100.0f, (bool success) => {});
    }

    internal void UnlockGamesPlayedAchievement(int gamesPlayed)
    {
        PlayGamesPlatform.Instance.ReportProgress(gamesPlayedAchievements[gamesPlayed], 100.0f, (bool success) => {});
    }

    internal void UpdateBubblePopLeaderboard(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_high_score__bubble_pop, (bool success) => {});
    }

    internal void UpdateBugZapLeaderboard(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_high_score__bug_zap, (bool success) => { });
    }

    internal void UpdateHarpoonLeaderboard(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_high_score__harpoon, (bool success) => { });
    }

    /// <summary>
    /// will show PlayGames sign in intent / activity
    // if user has not already authenticated with the game yet.
    // * will call IMenuView.SignInSucceeded() or IMenuView.SignInFailed() when complete
    /// </summary>
    public void SignIntoPlayGames()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate((success) => 
            {
                if (success)
                {
                    // move to authorized state
                    menuView.SignInSucceeded(PlayGamesPlatform.Instance.localUser.userName);
                }
                else
                {
                    // display a failed prompt
                    menuView.SignInFailed();
                }
            }, false);
        }
        else
        {
            Debug.Log("Already signed in to Google Play Games. No need to " +
                      "call this again");
        }
    }

    /// <summary>
    /// Signs the user out of play games.
    /// Upon calling this, if sign out succeeds, IMenuView.SignOutSucceeded(). will be called.
    /// Sign out will not succeed silently if you try to sign out someone who is already
    /// signed out.
    /// </summary>
    public void SignOutOfPlayGames()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            menuView.SignOutSucceeded();
        }
        else
        {
            Debug.Log("Already signed out of Google Play Games. No need to " +
                      "call this again");
        }
    }

    /// <summary>
    /// Subscribes the multiplayer scene to PlayGames events.
    /// Immediately shows the PlayGames lobby UI as a layer over
    /// the multiplayer view.
    /// This function must be called in your Start()!
    /// </summary>
    /// <param name="multiplayerView">the script that implements multiplayer events.</param>
    public void AttachMultiplayerView(IMultiplayerView multiplayerView)
    {

        // let the PlayGamesMultiplayerClient know that the scene has been loaded (set multiplayer view), (showMultiplayerUI)
        // so it can show the UI , 
        // pass in the multiplayer view to the PlayGamesMultiplayerClient so it can communicate
        // with the view, but also return the client (will return a reference)
        this.multiplayerView = multiplayerView;

        // show multiplayer UI
        PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
    }

    public void AttachSingleplayerView(ISingleplayerView singleplayerView)
    {

        // let the PlayGamesMultiplayerClient know that the scene has been loaded (set singleplayer view), 
        // pass in the multiplayer view to the PlayGamesMultiplayerClient so it can communicate
        // with the view, but also return the client (will return a reference)
        this.singleplayerView = singleplayerView;
    }

    /// <summary>
    /// Subscribes the menu to PlayGames events.
    /// Will try to silently sign in the user to play games 
    /// (will succeed if the user has already signed in on this device).
    /// Will call IMenuView.SignInSucceeded() when done, or fail without notifying.
    /// This function must be called in your Start()!
    /// </summary>
    /// <param name="menuView">the script that implements menu events.</param>
    public void AttachMenuView(IMenuView menuView)
    {
        this.menuView = menuView;

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            SignIntoPlayGamesSilent();
        } else {
            menuView.SignInSucceeded(PlayGamesPlatform.Instance.localUser.userName);
        }
    }

    // called when an invitation is received:
    private void OnInviteReceivedCallback(Invitation invitation, bool shouldAutoAccept)
    {
        pendingInvitation = invitation;
    }

    // DISPLAY STATISTICS //

   /// <summary>
   /// If the user is signed in, a menu will be shown
    /// to navigate the created leaderboards. The user will exit
    /// the menu on their own and return to the calling screen.
   /// </summary>
    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Must sign in to Google Play Games first to show Leaderboards");
        }

    }

    /// <summary>
    /// If the user is signed in, a menu will be shown
    /// to navigate the possible achievements in-game. The user
    /// will exit the menu on their own and return to the calling screen.
    /// </summary>
    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("Must sign in to Google Play Games first to show Achievements");
        }
    }


    // MULTIPLAYER CALLS // 

    /// <summary>
    /// Uses the GooglePlayGames Inbox UI and allows a user to select
    /// a pending invite from a list of invites (not just the most recent one). 
    /// </summary>
    public void ShowInvitations()
    {
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
    }

    /// <summary>
    /// Checks if there are available invitations. If there is an invite,
    /// IMenuView.ShowInvitation() will be called.
    /// this function should be called in OnGUI() in the menuView so that it 
    /// continuously checks for invitations as they come in.
    /// </summary>
    public void CheckInvitations()
    {
        if (pendingInvitation != null)
        {
            // await Invitation accepted / declined calls
            menuView.ShowInvitation(pendingInvitation.Inviter.DisplayName);
        }
    }

    /// <summary>
    /// Accepts the pending invitation. After GooglePlayGames registers
    /// the invitation as accepted, IMenuView.EnterMultiplayer() will
    /// be called to start the desired Multiplayer Scene.
    /// </summary>
    public void InvitationAccepted()
    {
        if (pendingInvitation != null)
        {
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(pendingInvitation.InvitationId,this);
        }
        else
        {
            Debug.Log("InvitationAccepted() called but there is no invitation to accept.");
        }
    }

    /// <summary>
    /// Declines the pending invitation silently.
    /// </summary>
    public void InvitationDeclined()
    {
        if (pendingInvitation != null)
        {
            pendingInvitation = null;
            PlayGamesPlatform.Instance.RealTime.DeclineInvitation(pendingInvitation.InvitationId);
        }
        else
        {
            Debug.Log("InvitationDeclined() called but there is no invitation to decline.");
        }
    }

    /// <summary>
    /// Starts a multiplayer PvP lobby/search and saves choice.
    /// </summary>
    public void StartMultiplayerPvP(){
        StartMultiplayer(1);
        multiplayerGameType = GameType.PvP;
    }

    /// <summary>
    /// Starts a group multiplayer lobby/search and saves choice.
    /// </summary>
    public void StartMultiplayerGroup() {
        StartMultiplayer(3);
        multiplayerGameType = GameType.GROUP;
    }

    private void StartMultiplayer(uint opponents) {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(opponents, opponents,
                0, this);
    }

    /// <summary>
    /// Sends message to a given participant in the lobby.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="participantId">Participant identifier.</param>
    private void SendRealTimeMessage(string message, string participantId) {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        PlayGamesPlatform.Instance.RealTime.SendMessage(true, participantId, buffer);
    }

    /// <summary>
    /// Sends a message to all participants in the lobby.
    /// </summary>
    /// <param name="message">Message.</param>
    private void BroadcastRealTimeMessage(string message) {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, buffer);
    }

    /// <summary>
    /// Event that is linked to an increase in the lobby creation progress.
    /// </summary>
    /// <param name="percent">Percent.</param>
    public void OnRoomSetupProgress(float percent)
    {
        if (menuView != null){
            // menu view should load appropriate game mode.
            // when game mode is attached, will show the lobby UI.
            menuView.EnterMultiplayer();
            menuView = null;
        }

        Debug.Log("Room setup progress: " + percent + "%");
    }

    /// <summary>
    /// Event that is called once the user is connected to the room.
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void OnRoomConnected(bool success)
    {
      
        if (success) {
            // if lobby host
            if (PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId ==
            GetLobbyHost(PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()))
            {
                // create shared multiplayer game object
                multiplayerGame = new MultiplayerGame(PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants(), multiplayerGameType);
                // tell participants to start game
                BroadcastRealTimeMessage(createGameStartString());
                multiplayerView.StartGame();
            }
        } 

    }

    /// <summary>
    /// Returns the lobby host.
    /// </summary>
    /// <returns>The lobby host.</returns>
    /// <param name="list">List.</param>
    private string GetLobbyHost(List<Participant> list)
    {
        return list[0].ParticipantId;
    }

    /// <summary>
    /// Called from a mulitplayer view to update the score with the rest of the clients.
    /// </summary>
    /// <param name="score">Score.</param>
    public void OnScoreUpdate(int score) {

        if (PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId ==
           GetLobbyHost(PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()))
        {
            multiplayerGame.setScore(PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId, score);
            multiplayerView.updatePlayerScore(multiplayerGame.getPlayerScore());

            // broadcast to rest of the group
            // for teammate, send player, opponent
            foreach (Participant p in multiplayerGame.getHostTeam())
            {
                if (p.ParticipantId != PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId)
                {
                    SendRealTimeMessage(createScoreStringHost(multiplayerGame.getPlayerScore(), multiplayerGame.getOpponentScore()), p.ParticipantId);
                }
            }

            // for opponent, swap player, opponent
            foreach (Participant p in multiplayerGame.getOpponentTeam())
            {
                SendRealTimeMessage(createScoreStringHost(multiplayerGame.getOpponentScore(), multiplayerGame.getPlayerScore()), p.ParticipantId);
            }

        } else {
            SendRealTimeMessage(createScoreStringClient(score), GetLobbyHost(PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()));
        }

    }

    public void OnMultiplayerGameEnd() {
        // implemented but not useful in current feature.
    }


    // this should be called when the user wants to exit the game -- ALWAYS!
    public void LeaveMultiplayerRoom() {
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }

    /// <summary>
    /// returns the games result from the host
    /// </summary>
    /// <param name="gameResult">Game result.</param>
    private void GameResultRecieved(string[] gameResult) {
        string winner = GetGameResultFromMessage(gameResult);
        
    }

    /// <summary>
    /// Once a game start is sent to clients, the clients will start their game via 
    /// this function.
    /// </summary>
    private void GameStartRecieved() {
        multiplayerView.StartGame();
    }

    /// <summary>
    /// When a score update is recieved from a paricipant,
    /// respond accordingly either updating your view or letting the host know.
    /// </summary>
    /// <param name="scores">Scores.</param>
    /// <param name="participantId">Participant identifier.</param>
    private void ScoreUpdateRecieved(string[] scores, string participantId) {

        // if you are lobby host
        if (PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId ==
           GetLobbyHost(PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()))
        {
            // sets score for the participantId team
            multiplayerGame.setScore(participantId, GetClientScoreFromMessage(scores));
            // updates on screen score
            multiplayerView.updatePlayerScore(multiplayerGame.getPlayerScore());
            multiplayerView.updateOpponentScore(multiplayerGame.getOpponentScore());

            // broadcast to rest of the group !!! 
            // for teammate, send player, opponent
            foreach (Participant p in multiplayerGame.getHostTeam()) {
                if (p.ParticipantId != PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId) {
                    SendRealTimeMessage(createScoreStringHost(multiplayerGame.getPlayerScore(), multiplayerGame.getOpponentScore()), p.ParticipantId);
                }
            }

            // for opponent, swap player, opponent
            foreach (Participant p in multiplayerGame.getOpponentTeam())
            {
                SendRealTimeMessage(createScoreStringHost(multiplayerGame.getOpponentScore(), multiplayerGame.getPlayerScore()), p.ParticipantId);
            }

        }
        else
        {
            // recieving message from host, just update the score on screen.
            multiplayerView.updatePlayerScore(GetPlayerScoreFromMessage(scores));
            multiplayerView.updateOpponentScore(GetOpponentScoreFromMessage(scores));
        }
    }

    public void OnLeftRoom()
    {
        multiplayerView.ExitGame();
    }

    public void OnParticipantLeft(Participant participant)
    {
        PlayGamesPlatform.Instance.RealTime.LeaveRoom();
    }

    public void OnPeersConnected(string[] participantIds)
    {

    }

    public void OnPeersDisconnected(string[] participantIds)
    {

    }

    /// <summary>
    /// deciphers what type of message it is and who it should call
    /// </summary>
    /// <param name="isReliable">If set to <c>true</c> is reliable.</param>
    /// <param name="senderId">Sender identifier.</param>
    /// <param name="data">Data.</param>
    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {

        // From byte array to string
        string message = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
        string[] messageArray = message.Split(null);

        if (messageArray[1] == "SCORE") {
            ScoreUpdateRecieved(messageArray, senderId);
        } else if (messageArray[1] == "START") {
            GameStartRecieved();
        } else if (messageArray[1] == "RESULT") {
            GameResultRecieved(messageArray);
        }
    }

    // MSG PROTOCOL DEFINITIONS

    private string createScoreStringHost(int playerScore, int opponentScore) {
        return "HOSTMSG SCORE PLAYER " + playerScore + " OPPONENT " + opponentScore;
    }

    private string createGameStartString() {
        return "HOSTMSG START";
    }

    private string createGameEndString(string winner) {
        return "HOSTMSG RESULT "+ winner;
    }

    private string createScoreStringClient(int score)
    {
        return "CLIENTMSG SCORE " + score;
    }

    private int GetOpponentScoreFromMessage(string[] scores)
    {
        return Int32.Parse(scores[5]);
    }

    private int GetPlayerScoreFromMessage(string[] scores)
    {
        return Int32.Parse(scores[3]);
    }

    private int GetClientScoreFromMessage(string[] scores)
    {
        return Int32.Parse(scores[2]);
    }

    private string GetGameResultFromMessage(string[] gameResult)
    {
        return gameResult[2];
    }

}
