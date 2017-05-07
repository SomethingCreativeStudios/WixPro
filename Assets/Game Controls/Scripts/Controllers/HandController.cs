using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using Assets.Game_Controls.Scripts;
using Assets.Game_Controls.Scripts.Enums;
using CodeBureau;

public class HandController : PoolViewerScript
{
    private List<WixossCard> cardsBeingMulligend;
    private bool needToMulligenCards = false;

    // Use this for initialization
    public override void StartUp()
    {
        menuItems = MenuHelper.MenuToArray<HandMenuMainPhase>();
    }

    // Update is called once per frame
    public override void UpdateScript()
    {
        //Can Happen On Same Turn For Both Players
        if ( GamePhaseCounter.currentPhase == GamePhase.FirstTurn )
        {
            menuItems = MenuHelper.MenuToArray<HandMenuFirstTurn>();

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

                        cardController.MoveCards(gameObjects , ControllerHelper.FindGameObject(Location.Hand) , ControllerHelper.FindGameObject(Location.Deck) , 3);
                        cardController.ShufflePlayerDeck();

                        for ( int i = 0; i < cardsBeingMulligend.Count; i++ )
                        {
                            WixossCard cardBeingMoved = cardController.getPlayerDeckController().poolOfCards[i];
                            cardController.MoveCardShowCard(cardBeingMoved , ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Hand) , i);
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
                menuItems = MenuHelper.MenuToArray<HandMenuClockPhase>();

                if ( Constants.hasClockedCard )
                    cardController.UpdateGamePhase(GamePhase.MainPhase);
            }
            if ( GamePhaseCounter.currentPhase == GamePhase.MainPhase )
            {
                menuItems = MenuHelper.MenuToArray<HandMenuMainPhase>();
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
        if (menuName == GetMenuItem(HandMenuMainPhase.SendToX))
        {
            DialogScripts.ShowDropDownDialog(new List<string>(MenuHelper.MenuToArray<SendToMenu>()), "Memory", "Move your card.", (selectedItem) => { sendToX(selectedItem); }, null);
        }
        #endregion

        #region Clock Phase
        if ( menuName == GetMenuItem(HandMenuClockPhase.ClockCard) )
        {
            GameObject cardUnderMouse = CardUnderMouse();
            WixossCard wixossCardUnderMouse = WixossCardUnderMouse();

            cardController.MoveCard(cardUnderMouse, ControllerHelper.FindGameObject(Location.Hand) , ControllerHelper.FindGameObject(Location.TrashZone));
            
            for (int i = 0; i < 2; i++)
            {
                WixossCard WixossCard = cardController.getPlayerDeckController().poolOfCards[i];
                cardController.RefreshDeck();
                cardController.MoveCardShowCard(WixossCard , ControllerHelper.FindGameObject(Location.Deck) , ControllerHelper.FindGameObject(Location.Hand) , i);
                cardController.RefreshDeck();
            }

            cardController.UpdateGamePhase(GamePhase.MainPhase);
            Constants.hasClockedCard = true;
        }

        if ( menuName == GetMenuItem(HandMenuClockPhase.MoveToMainPhase) )
        {
            cardController.UpdateGamePhase(GamePhase.MainPhase);
            Constants.hasClockedCard = true;
        }
        #endregion

        #region Mulligan Phase
        if ( menuName == GetMenuItem(HandMenuFirstTurn.MulliganCard) )
        {
            DialogScripts.ShowCardDialog("Mulligan Time", "Place any cards you want to mulligan here and press OK.", (mulliganedCards) => { MulliganCards(mulliganedCards); }, null);
        }
        if ( menuName == GetMenuItem(HandMenuFirstTurn.SkipMulliganCard) )
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
