using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Wix_Studio;
using Wix_Studio.Card_Parser;
using Wix_Studio.WixCardFiles;

public class CardCollection
{
    //public static string basePath = @"D:\WS PRO\Assets\Card Images\Resources\sets";//Application.dataPath + @"\Card Images\Resources\sets";

    /// <summary>
    /// Base path to sets, ends with \
    /// </summary>
    public static string baseSetPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Wix Cards\sets\";

    /// <summary>
    /// Base path to decks, ends with \
    /// </summary>
    public static string deckBasePath = baseSetPath + "decks\\";

    /// <summary>
    /// Path to set images, ends with \
    /// </summary>
    public static string setImages = baseSetPath + "setimages\\";

    public static Dictionary<String , WixossCard> cardCollection;

    public CardCollection()
    {
        cardCollection = new Dictionary<string , WixossCard>();
        CreateNeededFolders();
        LoadAllCards();
    }
    
    public void CreateNeededFolders()
    {
        if ( !Directory.Exists(baseSetPath) )
        {
            Directory.CreateDirectory(baseSetPath);
        }

        if ( !Directory.Exists(setImages) )
        {
            Directory.CreateDirectory(setImages);
        }

        if ( !Directory.Exists(deckBasePath) )
        {
            Directory.CreateDirectory(deckBasePath);
        }

        if ( !Directory.Exists(AuditLog.logPath) )
        {
            Directory.CreateDirectory(AuditLog.logPath);
        }
    }

    public List<WixossCard> GetSet(string setName)
    {
        List<WixossCard> cards = new List<WixossCard>();
        StreamReader reader = File.OpenText(baseSetPath + setName + ".xml");
        using ( var stream = new StringReader(reader.ReadToEnd()) )
        {
            var serializer = new XmlSerializer(typeof(List<WixossCard>));
            cards = new List<WixossCard>(serializer.Deserialize(stream) as List<WixossCard>);
            stream.Close();
            reader.Close();
        }

        return cards;
    }

    public void SaveSet(List<WixossCard> cardsInSet)
    {
        SaveSet(cardsInSet[0].CardSet , cardsInSet);
    }

    public void SaveSet(String setName , List<WixossCard> cardsInSet)
    {
        String filePath = baseSetPath + setName + ".xml";

        try
        {
            foreach ( var cardInSet in cardsInSet )
            {
                if ( !Directory.Exists(setImages + cardInSet.CardSet) )
                {
                    Directory.CreateDirectory(setImages + cardInSet.CardSet);
                }
                if ( !File.Exists(cardInSet.CardImagePath) )
                {

                    using ( WebClient client = new WebClient() )
                    {
                        if ( cardInSet.CardNumberInSet == null || cardInSet.CardNumberInSet.Contains("???") )
                            cardInSet.CardNumberInSet = cardInSet.CardName;

                        String newFilePath = CardCollection.setImages + cardInSet.CardSet + "\\" + cardInSet.CardNumberInSet + ".jpg";
                        if ( cardInSet.ImageUrl != null )
                        {
                            String urlName = cardInSet.ImageUrl;
                            client.DownloadFileAsync(new Uri(urlName) , newFilePath , cardInSet);
                        }
                    }
                }
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine("ERROR!");
        }

        XmlSerializer xsSubmit = new XmlSerializer(typeof(List<WixossCard>));
        using ( StringWriter sww = new StringWriter() )
        using ( XmlWriter writer = XmlWriter.Create(sww) )
        {
            xsSubmit.Serialize(writer , cardsInSet);
            File.WriteAllText(filePath , PrintXML(sww.ToString()));
            writer.Close();
        }
    }

    /// <summary>
    /// Get all sets found in base path
    /// </summary>
    /// <returns>names of all found sets</returns>
    public List<string> GetAllSets()
    {
        List<string> cardSets = new List<string>();
        string[] files = Directory.GetFiles(baseSetPath);
        for ( int i = 0; i < files.Length; i++ )
        {
            if ( files[i].EndsWith(".xml") && !files[i].Contains("nocard.txt") )
                cardSets.Add(files[i].Replace(baseSetPath , "").Replace(".xml" , ""));
        }

        return cardSets;
    }

    /// <summary>
    /// Get all sets thats starts with filter
    /// </summary>
    /// <returns>names of all found sets</returns>
    public List<string> GetAllSets(String filter)
    {
        List<string> cardSets = new List<string>();
        string[] files = Directory.GetFiles(baseSetPath);
        for ( int i = 0; i < files.Length; i++ )
        {
            bool passesFilter = ( filter != "" ? files[i].Contains(filter) : true);
            if ( files[i].EndsWith(".xml") && !files[i].Contains("nocard.txt") && passesFilter)
                cardSets.Add(files[i].Replace(baseSetPath , "").Replace(".xml" , ""));
        }

        return cardSets;
    }

    /// <summary>
    /// Get all cards in sets that starts with filter
    /// </summary>
    /// <returns>cards of all found sets</returns>
    public List<WixossCard> GetCardsInSets(String filter)
    {
        List<WixossCard> cardSets = new List<WixossCard>();
        List<String> setNames = GetAllSets(filter);
        foreach ( var setName in setNames )
        {
            cardSets.AddRange(GetSet(setName));
        }

        return cardSets;
    }

    public void LoadAllCards()
    {
        List<string> cardSets = GetAllSets();
        List<WixossCard> allCards = new List<WixossCard>();

        for ( int i = 0; i < cardSets.Count; i++ )
        {
            try {
                foreach ( var tempCard in GetSet(cardSets[i]) )
                {
                    if ( tempCard.ImageUrl != null && !cardCollection.ContainsKey(tempCard.ImageUrl) )
                        cardCollection.Add(tempCard.ImageUrl , tempCard);
                }
            }catch(Exception ex )
            {
                Console.WriteLine("Error occured when getting set id: " + cardSets[i]);
            }
        }
    }
    
    private WixossCard cardForName(string name)
    {

        for ( int i = 0; i < cardCollection.Keys.Count; i++ )
        {
            if ( cardCollection[cardCollection.Keys.ElementAt(i)].CardName == name )
                return cardCollection[cardCollection.Keys.ElementAt(i)];
        }

        return null;
    }

   


    public static List<String> getAllCardNames()
    {
        List<String> cardNames = new List<string>();
        foreach ( var cardKey in cardCollection.Keys )
        {
            String cardName = cardCollection[cardKey].CardName;
            if ( !cardNames.Contains(cardName.Trim()) )
                cardNames.Add(cardName.Trim());
        }
        return cardNames;
    }

    public static String PrintXML(String XML)
    {
        String Result = "";

        MemoryStream mStream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(mStream , Encoding.Unicode);
        XmlDocument document = new XmlDocument();

        try
        {
            // Load the XmlDocument with the XML.
            document.LoadXml(XML);

            writer.Formatting = Formatting.Indented;

            // Write the XML into a formatting XmlTextWriter
            document.WriteContentTo(writer);
            writer.Flush();
            mStream.Flush();

            // Have to rewind the MemoryStream in order to read
            // its contents.
            mStream.Position = 0;

            // Read MemoryStream contents into a StreamReader.
            StreamReader sReader = new StreamReader(mStream);

            // Extract the text from the StreamReader.
            String FormattedXML = sReader.ReadToEnd();

            Result = FormattedXML;
        }
        catch ( XmlException )
        {
        }

        mStream.Close();
        writer.Close();

        return Result;
    }
}
