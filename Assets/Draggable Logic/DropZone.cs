using Assets.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum DropZoneType
    {
        Field,
        WaitingRoom,
        Memory,
        Stock,
        Hand,
        Deck,
        DialogBox
    }
    public DropZoneType zoneType = DropZoneType.Field;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            

            if (zoneType == DropZoneType.Field)
            {
                if (hasDraggableAlready())
                {
                    return;
                }
            }

            if (zoneType == DropZoneType.Field || zoneType == DropZoneType.Hand || zoneType == DropZoneType.DialogBox)
                d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            if (zoneType == DropZoneType.Field)
            {
                if (hasDraggableAlready())
                {
                    PlaceUnderCard(d, eventData.pointerDrag.GetComponent<WixCardComponent>().Card);
                    DestroyObject(eventData.pointerDrag);
                    return;
                }
            }

            if (zoneType == DropZoneType.Hand || zoneType == DropZoneType.Field || zoneType == DropZoneType.DialogBox)
            {
                if (d.parentToReturnTo != this.transform)//no double add
                {
                    WixossCard card = eventData.pointerDrag.GetComponent<WixCardComponent>().Card;
                    PoolViewerScript logic = gameObject.GetComponent<PoolViewerScript>();
                    PanelDragger dragger = d.parentToReturnTo.GetComponent<PanelDragger>();

                    if (logic != null)
                        logic.poolOfCards.Add(card);

                    if (dragger != null)
                    {
                        PoolViewerScript oldScript = dragger.realParent.GetComponent<PoolViewerScript>();

                        if (oldScript != null)
                            oldScript.poolOfCards.Remove(card);

                    }
                    else
                    {
                        PoolViewerScript oldScript = d.parentToReturnTo.GetComponent<PoolViewerScript>();
                        if (oldScript != null)
                            oldScript.poolOfCards.Remove(card);
                    }
                    
                    // Deal with networking
                    if ( logic != null && logic.cardController.sendRPC )
                    {
                        logic.cardController.getPhotonView().RPC(Constants.RPC_MoveCardShowCardToX , PhotonTargets.Others , card.CardId , ControllerHelper.GameObjectToLocation(d.parentToReturnTo.gameObject) , ControllerHelper.GameObjectToLocation(gameObject));
                    }
                }
            }
            else
            {
                if (d.parentToReturnTo != this.transform)//no double add
                {
                    WixossCard card = eventData.pointerDrag.GetComponent<WixCardComponent>().Card;
                    PoolViewerScript logic = gameObject.GetComponent<PoolViewerScript>();

                    if (logic != null)
                        logic.poolOfCards.Add(card);

                    PanelDragger dragger = d.parentToReturnTo.GetComponent<PanelDragger>();
                    if (dragger != null)
                    {
                        PoolViewerScript oldScript = dragger.realParent.GetComponent<PoolViewerScript>();

                        if (oldScript != null)
                            oldScript.poolOfCards.Remove(card);

                    }
                    else
                    {
                        PoolViewerScript oldScript = d.parentToReturnTo.GetComponent<PoolViewerScript>();
                        if (oldScript != null)
                            oldScript.poolOfCards.Remove(card);
                    }

                    // Deal with networking
                    if ( logic != null && logic.cardController.sendRPC )
                    {
                        logic.cardController.getPhotonView().RPC(Constants.RPC_MoveCardToX , PhotonTargets.Others , card.CardId , ControllerHelper.GameObjectToLocation(d.parentToReturnTo.gameObject) , ControllerHelper.GameObjectToLocation(gameObject));
                    }
                }
                DestroyObject(eventData.pointerDrag);
            }


            d.parentToReturnTo = this.transform;
        }

    }

    private bool hasDraggableAlready()
    {
        return (this.GetComponentsInChildren(typeof(Draggable)).Length == 0) ? false : true;
    }

    private void PlaceUnderCard(Draggable d, WixossCard card)
    {
        PanelDragger dragger = d.parentToReturnTo.GetComponent<PanelDragger>();
        SIGNIController signiController = this.GetComponent<SIGNIController>();
        PoolViewerScript oldScript = null;
        if (dragger != null)
        {
            oldScript = dragger.realParent.GetComponent<PoolViewerScript>();
        } else
        {
            oldScript = d.parentToReturnTo.GetComponent<PoolViewerScript>();
        }

        signiController.poolOfCards.Add(card);
        oldScript.poolOfCards.Remove(card);
    }
}