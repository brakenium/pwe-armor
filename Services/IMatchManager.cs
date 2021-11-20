using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using watchtower.Constants;
using watchtower.Models;

namespace watchtower.Services {

    public interface IMatchManager {

        /// <summary>
        ///     Start a match. If the match is already running, nothing happens
        /// </summary>
        void StartRound();

        /// <summary>
        ///     Reset a match, clearing the runners
        /// </summary>
        void ClearMatch();

        /// <summary>
        ///     Stop a round
        /// </summary>
        void StopRound();

        /// <summary>
        ///     Get the current state of the match
        /// </summary>
        MatchState GetState();

        /// <summary>
        ///     Set the settings used in a match
        /// </summary>
        /// <param name="settings">Settings to use in the match</param>
        void SetSettings(MatchSettings settings);

        /// <summary>
        ///     Set the duration of the match in seconds
        /// </summary>
        /// <param name="seconds">How many seconds the match will run for</param>
        void SetMatchLength(int seconds);

        /// <summary>
        ///     Get how long the match will be running for
        /// </summary>
        int GetMatchLength();

        /// <summary>
        ///     Assign a team to a faction
        /// </summary>
        /// <param name="index">Index of the team</param>
        /// <param name="factionID">Faction ID to set</param>
        void AssignTeamFaction(int index, int factionID);

        /// <summary>
        ///     Get the current settings in a match
        /// </summary>
        MatchSettings GetSettings();

        /// <summary>
        ///     Get the team for the index
        /// </summary>
        /// <param name="index">Index of the team to get</param>
        TrackedPlayer? GetTeam(int index);

        /// <summary>
        ///     Set the score of a runner
        /// </summary>
        /// <param name="index">Index of the runner to set the score of</param>
        /// <param name="score">Score to set the runner to</param>
        void SetScore(int index, int score);

        /// <summary>
        ///     Get the score of a runner
        /// </summary>
        /// <param name="index">Index of the runner to get the score of</param>
        int GetScore(int index);

        /// <summary>
        ///     Get the <c>DateTime</c> of when a match was started
        /// </summary>
        DateTime GetMatchStart();

        /// <summary>
        ///     Get the <c>DateTime</c> of when the match ended, or <c>null</c> if it hasn't ended
        /// </summary>
        DateTime? GetMatchEnd();

        /// <summary>
        ///     Get how many seconds a match has been running for. Not really useful if the match has not started
        /// </summary>
        int GetTimeLeft();

    }

}
