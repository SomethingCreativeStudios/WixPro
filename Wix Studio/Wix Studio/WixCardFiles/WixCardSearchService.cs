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
                Boolean addCard = searchCard.isEmpty();
                if ( !addCard )
                {
                    if ( FallsInRange(searchCard.MinPower , searchCard.MaxPower , wixCard.Power) )
                        addCard = true;

                    else if ( FallsInRange(searchCard.MinLevel , searchCard.MaxLevel , wixCard.Level) )
                        addCard = true;

                    else if ( CheckBoolean(searchCard.Guard , wixCard.Guard) )
                        addCard = true;

                    else if ( CheckBoolean(searchCard.LifeBurst , wixCard.LifeBurst) )
                        addCard = true;

                    else if ( CheckBoolean(searchCard.MultiEner , wixCard.MultiEner) )
                        addCard = true;

                    else if ( CheckEnum<CardColor>(searchCard.Color , wixCard.Color) )
                        addCard = true;

                    else if ( CheckEnum<CardTiming>(searchCard.Timing , wixCard.Timing) )
                        addCard = true;

                    else if ( searchCard.Type != null && searchCard.Type.Value == wixCard.Type )
                        addCard = true;

                    else if ( searchCard.cardEffect != "" && wixCard.CardEffect.ToLower().Contains(searchCard.cardEffect.ToLower()) )
                        addCard = true;

                    else if ( searchCard.cardName != "" && wixCard.CardName.ToLower().Contains(searchCard.cardName.ToLower()) )
                        addCard = true;
                }

                if ( addCard )
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
            }

            return cardMatches;
        }

        public static Boolean CheckBoolean(Boolean? searchBool, Boolean cardBool)
        {
            bool cardMatches = false;

            if(searchBool != null )
            {
                cardMatches = searchBool == cardBool;
            }

            return cardMatches;
        }
        public static Boolean FallsInRange(int min , int max , int target)
        {
            bool inRange = false;
            if ( min == 0 && max == 0 )
                inRange = false;
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
