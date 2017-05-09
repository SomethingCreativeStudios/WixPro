using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wix_Studio.WixCardFiles;

namespace Wix_Studio
{
    public class WixCardSearchService
    {
        public static List<WixossCard> Search(WixCardSearchModel searchCard , SortBy sortBy , SortOrder sortOrder)
        { 
            CardCollection cardCollection = new CardCollection();
            List<WixossCard> resultCards = new List<WixossCard>();
            List<WixossCard> totalCards = ( searchCard.setName != "" ? cardCollection.GetCardsInSets(searchCard.setName) : CardCollection.cardCollection.Values.ToList() );
            
            

            return resultCards;
        }

        public static Boolean CheckEnum<T>(Enum searchEnum , List<T> cardEnum)
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

        public static Boolean CheckBoolean(Boolean? searchBool , Boolean cardBool)
        {
            bool cardMatches = true;

            if ( searchBool != null )
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
