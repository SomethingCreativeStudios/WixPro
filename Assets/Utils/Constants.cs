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
