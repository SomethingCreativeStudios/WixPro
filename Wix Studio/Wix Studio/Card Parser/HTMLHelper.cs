using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Wix_Studio.Card_Parser
{
    public class HTMLHelper
    {
        public static void getCardEffect(HtmlNode effect, ref WixossCard wixossCard)
        {
            String cardEffect = "";

            effect = effect.Descendants().Where
            (x => ( x.Name == "td")).ToList()[0];

            foreach ( var node in effect.DescendantsAndSelf() )
            {
                if ( !node.HasChildNodes )
                {
                    if(node.Name == "br" )
                    {
                        cardEffect += "\n";
                    }
                    if(node.Name == "img" && node.Attributes["class"].Value == "")
                    {
                        cardEffect += "{" + node.Attributes["alt"].Value + "}";
                        switch ( node.Attributes["alt"].Value )
                        {
                            case "Lifeburst":
                                wixossCard.LifeBurst = true;
                                break;
                            default:
                                break;
                        }
                    }
                    if(node.InnerText == "Guard" )
                    {
                        wixossCard.Guard = true;
                    }
                    if ( node.InnerText == "Multi Ener" )
                    {
                        wixossCard.MultiEner = true;
                    }
                    string text = node.InnerText;
                    if ( !string.IsNullOrEmpty(text) )
                        cardEffect += removeHTML(text);
                }
            }


            //Check timing
            if ( cardEffect.Contains("Use Timing") )
            {
                cardEffect = cardEffect.Replace("Use Timing" , "");
                var pattern = @"\[(.*?)\]";
                var matches = Regex.Matches(cardEffect , pattern);

                foreach ( Match m in matches )
                {
                    switch ( m.Groups[1].Value )
                    {
                        case "Attack Phase":
                            wixossCard.Timing.Add(CardTiming.AttackPhase);
                            cardEffect = cardEffect.Replace("[Attack Phase]" , "");
                            break;
                        case "Main Phase":
                            wixossCard.Timing.Add(CardTiming.MainPhase);
                            cardEffect = cardEffect.Replace("[Main Phase]" , "");
                            break;
                        case "Spell Cut-In":
                            wixossCard.Timing.Add(CardTiming.SpellCutIn);
                            cardEffect = cardEffect.Replace("[Spell Cut-In]" , "");
                            break;
                        default:
                            break;
                    }
                }
            }

            wixossCard.CardEffect = removeHTML(cardEffect).Trim();
        }

        public static void handleCardCost(HtmlNode costRow , ref WixossCard wixossCard)
        {
            List<HtmlNode> imgNodes = costRow.Descendants().Where
             (x => ( x.Name == "a" )).ToList();

            List<CardCost> costForCard = new List<CardCost>();
            List<long> cardCostList = new List<long>();

            foreach ( var item in removeHTML(costRow.InnerText).Split(new String[] { " x " } , StringSplitOptions.RemoveEmptyEntries) )
            {
                if(item.Trim() != "" )
                {
                    String cleanedItem = item;

                    if ( item.Contains("or") )
                    {
                        cleanedItem = item.Split(' ')[0];
                    }

                    cardCostList.Add(Convert.ToInt16(cleanedItem.Trim()));
                }
            }

            for ( int i = 0; i < imgNodes.Count; i++ )
            {
                CardCost cost = new CardCost();

                if ( i < cardCostList.Count )
                    cost.numberPerColor = (int)cardCostList[i];
                else
                    cost.numberPerColor = 0;

                cost.color = colorFromName(imgNodes[i].Attributes["title"].Value);
                costForCard.Add(cost);
            }
            wixossCard.Cost = costForCard;
        }
        public static void handleCardColor(HtmlNode colorRow , ref WixossCard wixossCard)
        {
            List<HtmlNode> imgNodes = colorRow.Descendants().Where
             (x => ( x.Name == "img" )).ToList();

            List<CardColor> colorsForCard = new List<CardColor>();
            foreach ( var imgNode in imgNodes )
            {
                colorsForCard.Add(colorFromName(imgNode.Attributes["alt"].Value));
            }

            wixossCard.Color = colorsForCard;
        }

        private static CardColor colorFromName(String colorName)
        {
            CardColor cardColor = CardColor.Colorless;

            switch ( colorName )
            {
                case "Green":
                    cardColor = CardColor.Green;
                    break;
                case "Black":
                    cardColor = CardColor.Black;
                    break;
                case "Blue":
                    cardColor = CardColor.Blue;
                    break;
                case "White":
                    cardColor = CardColor.White;
                    break;
                case "Red":
                    cardColor = CardColor.Red;
                    break;
                case "Colorless":
                    cardColor = CardColor.Colorless;
                    break;
            }

            return cardColor;
        }

        public static String removeHTML(String htmlText)
        {
            htmlText = htmlText.Replace("&lt;", "<");
            htmlText = htmlText.Replace("&gt;" , ">");
            htmlText = htmlText.Replace("Ã—" , "x");
            htmlText = htmlText.Replace("&#61;" , "=");
            htmlText = htmlText.Replace("â‘¡" , "(2)");
            htmlText = htmlText.Replace("â‘" , "(1)");
            htmlText = htmlText.Replace("â™¥" , "♥");
            


            return htmlText;
        }


        public static Boolean TableHeaderContains(HtmlNode tableNode , String headerText)
        {
            return indexOfRow(tableNode , headerText) != -1;
        }

        public static int indexOfRow(HtmlNode tableNode, String headerText)
        {
            int rowIndex = -1;

            foreach ( HtmlNode row in tableNode.SelectNodes("tr") )
            {
                HtmlNodeCollection cells = row.SelectNodes("th");

                if ( cells == null )
                {
                    continue;
                }

                int index = 0;
                foreach ( HtmlNode cell in cells )
                {
                    if ( cell.InnerText.Contains(headerText) )
                    {
                        rowIndex = index;
                        break;
                    }
                    index++;
                }

                if ( rowIndex != -1 )
                    break;
            }

            return rowIndex;
        }

        public static List<String> getRowData(HtmlNode tableNode , String headerText)
        {
            List<String> rowDataList = new List<string>();
            int rowIndex = indexOfRow(tableNode , headerText);

            if ( rowIndex != -1 )
            {
                foreach ( HtmlNode row in tableNode.SelectNodes("tr") )
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    if ( cells == null )
                    {
                        continue;
                    }

                    int index = 0;
                    foreach ( HtmlNode cell in cells )
                    {
                        if ( index == rowIndex )
                        {
                            rowDataList.Add(cell.InnerHtml.Trim());
                            break;
                        }
                        index++;
                    }
                }
            }
            return rowDataList;
        }
    }
}
