using Assets.Game_Controls.Scripts.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
/// <summary>
/// Wixoss Card Object
/// </summary>
public class WixossCard
{
    #region XML Variables
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
    public virtual IList<String> CardSets { get; set; }

    public virtual String CardImagePath { get { return CardCollection.setImages + "\\" + Id + ".jpg"; } }

    #endregion

    #region WixPro Temp Variables

    /// <summary>
    /// The power offset, can be negative
    /// </summary>
    [XmlIgnore]
    public int PowerBoost { get; set; }

    /// <summary>
    /// What position is the card?
    /// </summary>
    [XmlIgnore]
    public CardState StateOfCard { get; set; }

    /// <summary>
    /// Unique id of card. This is the Set + Card Number in Set
    /// </summary>
    [XmlIgnore]
    public String CardId { get { return Convert.ToString(Id); } }

    /// <summary>
    /// Is the card face up?
    /// </summary>
    [XmlIgnore]
    public Boolean FaceUp { get; set; }
    #endregion

    public WixossCard()
    {
        Color = new List<CardColor>();
        Cost = new List<CardCost>();
        Timing = new List<CardTiming>();
        Class = new List<string>();
        CardSets = new List<String>();
    }

    #region String Versions Of Arrays
    public String CostStr
    {
        get
        {
            String cardCostStr = "";

            foreach (var cardCost in Cost)
            {
                cardCostStr += "{" + cardCost.color + ": " + cardCost.numberPerColor + "} ";
            }

            if (cardCostStr == "")
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

            foreach (var cardColor in Color)
            {
                cardColorStr += "{" + cardColor + "} ";
            }

            if (cardColorStr == "")
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

            foreach (var cardTiming in Timing)
            {
                cardCostTimingStr += "{" + cardTiming + "} ";
            }
            if (cardCostTimingStr == "")
            {
                cardCostTimingStr = "No Timing";
            }
            return cardCostTimingStr;
        }

    }

    public String ClassStr
    {
        get
        {
            String cardClassStr = "";

            foreach (var cardClass in Class)
            {
                cardClassStr += "{" + cardClass + "} ";
            }
            if (cardClassStr == "")
            {
                cardClassStr = "No Class";
            }
            return cardClassStr;
        }

    }
    #endregion

    #region Override Methods
    public override bool Equals(object obj)
    {
        if ((obj.GetType() != typeof(WixossCard)))
            return false;

        WixossCard tempCard = (WixossCard)obj;
        return (CardId).Equals(tempCard.CardId);
    }

    public override int GetHashCode()
    {
        return (CardId).GetHashCode();
    }

    #endregion

    #region Helper Static Methods

    /// <summary>
    /// This will correctly check to see if card is null
    /// </summary>
    /// <param name="card">Card being Checked</param>
    /// <returns>is this card null or not</returns>
    public static bool isCardNull(WixossCard card)
    {
        if (card == null)
            return true;

        if (card.CardName == null)
            return true;

        return false;
    }


    /// <summary>
    /// Makes a deep copy of the wixcard
    /// </summary>
    /// <param name="obj">card you want to clone</param>
    /// <returns>newly deep copied card</returns>
    public static WixossCard Clone(WixossCard obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (WixossCard)formatter.Deserialize(ms);
        }
    }

    #endregion

    #region WixossCard Enums
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

        [JsonIgnore]
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

    #endregion
}
