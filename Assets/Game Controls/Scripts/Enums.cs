using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game_Controls.Scripts.Enums
{
    public enum CardState
    {
        Standing,
        Rest,
        Reversed,
        NoState
    }
    public enum AttackType
    {
        NoAttack,
        Frontal,
        Attacks,
        Battle
    }

    public enum Location
    {
        NoLocation,
        Stock,
        PlayerDeck,
        PlayerHand,
        WaitingRoom,
        Memory,
        Level,
        Clock,
        Event,
        Climax,
        Stage,
        FrontStageLeft,
        FrontStageCenter,
        FrontStageRight,
        BackStageLeft,
        BackStageRight
    }
}
