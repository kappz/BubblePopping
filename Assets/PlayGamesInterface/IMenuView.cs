using System;
namespace Application
{
    /*
     * this interface provides a way for a session management object
     * to interact with a menu UI, abstracting the details of the session
     * management object away from the menu and allowing for an event driven
     * pattern. 
    */
    public interface IMenuView
    {
        // takes necessary actions regarding a successful sign in
        // passes in the signed in user's username for identification
        void SignInSucceeded(String userName);

        // takes necessary actions regarding a failed sign in
        void SignInFailed();

        // takes necessary action regarding a successful sign out
        void SignOutSucceeded();

        // shows pending invitations
        void ShowInvitation(string displayName);

        // takes necessary actions to start multiplayer
        void EnterMultiplayer();
    }
}
