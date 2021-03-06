﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wix_Studio.WixCardFiles
{
    public class WixossDeck
    {
        public List<WixossCard> MainDeck { get; set; }
        public List<WixossCard> LRIGDeck { get; set; }

        public WixossDeck()
        {
            MainDeck = new List<WixossCard>();
            LRIGDeck = new List<WixossCard>();
        }
        
        public int totalCountOfCard(WixossCard cardToCheck , DeckType deckType , bool ignoreMaxCount = false)
        {
            int totalCount = 0;

            foreach ( var wixossCard in (deckType == DeckType.Main ?  MainDeck : LRIGDeck) )
            {
                if ( wixossCard.Equals(cardToCheck) )
                    totalCount++;

                if ( totalCount == 4 && !ignoreMaxCount )
                    break;
            }

            return totalCount;
        }
        
        public int totalCountOfLifeBurst(bool ignoreMaxCount = false)
        {
            int totalCount = 0;

            foreach ( var wixossCard in MainDeck )
            {
                if ( wixossCard.LifeBurst )
                    totalCount++;

                if ( totalCount == 20 && !ignoreMaxCount )
                    break;
            }

            return totalCount;
        }

        public bool isLegalDeck(bool showReport)
        {
            bool has40cards = MainDeck.Count == 40;
            bool has20LifeBursts = totalCountOfLifeBurst(true) == 20;

            bool hasLessThen11Cards = LRIGDeck.Count <= 10;
            bool hasLevel0Lrig = false;

            foreach ( var wixCard in LRIGDeck )
            {
                if ( wixCard.Type == WixossCard.CardType.LRIG && wixCard.Level == 0 )
                {
                    hasLevel0Lrig = true;
                    break;
                }
            }

            String deckReport = "Deck Report:\n";

            deckReport += "Main Deck(" + (( has40cards && has20LifeBursts) ? "Passed" : "Failed") +"):\n";
            deckReport += "\tHas 40 cards: " + (has40cards ? "Passed" : "Failed(" + MainDeck.Count + ")");
            deckReport += "\n\tHas 20 LifeBursts: " + ( has20LifeBursts ? "Passed" : "Failed(" + totalCountOfLifeBurst(true) + ")" );

            deckReport += "\n\nLRIG Deck(" + ( ( hasLessThen11Cards && hasLevel0Lrig ) ? "Passed" : "Failed" ) + "):\n";
            deckReport += "\tNo More Than 10 Cards: " + ( hasLessThen11Cards ? "Passed" : "Failed(" + LRIGDeck.Count + ")" );
            deckReport += "\n\tHas a Level 0 LRIG: " + ( hasLevel0Lrig ? "Passed" : "Failed");

            return has40cards && has20LifeBursts && hasLessThen11Cards && hasLevel0Lrig;
        }

        public void RemoveAt(int index, DeckType deckType)
        {
            switch ( deckType )
            {
                case DeckType.Main:
                    MainDeck.RemoveAt(index);
                    break;
                case DeckType.LRIG:
                    LRIGDeck.RemoveAt(index);
                    break;
                case DeckType.Both:
                    MainDeck.RemoveAt(index);
                    LRIGDeck.RemoveAt(index);
                    break;
            }
        }

        public void AddToDeck(WixossCard card, DeckType deckType)
        {
            switch ( deckType )
            {
                case DeckType.Main:
                    MainDeck.Add(card);
                    break;
                case DeckType.LRIG:
                    LRIGDeck.Add(card);
                    break;
                case DeckType.Both:
                    MainDeck.Add(card);
                    LRIGDeck.Add(card);
                    break;
            }
        }
        
        public static bool cardAllowedInMainDeck(WixossCard cardToCheck)
        {
            bool cardAllowed = true;

            if ( cardToCheck.Type == WixossCard.CardType.ARTS )
                cardAllowed = false;
            else if ( cardToCheck.Type == WixossCard.CardType.LRIG )
                cardAllowed = false;
            else if ( cardToCheck.Type == WixossCard.CardType.Resona )
                cardAllowed = false;

            return cardAllowed;
        }

        public static bool cardAllowedILRIGDeck(WixossCard cardToCheck)
        {
            bool cardAllowed = true;

            if ( cardToCheck.Type == WixossCard.CardType.SIGNI )
                cardAllowed = false;
            else if ( cardToCheck.Type == WixossCard.CardType.Spell )
                cardAllowed = false;

            return cardAllowed;
        }

        public bool canAddCard(WixossCard cardToAdd , DeckType deckType)
        {
            //Common Rule no more than 4 per card
            if ( totalCountOfCard(cardToAdd , deckType) == 4 )
                return false;

            if ( deckType == DeckType.LRIG ) //lrig special rules
            {
                if ( LRIGDeck.Count == 10 )
                    return false;

                if ( !cardAllowedILRIGDeck(cardToAdd) )
                {
                    return false;
                }
            } else if(deckType == DeckType.Main)// normal deck special rules
            {
                if ( MainDeck.Count == 40 )
                    return false;

                if ( !cardAllowedInMainDeck(cardToAdd) )
                {
                    return false;
                }

                if ( cardToAdd.LifeBurst && totalCountOfLifeBurst() == 20 )
                {
                    return false;
                }
            }

            return true;
        }

        public static void SaveDeck(String deckName, WixossDeck deck)
        {
            if (!Directory.Exists(CardCollection.deckBasePath + deckName))
            {
                String filePath = CardCollection.deckBasePath + deckName + ".json";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                else
                {
                    File.Delete(filePath);
                }

                String jsonDeck = Newtonsoft.Json.JsonConvert.SerializeObject(deck, Newtonsoft.Json.Formatting.Indented);
                StreamWriter writer = File.CreateText(filePath);

                writer.WriteLine(jsonDeck);

                writer.Close();
            }
        }

        public static WixossDeck LoadDeck(string deckName)
        {
            String filePath = CardCollection.deckBasePath + deckName + ".json";
            String wixDeck = "";
            WixossDeck loadedDeck = new WixossDeck();

            StreamReader reader = File.OpenText(filePath);
            wixDeck = reader.ReadToEnd();
            reader.Close();

            loadedDeck = (WixossDeck)Newtonsoft.Json.JsonConvert.DeserializeObject(wixDeck, typeof(WixossDeck));

            return loadedDeck;
        }
    }

    public enum DeckType
    {
        Main,
        LRIG,
        Both
    }
}
