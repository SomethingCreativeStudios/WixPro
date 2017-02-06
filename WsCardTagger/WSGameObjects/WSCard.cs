using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
public enum TriggerType
    {
        Door,
        Book,
        Gate,
        Shot,
        Wind,
        Bag,
        Treasure,
        TwoSoul,
        SoulDoor,
        SoulBook,
        SoulGate,
        SoulShot,
        SoulWind,
        SoulBag,
        SoulTreasure,
        Soul,
        None
    }

[Serializable]
public class WSCard
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private string set;

    public string Set
    {
        get { return set; }
        set { set = value; }
    }
    private string imageFile;

    public string ImageFile
    {
        get { return imageFile; }
        set { imageFile = value; }
    }
    private string type;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    private string color;

    public string Color
    {
        get { return color; }
        set { color = value; }
    }
    private string rarity;

    public string Rarity
    {
        get { return rarity; }
        set { rarity = value; }
    }
    private string serial;

    public string Serial
    {
        get { return serial; }
        set { serial = value; }
    }
    private string cardId;

    public string CardId
    {
        get { return imageFile; }
    }
    private string level;

    public string Level
    {
        get { return level; }
        set { level = value; }
    }
    private string cost;

    public string Cost
    {
        get { return cost; }
        set { cost = value; }
    }
    private string power;

    public string Power
    {
        get { return power; }
        set { power = value; }
    }

    private string powerBoost;

    public string PowerBoost
    {
        get { return powerBoost; }
        set { powerBoost = value; }
    }

    public int TruePower
    {
        get
        { 
            return Convert.ToInt32(power) + Convert.ToInt32(powerBoost);
        }
    }
    private string trigger;

    public string Trigger
    {
        get { return trigger; }
        set { trigger = value; }
    }
    private string[] traits;

    public string[] Traits
    {
        get { return traits; }
        set { traits = value; }
    }
    private string flavour;

    public string Flavour
    {
        get { return flavour; }
        set { flavour = value; }
    }
    private string ability1;

    public string Ability1
    {
        get { return ability1; }
        set { ability1 = value; }
    }
    private string ability2;

    public string Ability2
    {
        get { return ability2; }
        set { ability2 = value; }
    }
    private string ability3;

    public string Ability3
    {
        get { return ability3; }
        set { ability3 = value; }
    }

    private string soulBoost;

    public string SoulBoost
    {
        get { return soulBoost; }
        set { soulBoost = value; }
    }

    private string soul;

    public string Soul
    {
        get { return soul; }
        set { soul = value; }
    }
    
    public int TrueSoul
    {
        get
        {
            return Convert.ToInt32(soul) + Convert.ToInt32(soulBoost);
        }
    }

    public string TotalEffect
    {
        get { return ability1 + ability2 + ability3; }
    }

    public bool FaceUp { get; set; }

    private List<String> effectTags = new List<string>();

    public List<String> EffectTags
    {
        get { return effectTags; }
        set { effectTags = value; }
    }

    public WSCard()
    {
        FaceUp = true;
    }

    public WSCard(string[] cardContents)
    {
        if (cardContents.Length < 17)
            return;

        name = cardContents[0];
        set = cardContents[1];
        imageFile = cardContents[2];
        type = cardContents[3];
        color = cardContents[4];
        rarity = cardContents[5];
        serial = cardContents[6];

        if (type == "CX")
            level = "0";
        else
            level = cardContents[7];

        cost = cardContents[8];
        power = cardContents[9];
        soul = cardContents[10];
        trigger = cardContents[11];
        traits = cardContents[12].Split(new char[] { ',' });
        flavour = cardContents[13];
        ability1 = cardContents[14];
        ability2 = cardContents[15];
        ability3 = cardContents[16];

        powerBoost = "0";
        soulBoost = "0";
        FaceUp = true;
    }

    /// <summary>
    /// Returns the name of card, if no name found then "NO NAME"
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (name != null)
            return name;
        else
            return "NO NAME";
    }

    /// <summary>
    /// Checks to see if card is equal, based on serial
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        WSCard cardToCompare = (WSCard)obj;
        return (this.serial == cardToCompare.serial);
    }

    public override int GetHashCode()
    {
        System.Random ra = new System.Random();
        return base.GetHashCode() + ra.Next();
    }

    public static string GetImagePath(WSCard card)
    {
        return CardCollection.setImages + card.Set + "\\" + card.ImageFile + ".jpg";
    }

    /// <summary>
    /// This will correctly check to see if card is null
    /// </summary>
    /// <param name="card">Card being Checked</param>
    /// <returns>is this card null or not</returns>
    public static bool isCardNull(WSCard card)
    {
        if (card == null)
            return true;

        if (card.name == null)
            return true;

        return false;
    }

    public static String typeToFullType(string type)
    {
        switch (type)
        {
            case "CH":
                return "Character";
            case "CX":
                return "Climax";
            case "EV":
                return "Event";
            default:
                return "NEVER GONA SEE THIS";
        }
    }

    public static String fullTypeToType(string fullType)
    {
        switch (fullType)
        {
            case "Character":
                return "CH";
            case "Climax":
                return "CX";
            case "Event":
                return "EV";
            default:
                return "NEVER GONA SEE THIS";
        }

    }

    public static String CardToStringFormat(WSCard card)
    {
        string cardTraits = "";
        for (int i = 0; i < card.traits.Length; i++)
        {
            if (i != 0)
                cardTraits += ", " + card.traits[i];
            else
                cardTraits += card.traits[i];
        }
        return card.name + "\t" + card.set + "\t" + card.imageFile + "\t" + card.type + "\t" + card.color + "\t" + card.rarity + "\t" + card.serial + "\t" + card.level + "\t" + card.cost + "\t" + card.power + "\t" + card.soul + "\t" + card.trigger + "\t" + cardTraits + card.flavour + "\t" + card.ability1 + "\t" + card.ability2 + "\t" + card.ability3;
    }

    public static TriggerType stringToTriggerType(String trigger)
    {
        switch (trigger)
        {
            case "1":
                return TriggerType.Soul;
            case "2":
                return TriggerType.TwoSoul;
            case "W":
                return TriggerType.Wind;
            case "S":
                return TriggerType.Bag;
            case "D":
                return TriggerType.Door;
            case "B":
                return TriggerType.Book;
            case "F":
                return TriggerType.Shot;
            case "1W":
                return TriggerType.SoulWind;
            case "1S":
                return TriggerType.SoulBag;
            case "1D":
                return TriggerType.SoulDoor;
            case "1B":
                return TriggerType.SoulBook;
            case "1F":
                return TriggerType.SoulShot;
            default:
                return TriggerType.Soul;
        }
    }

    /// <summary>
    /// Makes a deep copy of the wscard
    /// </summary>
    /// <param name="obj">card you want to clone</param>
    /// <returns>newly deep copied card</returns>
    public static WSCard Clone(WSCard obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (WSCard)formatter.Deserialize(ms);
        }
    }
}
