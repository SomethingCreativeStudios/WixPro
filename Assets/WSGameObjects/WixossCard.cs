using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Assets.Game_Controls.Scripts.Enums;

[Serializable]
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
    public Boolean FaceUp { get; set; }

    [XmlIgnore]
    public int SoulBoost { get; set; }

    [XmlIgnore]
    public int PowerBoost { get; set; }

    [XmlIgnore]
    public String CardEffect { get; set; }

    [XmlIgnore]
    public String CardId { get { return CardSet + " " + CardNumberInSet; } }
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

    [XmlIgnore]
    public CardState StateOfCard { get; set; }

    public WixossCard()
    {
        Color = new List<CardColor>();
        Cost = new List<CardCost>();
        Timing = new List<CardTiming>();
        Class = new List<string>();
        FaceUp = true;
    }

    public String getClassStr()
    {
        String classStr = "";
        foreach ( var classItem in Class )
        {
            classStr += classItem + " ";
        }

        return classStr;
    }

    public String getCostStr()
    {
        String listStr = "";
        foreach ( var costItem in Cost )
        {
            listStr += costItem + " ";
        }

        return listStr;
    }

    public String getColorStr()
    {
        String listStr = "";
        foreach ( var colorItem in Color )
        {
            listStr += "{"+ colorItem + "} ";
        }

        return listStr;
    }

    public String getTimingStr()
    {
        String listStr = "";
        foreach ( var timeItem in Timing )
        {
            listStr += "[" + timeItem + "] ";
        }

        return listStr;
    }
    /// <summary>
    /// Checks to see if card is equal, based on imageUrl
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        WixossCard cardToCompare = (WixossCard)obj;
        return ( this.ImageUrl == cardToCompare.ImageUrl );
    }

    public override int GetHashCode()
    {
        System.Random ra = new System.Random();
        return base.GetHashCode() + ra.Next();
    }

    public static string GetImagePath(WixossCard card)
    {
        return CardCollection.setImages + card.CardSet + "\\" + card.CardNumberInSet + ".jpg";
    }

    /// <summary>
    /// This will correctly check to see if card is null
    /// </summary>
    /// <param name="card">Card being Checked</param>
    /// <returns>is this card null or not</returns>
    public static bool isCardNull(WixossCard card)
    {
        if ( card == null )
            return true;

        if ( card.CardName == null )
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
        using ( var ms = new MemoryStream() )
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms , obj);
            ms.Position = 0;

            return (WixossCard)formatter.Deserialize(ms);
        }
    }
}
[Serializable]
public enum CardColor
{
    Green,
    Black,
    Red,
    Blue,
    White,
    Colorless
}

[Serializable]
public enum CardType
{
    ARTS,
    LRIG,
    SIGNI,
    Resona,

    Spell
}
[Serializable]
public class CardCost
{
    public CardColor color { get; set; }
    public int numberPerColor { get; set; }

    public override string ToString()
    {
        return numberPerColor + " {" + color + "}";
    }
}
[Serializable]
public enum CardTiming
{
    MainPhase,
    AttackPhase,
    SpellCutIn
}

