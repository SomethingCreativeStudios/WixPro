using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wix_Studio.WixCardFiles
{
    public static class WixossDeckHelper
    {
        public static int totalCountOfCard(WixossCard cardToCheck , List<WixossCard> deck , bool ignoreMaxCount = false)
        {
            int totalCount = 0;

            foreach ( var wixossCard in deck )
            {
                if ( wixossCard.Equals(cardToCheck) )
                    totalCount++;

                if ( totalCount == 4 && !ignoreMaxCount )
                    break;
            }

            return totalCount;
        }

        public static int totalCountOfLifeBurst(List<WixossCard> deck , bool ignoreMaxCount = false)
        {
            int totalCount = 0;

            foreach ( var wixossCard in deck )
            {
                if ( wixossCard.LifeBurst )
                    totalCount++;

                if ( totalCount == 20 && !ignoreMaxCount )
                    break;
            }

            return totalCount;
        }

        public static bool isLegalDeck(List<WixossCard> mainDeck, List<WixossCard> lrigDeck, bool showReport)
        {
            bool has40cards = mainDeck.Count == 40;
            bool has20LifeBursts = totalCountOfLifeBurst(mainDeck , true) == 20;

            bool hasLessThen11Cards = lrigDeck.Count <= 10;
            bool hasLevel0Lrig = false;

            foreach ( var wixCard in lrigDeck )
            {
                if ( wixCard.Type == CardType.LRIG && wixCard.Level == 0 )
                {
                    hasLevel0Lrig = true;
                    break;
                }
            }

            String deckReport = "Deck Report:\n";

            deckReport += "Main Deck:\n";
            deckReport += "\tHas 40 cards: " + has40cards;
            deckReport += "\n\tHas 20 LifeBursts: " + has20LifeBursts;

            deckReport += "\n\nLRIG Deck:\n";
            deckReport += "\tNo More Than 10 Cards: " + hasLessThen11Cards;
            deckReport += "\n\tHas a Level 0 LRIG: " + hasLevel0Lrig;

            if( showReport )
                System.Windows.Forms.MessageBox.Show(deckReport);

            return has40cards && has20LifeBursts && hasLessThen11Cards && hasLevel0Lrig;
        }

        public static bool cardAllowedInMainDeck(WixossCard cardToCheck)
        {
            bool cardAllowed = true;

            if ( cardToCheck.Type == CardType.ARTS )
                cardAllowed = false;
            else if ( cardToCheck.Type == CardType.LRIG )
                cardAllowed = false;
            else if ( cardToCheck.Type == CardType.Resona )
                cardAllowed = false;
                    
            return cardAllowed;
        }

        public static bool cardAllowedILRIGDeck(WixossCard cardToCheck)
        {
            bool cardAllowed = true;

            if ( cardToCheck.Type == CardType.SIGNI )
                cardAllowed = false;
            else if ( cardToCheck.Type == CardType.Spell )
                cardAllowed = false;

            return cardAllowed;
        }

    }
}
