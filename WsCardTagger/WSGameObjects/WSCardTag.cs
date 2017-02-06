using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsCardTagger.WSGameObjects
{
    public class WSCardTag
    {
        public List<WSEffectTag> cost;
        public string effectType;
        public WSTagTrigger effectTrigger;
        public List<WSEffectTag> effectTags;
    }

    public class WSEffectTag : WSMethodTag
    {
        public WSEffectTag() { }

        public WSEffectTag(WSMethodTag methodTag)
        {
            this.methodName = methodTag.methodName;
            this.methodReturnType = methodTag.methodReturnType;
            this.methodParameters = new List<WSEffectParameter>(methodTag.methodParameters);
        }

        public string varName;
    }

    public class WSMethodTag
    {
        public string methodName;
        public string methodReturnType;
        public List<WSEffectParameter> methodParameters;

        public override string ToString()
        {
            String returnStr =  methodName + "(";

            int count = 0;
            foreach ( var methodParameter in methodParameters )
            {
                if ( count != 0 )
                    returnStr += ", ";

                returnStr += methodParameter.parameterName + " : " + methodParameter.parameterType;

                count++;
            }

            returnStr += ") return " + methodReturnType;
            return returnStr;
        }
    }

    public class WSEffectParameter
    {
        public string parameterName;
        public string parameterType;
        public string parameterValue;

        public override string ToString()
        {
            return parameterName + " : " + parameterType;
        }
    }

    public class WSTagTrigger
    {
        public Location toLocation;
        public Location fromlocation;
        public GamePhase gamePhase;
        public bool onMove;
        public bool cardHasTrigger;
        public AttackType onAttack;
        public List<WSCondition> conditionsToBeMent;
    }

    public class WSCondition
    {
        public int cardInZone;
        public Location zoneToCheck;

        public string characterType;

        public bool mustPayCost;
    }

    public class WSCost
    {
        public int numberOfCards;
        public string typeOfCard;
        public Location fromLocation;
        public Location toLocation;
        public CardState changeState;
    }

    public enum CardState
    {
        NoState,
        Standing,
        Rest,
        Reversed
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
    public enum GamePhase
    {
        NoPhase,
        FirstTurn,
        StandPhase,
        DrawPhase,
        ClockPhase,
        MainPhase,
        ClimaxPhase,
        AttackPhase,
        EncorePhase,
        EndPhase
    }
}
