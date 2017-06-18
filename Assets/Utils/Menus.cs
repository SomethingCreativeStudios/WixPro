using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using CodeBureau;

namespace Assets.Utils
{
    #region Hand Menu
    public enum HandMenuMainPhase
    {
        [StringValue("Send To X")]
        SendToX
    }

    public enum HandMenuClockPhase
    {
        [StringValue("Clock Card")]
        ClockCard,
        [StringValue("Move To Main Phase")]
        MoveToMainPhase
    }

    public enum HandMenuFirstTurn
    {
        [StringValue("Mulligan Card")]
        MulliganCard,
        [StringValue("Skip Mulligan Card")]
        SkipMulliganCard
    }
    #endregion

    public enum DeckMenu
    {
        [StringValue("Draw Card")]
        DrawCard,
        [StringValue("Mill Top Card")]
        MillTopCard,
        [StringValue("Shuffle Deck")]
        ShuffleDeck,
        [StringValue("View Deck")]
        ViewDeck
    }

    public enum SigniMenu
    {
        
        [StringValue("Attack")]
        Attack,
        [StringValue("Down")]
        Down,
        //[StringValue("Power Up 500")]
        //PowerUp500,
        [StringValue("Power Up X")]
        PowerUpX,
        [StringValue("Power Down X")]
        PowerDownX,
        [StringValue("View Cards")]
        ViewCards
    }

    public enum CoinMenu
    {
        [StringValue("Add Coin")]
        AddCoin,
        [StringValue("Subtract Coin")]
        SubtractCoin        
    }
    public enum LRIGMenu
    {
        
        [StringValue("Attack")]
        Attack,
        [StringValue("Down")]
        Down,
        [StringValue("View Cards")]
        ViewCards
    }

    public enum LRIGTrashMenu
    {
        [StringValue("View Cards")]
        ViewCards
    }

    public enum LifeClothMenu
    {
        [StringValue("View Cards")]
        ViewCards,
        [StringValue("Remove Top")]
        RemoveTop
    }

    public enum EnerMenu
    {
        
        [StringValue("Pay 1")]
        PayOne,
        [StringValue("Pay 2")]
        PayTwo,
        [StringValue("Pay 3")]
        PayThree,
        [StringValue("Pay X")]
        PayX,
        [StringValue("View Cards")]
        ViewCards
    }

    public enum OpponentMenu
    {
        [StringValue("View Cards")]
        ViewCards
    }

    public enum TrashMenu
    {
        [StringValue("View Cards")]
        SendToX
    }

    public enum SendToMenu
    {
        [StringValue("Send To Trash")]
        SendToTrash,
        [StringValue("Shuffle Into Deck")]
        ShuffleIntoDeck,
        [StringValue("Send To (T)Deck")]
        SendToTopOfDeck,
        [StringValue("Send To (B)Deck")]
        SendToBottomOfDeck,
        [StringValue("Send To Hand")]
        SendToHand
    }
    public enum LRIGDeckMenu
    {
        [StringValue("View Cards")]
        ViewCards
    }

    public class MenuHelper
    {
        /// <summary>
        /// Converts enum to string array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] MenuToArray<T>()
        {
            List<String> listOfMenuItems = new List<string>();
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if ( enumType.BaseType != typeof(Enum) )
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            StringEnum stringEnum = new StringEnum(typeof(T));
            foreach ( T val in enumValArray )
            {
                listOfMenuItems.Add(stringEnum.GetStringValue(val.ToString()));
            }

            return listOfMenuItems.ToArray();
        }
    }
}
