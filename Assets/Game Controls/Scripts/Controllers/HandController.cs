using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using Assets.Game_Controls.Scripts;
using Assets.Game_Controls.Scripts.Enums;

public class HandController : PoolViewerScript
{
    private List<WixossCard> cardsBeingMulligend;
    private bool needToMulligenCards = false;

    // Use this for initialization
    public override void StartUp()
    {
        menuItems = Constants.handMenuMainPhase;
    }

    // Update is called once per frame
    public override void UpdateScript()
    {
        //Can Happen On Same Turn For Both Players
        if ( GamePhaseCounter.currentPhase == GamePhase.FirstTurn )
        {
            menuItems = Constants.handMenuFirstTurn;

            if ( !Constants.hasMulligenedCards && needToMulligenCards )
            {
                if ( cardsBeingMulligend != null )
                {
                    if ( cardsBeingMulligend.Count != 0 )
                    {
                        List<GameObject> gameObjects = new List<GameObject>();
                        List<string> cardsBeingMovedIds = new List<string>();

                        foreach ( var card in cardsBeingMulligend )
                        {
                            gameObjects.Add(AddWixossCard(card , false));
                            cardsBeingMovedIds.Add(card.CardId);
                        }

                        cardController.MoveCards(gameObjects , cardController.PlayerHand , cardController.PlayerDeck , 3);
                        cardController.ShufflePlayerDeck();

                        for ( int i = 0; i < cardsBeingMulligend.Count; i++ )
                        {
                            WixossCard cardBeingMoved = cardController.getPlayerDeckController().poolOfCards[i];
                            cardController.MoveCardShowCard(cardBeingMoved , cardController.PlayerDeck , cardController.PlayerHand, i);
                        }

                        Constants.hasMulligenedCards = true;
                        cardController.SendFlagToOp("OtherPlayerHasMulligend" , true);

                        if ( !cardController.sendRPC )
                            Constants.OtherPlayerHasMulligend = true;
                    }
                }

            }
        }

        #region My Turn
        if ( Constants.isMyTurn )
        {
            if ( GamePhaseCounter.currentPhase == GamePhase.FirstTurn )
            {
                //Only turn player has the power to move game state
                if ( Constants.OtherPlayerHasMulligend && Constants.hasMulligenedCards )
                    cardController.UpdateGamePhase(GamePhase.DrawPhase);
            }
            if ( GamePhaseCounter.currentPhase == GamePhase.ClockPhase )
            {
                menuItems = Constants.handMenuClockPhase;

                if ( Constants.hasClockedCard )
                    cardController.UpdateGamePhase(GamePhase.MainPhase);
            }
            if ( GamePhaseCounter.currentPhase == GamePhase.MainPhase )
            {
                menuItems = Constants.handMenuMainPhase;
            }
        }
        #endregion

        #region Op Turn
        if ( !Constants.isMyTurn )
        {
            if ( GamePhaseCounter.currentPhase == GamePhase.ClockPhase )
            {
                menuItems = new string[0]; //no menu for you
            }
        }
        #endregion
    }


    public override void MenuClicked()
    {
        if (Input.GetMouseButtonDown(0) && canBeViewed)
        {
            GameObject poolViewer = (GameObject)Instantiate(Resources.Load("PoolViewer"));
            poolViewer.name = gameObject.GetComponent<DropZone>().zoneType.ToString() + " Viewer";
            PanelDragger dragger = poolViewer.GetComponent<PanelDragger>();
            dragger.poolOfCards = new List<WixossCard>(poolOfCards);
            //poolViewer.AddComponent<CanvasRenderer>();
            dragger.realParent = gameObject;
            poolViewer.transform.SetParent(parentCanvas.transform, false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject contextMenu = (GameObject)Instantiate(Resources.Load("ContextMenu"));
            ContextMenuScript menu = contextMenu.GetComponent<ContextMenuScript>();
            List<string> viewMenu = new List<string>(menuItems);
            menuItems = viewMenu.ToArray();
            menu.SetUpContextMenu(viewMenu);//new List<string>(menuItems));
            menu.clicked += Menu_clicked;
            contextMenu.name = "Pool Viewer Context Menu";
            //poolViewer.AddComponent<CanvasRenderer>();
            contextMenu.transform.SetParent(parentCanvas, false);
            contextMenu.transform.position = Input.mousePosition;
        }
    }

    public IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
    }

    public override void Menu_clicked(string menuName)
    {
        #region Main Phase
        if (menuName == Constants.handMenuMainPhase[0]) //Send to X
        {
            DialogScripts.ShowDropDownDialog(new List<string>(Constants.sendToMenu), "Memory", "Move your card.", (selectedItem) => { sendToX(selectedItem); }, null);
        }
        #endregion

        #region Clock Phase
        if (menuName == Constants.handMenuClockPhase[0]) // Clock Card
        {
            GameObject cardUnderMouse = CardUnderMouse();
            WixossCard wixossCardUnderMouse = WixossCardUnderMouse();

            cardController.MoveCard(cardUnderMouse, cardController.PlayerHand, cardController.Clock);
            
            for (int i = 0; i < 2; i++)
            {
                WixossCard WixossCard = cardController.getPlayerDeckController().poolOfCards[i];
                cardController.RefreshDeck();
                cardController.MoveCardShowCard(WixossCard , cardController.PlayerDeck, cardController.PlayerHand, i);
                cardController.RefreshDeck();
            }

            cardController.UpdateGamePhase(GamePhase.MainPhase);
            Constants.hasClockedCard = true;
        }

        if (menuName == Constants.handMenuClockPhase[1]) // Move On To Main Phase
        {
            cardController.UpdateGamePhase(GamePhase.MainPhase);
            Constants.hasClockedCard = true;
        }
        #endregion

        #region Mulligan Phase
        if (menuName == Constants.handMenuFirstTurn[0]) // Mulligan Card
        {
            DialogScripts.ShowCardDialog("Mulligan Time", "Place any cards you want to mulligan here and press OK.", (mulliganedCards) => { MulliganCards(mulliganedCards); }, null);
        }
        if ( menuName == Constants.handMenuFirstTurn[1] ) // Skip Mulligan Card
        {
            Constants.hasMulligenedCards = true;
            cardController.SendFlagToOp("OtherPlayerHasMulligend" , true);

            if ( !cardController.sendRPC )
                Constants.OtherPlayerHasMulligend = true;
        }
        #endregion
    }

    public void MulliganCards(List<WixossCard> cardsBeingMulliganed)
    {
        needToMulligenCards = true;

        if (cardsBeingMulliganed != null)
            this.cardsBeingMulligend = new List<WixossCard>(cardsBeingMulliganed);
    }
}
