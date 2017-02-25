using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wix_Studio
{
    public class WixossCard
    {
        public string CardName { get; set; }
        public List<CardColor> Color { get; set; }
        public CardType Type { get; set; }
        public List<CardCost> Cost { get; set; }
        public string LimitingCondition { get; set; }
        public List<CardTiming> Timing { get; set; }
        public int Level { get; set; }
        public int Limit { get; set; }
        public int Power { get; set; }
        public List<String> Class { get; set; }
        public Boolean Guard { get; set; }
        public Boolean MultiEner { get; set; }
        public Boolean LifeBurst { get; set; }
        public String CardUrl { get; set; }
        public String ImageUrl { get; set; }

        [XmlIgnore]
        public String CardEffect { get; set; }
        [XmlElement("CardEffect")]
        public System.Xml.XmlCDataSection CardEffectCData
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(CardEffect);
            }
            set
            {
                CardEffect = value.Value;
            }
        }
        public String CardSet { get; set; }
        public String CardNumberInSet { get; set; }

        public String CardImagePath { get { return CardCollection.setImages + CardSet + "\\" + CardNumberInSet + ".jpg"; } }

        public override bool Equals(object obj)
        {
            if ((obj.GetType() != typeof(WixossCard)))
                return false;

            WixossCard tempCard = (WixossCard)obj;
            return (CardSet + "/" + CardNumberInSet).Equals(tempCard.CardSet + "/" + tempCard.CardNumberInSet);
        }

        public override int GetHashCode()
        {
            return ( CardSet + "/" + CardNumberInSet ).GetHashCode();
        }
        public String CostStr
        {
            get
            {
                String cardCostStr = "";

                foreach ( var cardCost in Cost )
                {
                    cardCostStr += "{"+cardCost.color + ": " + cardCost.numberPerColor + "} ";
                }

                if ( cardCostStr == "" )
                {
                    cardCostStr = "No Cost";
                }

                return cardCostStr;
            }

        }

        public String ColorStr
        {
            get
            {
                String cardColorStr = "";

                foreach ( var cardColor in Color )
                {
                    cardColorStr += "{" + cardColor + "} ";
                }

                if ( cardColorStr == "" )
                {
                    cardColorStr = "No Color";
                }

                return cardColorStr;
            }

        }

        public String TimingStr
        {
            get
            {
                String cardCostTimingStr = "";
               
                foreach ( var cardTiming in Timing )
                {
                    cardCostTimingStr += "{" + cardTiming + "} ";
                }
                if(cardCostTimingStr == "" )
                {
                    cardCostTimingStr = "No Timing";
                }
                return cardCostTimingStr;
            }

        }

        public WixossCard()
        {
            Color = new List<CardColor>();
            Cost = new List<CardCost>();
            Timing = new List<CardTiming>();
            Class = new List<string>();
        }
    }

    public enum CardColor
    {
        Green,
        Black,
        Red,
        Blue,
        White,
        Colorless,
        NoColor
    }

    public enum CardType
    {
        ARTS,
        LRIG,
        SIGNI,
        Resona,
        Spell,
        NoType
    }

    public class CardCost
    {
        public CardColor color { get; set; }
        public int numberPerColor { get; set; }
    }

    public enum CardTiming
    {
        MainPhase,
        AttackPhase,
        SpellCutIn,
        NoTiming
    }
}
