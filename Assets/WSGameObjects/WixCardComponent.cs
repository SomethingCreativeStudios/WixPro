using Assets.Game_Controls.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WixCardComponent : MonoBehaviour, IPointerEnterHandler
{

    [HideInInspector]
    public WixossCard Card { get; set; }

    [HideInInspector]
    public Vector2 CurrentPos { get; set; }

    [HideInInspector]
    public Vector2 TargetPos { get; set; }

    [HideInInspector]
    public Boolean MoveCardFlag { get; set; }

    [HideInInspector]
    public Boolean destoryCardAfterMove = true;

    [HideInInspector]
    public Boolean flipCardOnMove = false;

    [HideInInspector]
    public float cardSpeed { get; set; }

    public Sprite cardBack;

    public Sprite tempSprite = null;

    private Boolean rotateFlag = false;
    private Vector3 newRotation = new Vector3();
    

    public Boolean faceUp
    {
        get
        {
            return tempSprite == null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardViewerLogic cardLogic = GameObject.FindGameObjectWithTag("CardViewer").GetComponent<CardViewerLogic>();
        cardLogic.setCardData(WixossCard.Clone(Card));
    }

    /// <summary>
    /// This will trigger the moveCard event.
    /// </summary>
    /// <param name="destoryAfterMove">Should we destory the gameObject after we moved it?</param>
    /// <param name="cardSpeed">How fast the card should move?</param>
    public void StartMove(Boolean destoryAfterMove, bool flipCardOnMove, float cardSpeed = 1f)
    {
        MoveCardFlag = true;
        this.destoryCardAfterMove = destoryAfterMove;
        this.cardSpeed = cardSpeed;
        this.flipCardOnMove = flipCardOnMove;
    }

    /// <summary>
    /// This is "flip" the card and show either the face image or back image based on FaceUp
    /// </summary>
    public void FlipCard()
    {
        Image rend = gameObject.GetComponent<Image>();
        if (Card.FaceUp)
        {
            Card.FaceUp = false;
            tempSprite = rend.sprite;
            rend.sprite = cardBack;
        }
        else
        {
            Card.FaceUp = true;
            rend.sprite = tempSprite;
        }
    }

    void Update()
    {
        if (MoveCardFlag)
        {
            if (CurrentPos != TargetPos)
            {
                Vector3 tempTargetPos = Vector3.MoveTowards(CurrentPos, TargetPos, cardSpeed);
                CurrentPos = new Vector2(tempTargetPos.x, tempTargetPos.y);
                gameObject.transform.position = CurrentPos;
                if(CurrentPos == TargetPos ) //Stupid thing to force the layout to refresh
                {
                    HorizontalLayoutGroup group = this.gameObject.GetComponentInParent<HorizontalLayoutGroup>();
                    if (group != null)
                    {
                        group.childAlignment = TextAnchor.LowerCenter;
                        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.GetComponentInParent<RectTransform>());
                    }
                    if ( flipCardOnMove )
                        FlipCard();
                }
            }
            else
            {
                MoveCardFlag = false;
                if (destoryCardAfterMove)
                    DestroyObject(gameObject);
            }

        }

        if (rotateFlag)
        {
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            long currentRoation = (long)Math.Round(rectTransform.rotation.eulerAngles.z);
            if (currentRoation != newRotation.z)
            {
                if (newRotation.z > currentRoation)
                    rectTransform.Rotate(0, 0, 5);
                else if (newRotation.z < currentRoation)
                    rectTransform.Rotate(0, 0, -5);
            }
            else
            {
                rotateFlag = false;
            }
        }
    }

    /// <summary>
    /// Change the state of the card, also handles rotation
    /// </summary>
    /// <param name="newState">The new state of card</param>
    public void ChangeState(CardState newState)
    {
        if (rotateFlag || Card.StateOfCard == newState)
            return;

        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        Card.StateOfCard = newState;
        Vector3 newRotation = new Vector3();
        newRotation = transform.position;
        switch (newState)
        {
            case CardState.Rest:
                newRotation.z = 90;
                break;
            case CardState.Standing:
                newRotation.z = 0;
                break;
            case CardState.Reversed:
                newRotation.z = 180;
                break;
            default:
                break;
        }
        
        rotateFlag = true;
        this.newRotation = newRotation;
    }

    /// <summary>
    /// Change the state of the card, also handles rotation
    /// </summary>
    /// <param name="newState">The new state of card</param>
    public void ChangeStateOP(CardState newState)
    {
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        Card.StateOfCard = newState;
        Vector3 newRotation = new Vector3();
        newRotation = transform.position;
        switch ( newState )
        {
            case CardState.Rest:
                newRotation.z = 270;
                break;
            case CardState.Standing:
                newRotation.z = 180;
                break;
            case CardState.Reversed:
                newRotation.z = 0;
                break;
            default:
                break;
        }

        rotateFlag = true;
        this.newRotation = newRotation;
    }

    void Start()
    {
    }
}
