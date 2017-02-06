using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Utils
{
    class Constants
    {
        #region File Path Constants
        public static String cardBack = CardCollection.setImages + "general\\Card Back.jpg";
        public static String noCardFound = CardCollection.setImages + "general\\No Card Found.jpg";
        #endregion

        #region Menu Constants
        public static String[] handMenuMainPhase = new string[] { "Send to X" };
        public static String[] handMenuClockPhase = new string[] { "Clock card" , "Move To Main Phase" };
        public static String[] handMenuFirstTurn = new string[] { "Mulligan Card", "Skip Mulligan Card" };
        public static String[] deckMenu = new string[] { "Draw Card" , "Mill Top Card" , "Shuffle Deck" , "View Deck" , "Send to Stock" };
        public static String[] centerStageFrontMenu = new string[] { "Attack" , "Side Attack" , "Rest Card" , "Stand Card" , "Reverse Card" , "Soul Modding" , "Attack Modding" , "Send To X" };// ^ for Send to menus for drop down box
        public static String[] centerStageBackMenu = new string[] { "Rest Card" , "Stand Card" , "Send To X" };
        public static String[] oppMenu = new string[] { "View Cards" };
        public static String[] memoryMenu = new string[] { "Send to X" };// Done
        public static String[] stockMenu = new string[] { "Pay 1" , "Pay 2" , "Pay X", "Pay Encore" };
        public static String[] clockMenu = new string[] { "Send To X" }; // Done
        public static String[] levelMenu = new string[] { "Swap" };
        public static String[] climaxMenu = new string[] { "Remove Card" };
        public static String[] eventMenu = new string[] { "Remove Card" };
        public static String[] waitRoomMenu = new string[] { "Send To X" }; // Send to X will bring up drop down menu (Done)
        public static String[] sendToMenu = new string[] { "Send To Waiting Room" , "Shuffle into Deck" , "Send to Stock" , "(T)Send To Deck" , "(B)Send To Deck" , "Send To Clock" , "Send to Hand" , "Send To Memory" };
        #endregion

        #region Network Constants
        /// <summary>
        /// CardId, LocationTo, LocationFrom
        /// </summary>
        public static string RPC_MoveCardToX = "RPCMoveCardToX";

        /// <summary>
        /// CardId, LocationTo, LocationFrom
        /// </summary>
        public static string RPC_MoveCardShowCardToX = "RPCMoveCardShowCardToX";

        /// <summary>
        /// List<> CardIds, LocationTo, LocationFrom
        /// </summary>
        public static string RPC_MoveCardsToX = "RPCMoveCardsToX";

        /// <summary>
        /// List<> CardIds
        /// </summary>
        public static string RPC_SetOPDeck = "RPCSetOPDeck";

        /// <summary>
        /// No parameters, Draw hand, and start duel
        /// </summary>
        public static string RPC_StartDuel = "StartDuel";

        /// <summary>
        /// GamePhase newGamePhase
        /// </summary>
        public static string RPC_UpdateGameState = "RPCUpdateGameState";

        /// <summary>
        /// String flagName, bool flagValue
        /// </summary>
        public static string RPC_SendFlag = "RPCSendFlag";

        /// <summary>
        /// CardState newCardState, Location cardLocation
        /// </summary>
        public static string RPC_ChangeCardState = "RPCChangeCardState";

        /// <summary>
        /// int attackModValue, Location cardLocation
        /// </summary>
        public static string RPC_ModCardPower = "RPCModCardPower";

        /// <summary>
        /// int soulModValue, Location cardLocation
        /// </summary>
        public static string RPC_ModCardSoul = "RPCModCardSoul";
        #endregion

        #region Game State Constants

        /**********************START ONE TIME FLAGS************/

        public static bool OtherPlayerHasMulligend = false;
        public static bool isFirstTurnOfDuel = true;

        /**********************END ONE TIME FLAGS**************/

        /***********************START TURN CONSTANTS***********/
        public static bool isMyTurn = true;
        public static bool hasMulligenedCards = false;
        public static bool hasDrawnCard = false;
        public static bool hasClockedCard = false;

        /***********************END TURN CONSTANTS*************/
        #endregion
    }
}
