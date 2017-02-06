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

public class CardCollection
{
    //public static string basePath = @"D:\WS PRO\Assets\Card Images\Resources\sets";//Application.dataPath + @"\Card Images\Resources\sets";
    public static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Wix Cards\sets\";
    public static string deckBasePath = basePath + "decks\\";
    public static string setImages = basePath + "setimages\\";
    public static Dictionary<String , WixossCard> cardCollection;

    public CardCollection()
    {
        cardCollection = new Dictionary<string , WixossCard>();
        CreateNeededFolders();
        LoadAllCards();
    }
    
    public void CreateNeededFolders()
    {
        if ( !Directory.Exists(basePath) )
        {
            Directory.CreateDirectory(basePath);
        }

        if ( !Directory.Exists(setImages) )
        {
            Directory.CreateDirectory(setImages);
        }

        if ( !Directory.Exists(deckBasePath) )
        {
            Directory.CreateDirectory(deckBasePath);
        }
    }

    public List<WixossCard> GetSet(string setName)
    {
        List<WixossCard> cards = new List<WixossCard>();

        using ( var stream = new StringReader(File.OpenText(basePath + setName + ".xml").ReadToEnd()) )
        {
            var serializer = new XmlSerializer(typeof(List<WixossCard>));
            cards = new List<WixossCard>(serializer.Deserialize(stream) as List<WixossCard>);
        }

        return cards;
    }

    public void SaveSet(List<WixossCard> cardsInSet)
    {
        SaveSet(cardsInSet[0].CardSet , cardsInSet);
    }

    public void SaveSet(String setName, List<WixossCard> cardsInSet)
    {
        if(!Directory.Exists(basePath + setName) )
        {
            String filePath = basePath + setName + ".xml";
            if ( !File.Exists(filePath) )
            {
                File.Create(filePath).Close();
            } else
            {
              File.Delete(filePath);
            }

            try
            {
                foreach ( var cardInSet in cardsInSet )
                {
                    if(!Directory.Exists(setImages + cardInSet.CardSet) )
                    {
                        Directory.CreateDirectory(setImages + cardInSet.CardSet);
                    }

                    using ( WebClient client = new WebClient() )
                    {
                        String newFilePath = setImages + cardInSet.CardSet + "\\" + cardInSet.CardNumberInSet + ".jpg";
                        client.DownloadFileAsync(new Uri(cardInSet.ImageUrl) , newFilePath);
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
            }
        }
    }

    public List<string> GetAllSets()
    {
        List<string> cardSets = new List<string>();
        string[] files = Directory.GetFiles(basePath);
        for ( int i = 0; i < files.Length; i++ )
        {
            if ( files[i].EndsWith(".xml") && !files[i].Contains("nocard.txt") )
                cardSets.Add(files[i].Replace(basePath , "").Replace(".xml" , ""));
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

    private String PrintXML(String XML)
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
