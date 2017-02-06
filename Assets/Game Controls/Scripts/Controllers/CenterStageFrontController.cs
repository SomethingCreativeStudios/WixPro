using UnityEngine;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Game_Controls.Scripts.Enums;
using Assets.Game_Controls.Scripts;

public class CenterStageFrontController : PoolViewerScript
{
    bool attackPowerModFlag = false;
    int attackPowerModValue = 0;

   // AttackService attackService;
    
    bool soulModFlag = false;
    int soulModValue = 0;

    int locationSet;
    // Use this for initialization
    public override void StartUp()
    {
        menuItems = Constants.centerStageFrontMenu;
        //attackService = new AttackService();
    }
    public int locationValue(Location location)
    {
        switch (location)//may need to create OP values with switch on left right values
        {
            case Location.FrontStageLeft:
                return 0;
            case Location.FrontStageCenter:
                return 1;
            case Location.FrontStageRight:
                return 2;
            case Location.BackStageLeft:
                return 3;
            case Location.BackStageRight:
                return 4;
            default:
                return 7;
        }
    }
    // Update is called once per frame
    public override void UpdateScript()
    {
        if (attackPowerModFlag)
        {
            cardController.ModCardPower(this.gameObject.GetComponentInChildren<WixCardComponent>() , attackPowerModValue);
            attackPowerModFlag = false;
        }

        if (soulModFlag)
        {
            cardController.ModCardSoul(this.gameObject.GetComponentInChildren<WixCardComponent>() , soulModValue);
            soulModFlag = false;
        }

        if(GamePhaseCounter.currentPhase == GamePhase.StandPhase )
        {
            //Stand every card, we also do backrow cause why not?
            foreach ( WixCardComponent WixossCardComponent in cardController.GetCardsOnMyField() )
            {
                if(WixossCardComponent != null )
                {
                    cardController.ChangeCardState(WixossCardComponent , CardState.Standing);
                }
            }
        }
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
            List<string> viewMenu = new List<string>(Constants.centerStageFrontMenu);
            menuItems = viewMenu.ToArray();
            menu.SetUpContextMenu(viewMenu);//new List<string>(menuItems));
            menu.clicked += Menu_clicked;
            contextMenu.name = "Pool Viewer Context Menu";
            //poolViewer.AddComponent<CanvasRenderer>();
            contextMenu.transform.SetParent(parentCanvas, false);
            contextMenu.transform.position = Input.mousePosition;
        }
    }

    public override void Menu_clicked(string menuName)
    {
        WixCardComponent cardComponent = this.GetComponentInChildren<WixCardComponent>();
        if (cardComponent != null) // make sure it has a card
        {
            switch (menuName)
            {
                case "Attack":                    
                    locationSet = locationValue(location);
                    if(locationSet == 0)
                    {                        
                       /* WixossCard playerCard = this.poolOfCards[0];
                        WixCardComponent OpCardComp = cardController.GetCardsOnMyOpField()[locationSet];
                        WixCardComponent playerCardComp = cardController.GetCardsOnMyField()[locationSet];
                        int playerSoul = playerCard.TrueSoul;
                        if (playerCard.StateOfCard == CardState.Standing)
                        {
                            if (playerCard != null)
                            {
                                if (OpCardComp == null)
                                {
                                    int soulBoost = Convert.ToInt32(playerCard.SoulBoost);
                                    soulBoost++;
                                    playerCard.SoulBoost = soulBoost.ToString();
                                }
                                else
                                {
                                    WixossCard OpCard = OpCardComp.Card;
                                    int playerAttack = playerCard.TruePower;
                                    int opAttack = OpCard.TruePower;

                                    if (playerAttack > opAttack) // player stronger
                                    {
                                        cardController.ChangeCardState(OpCardComp, CardState.Reversed);
                                        cardController.ChangeCardState(playerCardComp, CardState.Rest);
                                    }
                                    else if (playerAttack < opAttack) // op stronger
                                    {
                                        cardController.ChangeCardState(OpCardComp, CardState.Rest);
                                        cardController.ChangeCardState(playerCardComp, CardState.Reversed);
                                    }
                                    else // equal
                                    {
                                        cardController.ChangeCardState(OpCardComp, CardState.Reversed);
                                        cardController.ChangeCardState(playerCardComp, CardState.Reversed);
                                    }
                                }
                            }                           
                            //attackService.DamageStep(playerSoul);
                        }
                        else
                        {
                            //Log input cannot attack
                            Debug.Log("Cannot Attack!");
                        }*/
                    }
                    else if(locationSet == 1)
                    {

                    }
                    else
                    {

                    }
                    break;
                case "Side Attack":
                    if (locationSet == 0)
                    {

                    }
                    else if (locationSet == 1)
                    {

                    }
                    else
                    {

                    }
                    break;
                case "Send To X":
                    DialogScripts.ShowDropDownDialog(new List<string>(Constants.sendToMenu), "Memory", "Move your card.", (selectedItem) => { sendToX(selectedItem); }, null);
                    break;
                case "Stand Card":
                    cardController.ChangeCardState(cardComponent , CardState.Standing);
                    break;
                case "Rest Card":
                    cardController.ChangeCardState(cardComponent , CardState.Rest);
                    break;
                case "Reverse Card":
                    cardController.ChangeCardState(cardComponent , CardState.Reversed);
                    break;
                case "Soul Modding":
                    DialogScripts.ShowNumDialog("Please select soul boost", 1, (selectedItem) => { soulModFlag = true; soulModValue = Convert.ToInt32(selectedItem); });
                    break;
                case "Attack Power":
                    DialogScripts.ShowNumDialog("Please select attack power boost", 100, (selectedItem) => { attackPowerModFlag = true; attackPowerModValue = Convert.ToInt32(selectedItem); });
                    break;
            }
        }
    }
}
