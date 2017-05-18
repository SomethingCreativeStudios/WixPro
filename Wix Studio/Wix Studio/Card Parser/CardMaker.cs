using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Threading;

namespace Wix_Studio.Card_Parser
{
    public class CardMaker
    {
        public static String urlName = "http://selector-wixoss.wikia.com";
        String boosterPackUrl = "";
        System.Windows.Forms.WebBrowser browser = new System.Windows.Forms.WebBrowser();

        public Dictionary<String, String> GetAllSets(String url)
        {
            HtmlNode deckTable = null;
            Dictionary<String , String> setList = new Dictionary<string , string>();
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(url));

            List<HtmlNode> cardTables = html.DocumentNode.Descendants().Where
            (x => ( x.Name == "table" && x.Attributes["width"] != null &&
               x.Attributes["width"].Value.Contains("100%") )).ToList();
            
            foreach ( HtmlNode row in cardTables[0].SelectNodes("tr" ) )
            {
                HtmlNodeCollection cells = row.SelectNodes("td/ul/li");

                if ( cells == null )
                {
                    continue;
                }

                int index = 0;
                CardCollection cardCollection = new CardCollection();
                foreach ( HtmlNode cell in cells )
                {
                    String urlToSet = urlName + "/" + cell.FirstChild.Attributes["href"].Value.Trim();
                    String setName = cell.FirstChild.Attributes["title"].Value.Split(' ')[0];
                    setList.Add(setName, urlToSet);
                }
            }

            return setList;
        }

        /// <summary>
        /// This method is awful, and I kinda hate myself for writing it
        /// But.... it works so...
        /// </summary>
        /// <returns></returns>
        public Dictionary<String , String> GetBoosterSets()
        {
            Dictionary<String , String> setList = new Dictionary<string , string>();
            browser.ScriptErrorsSuppressed = true;

            var html = new HtmlDocument();
            Boolean keepLooking = true;
            Boolean websiteDownloading = false;
            int currentSet = 1;
            string url = "http://selector-wixoss.wikia.com/wiki/WX-";
            boosterPackUrl = url + makeSetName(currentSet);

            while ( keepLooking )
            {
                while ( websiteDownloading )
                    System.Windows.Forms.Application.DoEvents();

                browser.Navigate(boosterPackUrl);
                websiteDownloading = true;
                browser.DocumentCompleted += (s , e) =>
                {
                    //Stop loading the same set
                    if ( e.Url.ToString() != boosterPackUrl )
                        return;

                    String htmlStr = browser.DocumentText;
                    html.LoadHtml(htmlStr);
                    if ( htmlStr.Contains("alternative-suggestion") )
                    {
                        //Dig down and find the a tag we need
                        var wikiNode = html.GetElementbyId("WikiaArticle");
                        var spanClasses = wikiNode
                        .Descendants("span")
                        .Where(d =>
                           d.Attributes.Contains("class")
                           &&
                           d.Attributes["class"].Value.Contains("mw-headline")
                        );

                        var htmlClasses = spanClasses.ElementAt(0).Descendants("span")
                         .Where(d =>
                            d.Attributes.Contains("class")
                            &&
                            d.Attributes["class"].Value.Contains("alternative-suggestion")
                         );

                        var aLink = htmlClasses.ElementAt(0).Descendants("a").ElementAt(0);

                        //Sometimes we might hit a duplicate(shouldnt happen, but this is a saftey check)
                        if ( !setList.ContainsKey(aLink.Attributes["title"].Value) )
                            setList.Add(aLink.Attributes["title"].Value , "http://selector-wixoss.wikia.com" + aLink.Attributes["href"].Value);

                        //Load up next booster pack url
                        currentSet++;
                        boosterPackUrl = url + makeSetName(currentSet);
                    } else
                        keepLooking = false;

                    websiteDownloading = false;
                };
            }

            return setList;
        }

        private String makeSetName(int currentSet)
        {
            string setName = "";

            if ( currentSet < 10 )
                setName = "0";

            return setName + currentSet.ToString();
        }

        public Dictionary<String,int> GetCardsFromUrl(String urlOfDeck)
        {

            Dictionary<String , int> listOfCards = new Dictionary<string , int>();
            HtmlNode deckTable = null;
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(urlOfDeck));
            var root = html.DocumentNode;

            List<HtmlNode> cardTables = html.DocumentNode.Descendants().Where
            (x => ( x.Name == "table")).ToList();

            foreach ( HtmlNode row in cardTables )
            {
                if(HTMLHelper.TableHeaderContains(row,"Card Name") )
                {
                    deckTable = row;
                    break;
                }
            }


            if ( deckTable != null )
            {
                List<String> numberOfCards = HTMLHelper.getRowData(deckTable , "Qty");
                int index = 0;
                foreach ( var aTagText in HTMLHelper.getRowData(deckTable , "Card Name") )
                {
                    HtmlDocument justATag = new HtmlDocument();
                    justATag.LoadHtml(aTagText);
                    String cardUrl = "";
                    try { cardUrl = justATag.DocumentNode.ChildNodes["a"].Attributes["href"].Value; }
                    catch ( Exception ex ) { cardUrl = justATag.DocumentNode.ChildNodes["b"].ChildNodes["a"].Attributes["href"].Value; }


                    if ( !cardUrl.StartsWith(urlName) )
                        cardUrl = urlName + cardUrl;

                    int qty = 1;
                    if ( numberOfCards.Count != 0 )
                    {
                        qty = Convert.ToInt16(numberOfCards[index].Replace("x" , ""));
                    }
                    if ( !listOfCards.ContainsKey(cardUrl) )
                        listOfCards.Add(cardUrl , qty);

                    index++;
                }
            }

            return listOfCards;
        }

        public WixossCard GetCardFromUrl(String urlOfCard)
        {
            WixossCard wixossCard = new WixossCard();

            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(urlOfCard));
            var root = html.DocumentNode;
            List<HtmlNode> cardTable = html.DocumentNode.Descendants().Where
            (x => ( x.Name == "div" && x.Attributes["id"] != null &&
               x.Attributes["id"].Value.Contains("cftable") )).ToList();

            if ( cardTable.Count != 0 )
            {
                wixossCard.CardName = getCardName(cardTable[0]);//info-main
                setUpCard(cardTable[0] , wixossCard);
            }

            List<HtmlNode> imageTable = html.DocumentNode.Descendants().Where
            (x => ( x.Name == "img" && x.Attributes["width"] != null &&
               x.Attributes["width"].Value.Contains("250") && x.Attributes["height"] != null &&
               (x.Attributes["height"].Value.StartsWith("3")) ) ).ToList();

            wixossCard.CardUrl = urlOfCard;
            if ( imageTable.Count != 0 )
            {

                wixossCard.ImageUrl = imageTable[0].Attributes["src"].Value;
            }

          

            return wixossCard;
        }

        private String getCardName(HtmlNode tableNode)
        {
            HtmlNode header = tableNode.Descendants().Where
                    (x => ( x.Name == "div" && x.Attributes["id"] != null && x.Attributes["id"].Value.Contains("header") )).ToList()[0];

            String cardName = header.InnerHtml;

            if ( cardName.Contains("<br>") )
                cardName = cardName.Split(new string[] { "<br>" } , StringSplitOptions.None)[0];

            return HTMLHelper.removeHTML(cardName);
        }

        private void setUpCard(HtmlNode tableNode , WixossCard wixossCard)
        {
            HtmlNode infoTable = tableNode.Descendants().Where
                    (x => ( x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("info-main") )).ToList()[0];

            infoTable = infoTable.Descendants().Where
                    (x => ( x.Name == "table" )).ToList()[0];

            Dictionary<String , HtmlNode> tableMap = htmlTableToDictionary(infoTable);
            setInfoTable(tableMap, wixossCard);

            setEffect(tableNode , wixossCard);
            setCardSetInfo(tableNode , wixossCard);

        }

        private void setInfoTable(Dictionary<String , HtmlNode> tableMap, WixossCard wixossCard)
        {
            foreach ( var key in tableMap.Keys )
            {
                switch ( key )
                {
                    case "LevelLimit":
                        try { wixossCard.LevelLimit = Convert.ToInt16(tableMap[key].FirstChild.InnerText.Trim()); }
                        catch (Exception ex){ wixossCard.LevelLimit = 0; }
                        break;
                    case "Card Type":
                        wixossCard.Type = (CardType)Enum.Parse(typeof(CardType) , tableMap[key].ChildNodes[1].InnerText.Trim());
                        break;
                    case "Level":
                        wixossCard.Level = Convert.ToInt16(tableMap[key].FirstChild.InnerText.Trim());
                        break;
                    case "Color":
                        HTMLHelper.handleCardColor(tableMap[key] , ref wixossCard);
                        break;
                    case "Grow Cost":
                        HTMLHelper.handleCardCost(tableMap[key] , ref wixossCard);
                        break;
                    case "Cost":
                        HTMLHelper.handleCardCost(tableMap[key] , ref wixossCard);
                        break;
                    case "Power":
                        wixossCard.Power = Convert.ToInt16(tableMap[key].FirstChild.InnerText.Trim());
                        break;
                    case "Class":
                        wixossCard.Class.Add(tableMap[key].ChildNodes[1].InnerText.Trim());
                        break;
                    case "Use Timing":
                        var pattern = @"\[(.*?)\]";
                        var matches = Regex.Matches(tableMap[key].FirstChild.InnerText.Trim() , pattern);

                        foreach ( Match m in matches )
                        {
                            switch ( m.Groups[1].Value )
                            {
                                case "Attack Phase":
                                    wixossCard.Timing.Add(CardTiming.AttackPhase);
                                    break;
                                case "Main Phase":
                                    wixossCard.Timing.Add(CardTiming.MainPhase);
                                    break;
                                case "Spell Cut-In":
                                    wixossCard.Timing.Add(CardTiming.SpellCutIn);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void setEffect(HtmlNode tableNode , WixossCard wixossCard)
        {
            HtmlNode effectTable = tableNode.Descendants().Where
                   (x => ( x.Name == "table" )).ToList()[1];

            if ( HTMLHelper.TableHeaderContains(effectTable , "Card Abilities") )
                HTMLHelper.getCardEffect(effectTable , ref wixossCard);
        }

        private void setCardSetInfo(HtmlNode tableNode , WixossCard wixossCard)
        {
            HtmlNode setTable = null;
            List<HtmlNode> tableNodes = tableNode.Descendants().Where
                   (x => ( x.Name == "table" )).ToList();

            foreach ( var theTableNode in tableNodes )
            {
                if ( HTMLHelper.TableHeaderContains(theTableNode , "Sets") )
                    setTable = theTableNode;
            }

            if ( setTable != null )
            {
                foreach ( var setInfo in HTMLHelper.getRowData(setTable , "Sets") )
                {
                    if ( setInfo != "" )
                    {
                        HtmlDocument htmlTableData = new HtmlDocument();
                        htmlTableData.LoadHtml(setInfo);

                        var aTags = htmlTableData.DocumentNode.Descendants("a")
                         .Where(d =>
                            d.Attributes.Contains("title")
                            &&(
                            d.Attributes["href"].Value.Contains("WX") ||
                            d.Attributes["href"].Value.Contains("SP"))
                         );

                        foreach ( var aTag in aTags )
                        {
                            //A very cheap way to do this.
                            //Will have to update is they ever do in the three digits
                            String setName = aTag.Attributes["href"].Value;
                            int maxCount = setName.Contains("WXD") ? 6 : 5;
                            setName = setName.Substring(setName.LastIndexOf("/") +  1, maxCount);
                            wixossCard.CardSets.Add(setName);
                        }
                    }
                }
            }
           
        }


        private Dictionary<String, HtmlNode> htmlTableToDictionary(HtmlNode tableNode)
        {
            Dictionary<String , HtmlNode> tableValues = new Dictionary<string , HtmlNode>();

            List<HtmlNode> tableRows = tableNode.Descendants().Where
                    (x => ( x.Name == "tr" )).ToList();

            foreach ( var tableRow in tableRows )
            {
                String title = "";
                HtmlNode value = null;

                title = tableRow.ChildNodes[1].InnerText.Trim();
                value = tableRow.ChildNodes[2];

                tableValues.Add(title , value);
            }

            return tableValues;
        }
    }
}
