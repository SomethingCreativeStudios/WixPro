using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WsCardTagger.WSGameObjects;

public class CardCollection
{
    //public static string basePath = @"D:\WS PRO\Assets\Card Images\Resources\sets";//Application.dataPath + @"\Card Images\Resources\sets";
    public static string basePath = @"C:\Users\eric-\Documents\WSPro\Card Images\Resources\sets";
    public static string deckBasePath = basePath + "\\decks";
    public static string setImages = basePath + "\\setimages\\";
    public static string methodsFile = basePath + "\\Card Method\\cardMethods.xml";
    public static string methodsOLDFile = basePath + "\\Card Method\\cardMethodsOLD.xml";
    public static Dictionary<String , WSCard> cardCollection;

    public CardCollection()
    {
        cardCollection = new Dictionary<string , WSCard>();
        if ( !File.Exists(methodsFile) )
        {
            Directory.CreateDirectory(basePath + "\\Card Method");
            File.Create(methodsFile).Close();
            SaveEffects(new List<WSEffectTag>());
        }
        LoadAllCards();
    }

    public static void SaveEffects(List<WSEffectTag> effects)
    {
        if ( File.Exists(methodsOLDFile) )
            File.Delete(methodsOLDFile);

        if ( File.Exists(methodsFile) )
        {
            File.Move(methodsFile , methodsOLDFile);
            File.Create(methodsFile).Close();
        }
        XmlSerializer xsSubmit = new XmlSerializer(typeof(List<WSEffectTag>));
        using ( StringWriter sww = new StringWriter() )
        using ( XmlWriter writer = XmlWriter.Create(sww) )
        {
            xsSubmit.Serialize(writer , effects);
            File.WriteAllText(methodsFile , sww.ToString());
        }
    }

    public static List<WSEffectTag> LoadEffects()
    {
        List<WSEffectTag> effects = new List<WSEffectTag>();


        using ( var stream = new StringReader(File.OpenText(methodsFile).ReadToEnd()) )
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<WSEffectTag>));
                effects = new List<WSEffectTag>(serializer.Deserialize(stream) as List<WSEffectTag>);
            }
            catch ( Exception e )
            {

            }
        }
       

        return effects;
    }

    public List<WSCard> GetSet(string setName)
    {
        List<WSCard> cards = new List<WSCard>();

        using ( var stream = new StringReader(File.OpenText(basePath + setName + ".xml").ReadToEnd()) )
        {
            var serializer = new XmlSerializer(typeof(List<WSCard>));
            cards = new List<WSCard>(serializer.Deserialize(stream) as List<WSCard>);
        }

        return cards;
    }

    public List<WSCard> GetSetText(string setName)
    {
        List<WSCard> cards = new List<WSCard>();
        string[] fileContents = File.ReadAllLines(basePath + setName + ".txt");
        for ( int i = 1; i < fileContents.Length; i++ )
        {
            string[] info = fileContents[i].Split(new char[] { '\t' });
            WSCard tempCard = new WSCard(info);
            cards.Add(tempCard);
            if ( !cardCollection.ContainsKey(tempCard.ImageFile) )
                cardCollection.Add(tempCard.ImageFile , tempCard);
        }

        return cards;
    }

    public List<string> GetAllSets()
    {
        List<string> cardSets = new List<string>();
        string[] files = Directory.GetFiles(basePath);
        for ( int i = 1; i < files.Length; i++ )
        {
            if ( files[i].EndsWith(".xml") && !files[i].Contains("nocard.txt") )
                cardSets.Add(files[i].Replace(basePath , "").Replace(".xml" , ""));
        }

        return cardSets;
    }

    public List<string> GetAllSetsTxt()
    {
        List<string> cardSets = new List<string>();
        string[] files = Directory.GetFiles(basePath);
        for ( int i = 1; i < files.Length; i++ )
        {
            if ( files[i].EndsWith(".txt") && !files[i].Contains("nocard.txt") )
                cardSets.Add(files[i].Replace(basePath , "").Replace(".txt" , ""));
        }

        return cardSets;
    }

    public void LoadAllCards()
    {
        List<string> cardSets = GetAllSets();
        List<WSCard> allCards = new List<WSCard>();

        for ( int i = 0; i < cardSets.Count; i++ )
        {
            foreach ( var tempCard in GetSet(cardSets[i]) )
            {
                if ( !cardCollection.ContainsKey(tempCard.ImageFile) )
                    cardCollection.Add(tempCard.ImageFile , tempCard);
            }
        }
    }

    public List<WSCard> loadDeck(string deckName)
    {
        List<WSCard> deckList = new List<WSCard>();
        StreamReader reader = new StreamReader(deckBasePath + "\\" + deckName + ".txt");
        while ( !reader.EndOfStream )
        {
            string[] card = reader.ReadLine().Split(new char[] { '\t' });
            int numOfCard = Convert.ToInt16(card[0]);
            for ( int i = 0; i < numOfCard; i++ )
            {
                WSCard tmpCard = cardForName(card[1]);
                deckList.Add(tmpCard);
                if ( !cardCollection.ContainsKey(tmpCard.ImageFile) )
                    cardCollection.Add(tmpCard.ImageFile , tmpCard);
            }

        }
        reader.Close();
        return deckList;
    }
    private WSCard cardForName(string name)
    {

        for ( int i = 0; i < cardCollection.Keys.Count; i++ )
        {
            if ( cardCollection[cardCollection.Keys.ElementAt(i)].Name == name )
                return cardCollection[cardCollection.Keys.ElementAt(i)];
        }

        return null;
    }

    public void MigrateCards()
    {
        List<String> allSets = GetAllSetsTxt();

        XmlSerializer xsSubmit = new XmlSerializer(typeof(List<WSCard>));
        foreach ( String set in allSets )
        {
            var subReq = GetSetText(set);
            using ( StringWriter sww = new StringWriter() )
            using ( XmlWriter writer = XmlWriter.Create(sww) )
            {
                xsSubmit.Serialize(writer , subReq);
                File.WriteAllText(basePath + set + ".xml" , sww.ToString());
                File.Delete(basePath + set + ".txt");
            }
        }

    }

    public static void EditCard(WSCard oldCard , WSCard newCard)
    {
        List<WSCard> cards = new List<WSCard>();
        CardCollection cardCollection = new CardCollection();
        cards = cardCollection.GetSet(oldCard.Set);
        File.Delete(basePath + oldCard.Set + ".txt");
        StreamWriter writer = File.CreateText(basePath + oldCard.Set + ".txt");
        writer.WriteLine("Name\tSet\tImageFile\tType\tColour\tRarity\tSerial\tLevel\tCost\tPower\tSoul\tTrigger\tTraits\tFlavour\tAbility1\tAbility2\tAbility3");
        foreach ( WSCard card in cards )
        {
            if ( card.Name != newCard.Name )
                writer.WriteLine(WSCard.CardToStringFormat(card));
            else
                writer.WriteLine(WSCard.CardToStringFormat(newCard));
        }
        writer.Close();
    }


    public static List<String> getAllCardNames()
    {
        List<String> cardNames = new List<string>();
        foreach ( var cardKey in cardCollection.Keys )
        {
            String cardName = cardCollection[cardKey].Name;
            if ( !cardNames.Contains(cardName) )
                cardNames.Add(cardName);
        }
        return cardNames;
    }

    public static List<String> getAllCardTraits()
    {
        List<String> cardTraits = new List<string>();

        foreach ( var cardKey in cardCollection.Keys )
        {
            String[] cardTraitsArray = cardCollection[cardKey].Traits;
            foreach ( var cardTrait in cardTraitsArray )
            {
                if ( !cardTraits.Contains(cardTrait) )
                    cardTraits.Add(cardTrait);
            }
        }

        return cardTraits;
    }
}
