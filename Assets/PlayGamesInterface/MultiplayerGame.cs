using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
namespace Application
{

    public class MultiplayerGame
    {
        public GameState game;

        public GameType type;

        List<Participant> participants;

        List<Participant> host;

        List<Participant> opponent;

        int hostScore;

        int opponentScore;

        public MultiplayerGame(List<Participant> participants, GameType type) {
            this.type = type;
            this.participants = participants;

            if (type == GameType.GROUP) {
                host = participants.GetRange(0, 2);
                opponent = participants.GetRange(2, 2);
            } else if (type == GameType.PvP) {
                host = participants.GetRange(0, 1);
                opponent = participants.GetRange(1, 1);
            }

            hostScore = 0;

            opponentScore = 0;
        }




        internal int getPlayerScore()
        {
            return hostScore;
        }

        internal List<Participant> getHostTeam()
        {
            return host;
        }

        internal int getOpponentScore()
        {
            return opponentScore;
        }

        internal List<Participant> getOpponentTeam()
        {
            return opponent;
        }

        internal void setScore(string participantId, int score)
        {
            foreach (Participant p in host) {
                if (participantId == p.ParticipantId) {
                    hostScore += score;
                }
            }

            foreach (Participant p in opponent)
            {
                if (participantId == p.ParticipantId)
                {
                    opponentScore += score;
                }
            }

        }
    }
}
