using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wix_Studio.WixCardFiles;

namespace Wix_Studio
{
    public class WixCardSearchService
    {
        public static List<WixossCard> FindCards(WixCardSearchModel searchCard, SortBy sortBy, SortOrder sortOrder)
        {
            List<WixossCard> resultCards = new List<WixossCard>();
            foreach ( var wixCard in CardCollection.cardCollection.Values )
            {
                if ( FallsInRange(searchCard.MinPower , searchCard.MaxPower , wixCard.Power) )
                    resultCards.Add(wixCard);

                else if ( FallsInRange(searchCard.MinLevel , searchCard.MaxLevel , wixCard.Level) )
                    resultCards.Add(wixCard);

                else if ( CheckBoolean(searchCard.Guard , wixCard.Guard) )
                    resultCards.Add(wixCard);

                else if ( CheckBoolean(searchCard.LifeBurst , wixCard.LifeBurst) )
                    resultCards.Add(wixCard);

                else if ( CheckBoolean(searchCard.MultiEner , wixCard.MultiEner) )
                    resultCards.Add(wixCard);

                else if ( CheckEnum<CardColor>(searchCard.Color , wixCard.Color) )
                    resultCards.Add(wixCard);

                else if ( CheckEnum<CardTiming>(searchCard.Timing , wixCard.Timing) )
                    resultCards.Add(wixCard);

                else if ( searchCard.Type != null && searchCard.Type.Value == wixCard.Type )
                    resultCards.Add(wixCard);

                else if ( searchCard.cardEffect != "" && wixCard.CardEffect.ToLower().Contains(searchCard.cardEffect.ToLower()) )
                    resultCards.Add(wixCard);

                else if ( searchCard.cardName != "" && wixCard.CardName.ToLower().Contains(searchCard.cardName.ToLower()) )
                    resultCards.Add(wixCard);

            }

            return resultCards;
        }

        public static Boolean CheckEnum<T>(Enum searchEnum, List<T> cardEnum)
        {
            bool cardMatches = false;

            if ( !searchEnum.ToString().StartsWith("No") )
            {
                foreach ( var enumValue in cardEnum )
                {
                    if ( searchEnum.ToString().Equals(enumValue.ToString()) )
                    {
                        cardMatches = true;
                        break;
                    }
                }
            } else
                cardMatches = true;

            return cardMatches;
        }
        public static Boolean CheckBoolean(Boolean? searchBool, Boolean cardBool)
        {
            bool cardMatches = true;

            if(searchBool != null )
            {
                cardMatches = searchBool == cardBool;
            }

            return cardMatches;
        }
        public static Boolean FallsInRange(int min , int max , int target)
        {
            bool inRange = false;

            if ( min == 0 && max == 0 ) //ignore range
                inRange = true;
            else if ( min == 0 && target <= max )
                inRange = true;
            else if ( max == 0 && target >= min )
                inRange = true;
            else if ( target >= min && target <= max )
                inRange = true;

            return inRange;
        }
    }
}
