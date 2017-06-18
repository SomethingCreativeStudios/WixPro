using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Wix_Studio;
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

    public static Dictionary<String, WixossCard> cardCollection;

    public CardCollection()
    {
        cardCollection = new Dictionary<string, WixossCard>();
        CreateNeededFolders();
        //LoadAllCards();
    }

    public void CreateNeededFolders()
    {
        if (!Directory.Exists(baseSetPath))
        {
            Directory.CreateDirectory(baseSetPath);
        }

        if (!Directory.Exists(setImages))
        {
            Directory.CreateDirectory(setImages);
        }

        if (!Directory.Exists(deckBasePath))
        {
            Directory.CreateDirectory(deckBasePath);
        }

       /* if (!Directory.Exists(AuditLog.logPath))
        {
            Directory.CreateDirectory(AuditLog.logPath);
        }*/
    }

    public List<WixossCard> GetSet(string setName)
    {
        List<WixossCard> cards = new List<WixossCard>();
        WixCardSearchModel searchModel = new WixCardSearchModel();
        searchModel.setName = setName.Substring(1);
        return null;// WixCardService.Search(searchModel);
    }

    /// <summary>
    /// Get all sets found in base path
    /// </summary>
    /// <returns>names of all found sets</returns>
    public List<string> GetAllSets()
    {
        return null;// WixCardService.GetSetNames();
    }

    public void LoadAllCards()
    {
        List<string> cardSets = GetAllSets();
        List<WixossCard> allCards = new List<WixossCard>();

      /*  foreach (var tempCard in WixCardService.getAllCards())
        {
            try
            {
                if (tempCard.ImageUrl != null && !cardCollection.ContainsKey(tempCard.ImageUrl))
                    cardCollection.Add(tempCard.ImageUrl, tempCard);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured when getting card id: " + tempCard.Id);
            }
        }*/

    }

    private WixossCard cardForName(string name)
    {

        for (int i = 0; i < cardCollection.Keys.Count; i++)
        {
            if (cardCollection[cardCollection.Keys.ElementAt(i)].CardName == name)
                return cardCollection[cardCollection.Keys.ElementAt(i)];
        }

        return null;
    }




    public static List<String> getAllCardNames()
    {
        return null;// WixCardService.GetAllCardNames();
    }

    public static String PrintXML(String XML)
    {
        String Result = "";

        MemoryStream mStream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
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
        catch (XmlException)
        {
        }

        mStream.Close();
        writer.Close();

        return Result;
    }
}
