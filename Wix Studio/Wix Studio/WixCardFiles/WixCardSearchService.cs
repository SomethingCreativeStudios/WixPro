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
            CardCollection cardCollection = new CardCollection();
            List<WixossCard> resultCards = new List<WixossCard>();
            List<WixossCard> totalCards = ( searchCard.setName != "" ? cardCollection.GetCardsInSets(searchCard.setName) : CardCollection.cardCollection.Values.ToList() );

            foreach ( var wixCard in totalCards)
            {
                Boolean addCard = true;// searchCard.isEmpty();
                if ( addCard )
                {
                    if ( !FallsInRange(searchCard.MinPower , searchCard.MaxPower , wixCard.Power) )
                        addCard = false;

                    if ( !FallsInRange(searchCard.MinLevel , searchCard.MaxLevel , wixCard.Level) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.Guard , wixCard.Guard) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.LifeBurst , wixCard.LifeBurst) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.MultiEner , wixCard.MultiEner) )
                        addCard = false;

                    if ( !CheckEnum<CardColor>(searchCard.Color , wixCard.Color) )
                        addCard = false;

                    if ( !CheckEnum<CardTiming>(searchCard.Timing , wixCard.Timing) )
                        addCard = false;

                    if ( (searchCard.Type != null && searchCard.Type != CardType.NoType) && searchCard.Type.Value != wixCard.Type )
                        addCard = false;

                    if ( searchCard.cardEffect != "" && !wixCard.CardEffect.ToLower().Contains(searchCard.cardEffect.ToLower()) )
                        addCard = false;

                    if ( searchCard.cardName != "" && !wixCard.CardName.ToLower().Contains(searchCard.cardName.ToLower()) )
                        addCard = false;
                }

                if ( addCard )
                    resultCards.Add(wixCard);
            }

            return resultCards;
        }

        public static Boolean CheckEnum<T>(Enum searchEnum, List<T> cardEnum)
        {
            bool cardMatches = true;

            if ( !searchEnum.ToString().StartsWith("No") )
            {
                cardMatches = false;
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
            if ( min == 0 && max == 0 )
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
