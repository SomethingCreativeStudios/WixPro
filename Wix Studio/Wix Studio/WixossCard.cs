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
        Colorless
    }

    public enum CardType
    {
        ARTS,
        LRIG,
        SIGNI,
        Resona,
        Spell
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
        SpellCutIn
    }
}
