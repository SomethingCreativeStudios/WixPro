using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game_Controls.Scripts
{
    public enum GamePhase
    {
        FirstTurn,
        StandPhase,
        DrawPhase,
        EnerPhase,
        GrowPhase,
        MainPhase,
        AttackPhase,
        ARTSUseStep,
        SigniAttackStep,
        LRIGAttackStep,
        GuardStep,
        EndPhase,
        OppPhase //Opp Turn May Be needed though Unsure
    }
    static class GamePhaseCounter //This is first set in OnLoad
    {
        private static GamePhase currentPhaseLocal;

        /// <summary>
        /// The previous state the game was in
        /// </summary>
        public static GamePhase lastPhase { get; set; }

        /// <summary>
        /// Current phase the game is in.
        /// </summary>
        public static GamePhase currentPhase
        {
            get
            {
                return currentPhaseLocal;
            }
            set
            {
                lastPhase = currentPhase;
                currentPhaseLocal = value;
                PhaseController.ChangePhase(currentPhaseLocal, lastPhase);
            }
        }
    }
}
