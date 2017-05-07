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
        public virtual int Id { get; protected set; }
        public virtual string CardName { get; set; }
        public virtual IList<CardColor> Color { get; set; }
        public virtual CardType Type { get; set; }
        public virtual IList<CardCost> Cost { get; set; }
        public virtual string LimitingCondition { get; set; }
        public virtual IList<CardTiming> Timing { get; set; }
        public virtual int Level { get; set; }
        public virtual int LevelLimit { get; set; }
        public virtual int Power { get; set; }
        public virtual IList<String> Class { get; set; }
        public virtual Boolean Guard { get; set; }
        public virtual Boolean MultiEner { get; set; }
        public virtual Boolean LifeBurst { get; set; }
        public virtual String CardUrl { get; set; }
        public virtual String ImageUrl { get; set; }

        [XmlIgnore]
        public virtual String CardEffect { get; set; }
        [XmlElement("CardEffect")]
        public virtual System.Xml.XmlCDataSection CardEffectCData
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
        public virtual String CardSet { get; set; }
        public virtual String CardNumberInSet { get; set; }

        public virtual String CardImagePath { get { return CardCollection.setImages + CardSet + "\\" + CardNumberInSet + ".jpg"; } }

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
        public virtual String CostStr
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

        public virtual String ColorStr
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

        public virtual String TimingStr
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
        public virtual int Id { get; protected set; }
        public virtual CardColor color { get; set; }
        public virtual int numberPerColor { get; set; }

        public virtual WixossCard wixCard { get; set; }

        public CardCost()
        {

        }

        public CardCost(CardColor color, int numberPerColor, WixossCard wixCard)
        {
            this.color = color;
            this.numberPerColor = numberPerColor;
            this.wixCard = wixCard;
        }
    }

    public enum CardTiming
    {
        MainPhase,
        AttackPhase,
        SpellCutIn,
        NoTiming
    }
}
