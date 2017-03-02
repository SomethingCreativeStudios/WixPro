using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeBureau;

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
        [StringValue("CoinZone")]
        CoinZone,
        [StringValue("PlayerDeck")]
        Deck,
        [StringValue("PlayerHand")]
        Hand,
        [StringValue("Ener_Zone")]
        EnerZone,
        [StringValue("LRIG_Zone")]
        LRIGZone,
        [StringValue("LRIG_Trash")]
        LRIGTrashZone,
        [StringValue("Trash")]
        TrashZone,
        [StringValue("PlayerField")]
        PlayerField,
        [StringValue("Signi_Zone_Left")]
        SIGNI_Left,
        [StringValue("Signi_Zone_Center")]
        SIGNI_Center,
        [StringValue("Signi_Zone_Right")]
        SIGNI_Right,
        [StringValue("LifeCloth_1")]
        LifeCloth1,
        [StringValue("LifeCloth_3")]
        LifeCloth2,
        [StringValue("LifeCloth_3")]
        LifeCloth3,
        [StringValue("LifeCloth_4")]
        LifeCloth4,
        [StringValue("LifeCloth_5")]
        LifeCloth5,
        [StringValue("LifeCloth_6")]
        LifeCloth6,
        [StringValue("LifeCloth_7")]
        LifeCloth7
    }
}
