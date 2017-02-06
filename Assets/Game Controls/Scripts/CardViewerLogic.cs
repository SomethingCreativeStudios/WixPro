using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets.Utils;

public class CardViewerLogic : MonoBehaviour {

	public GameObject cardIamge, cardName, cardPower, cardLevel, cardClass, cardColor, cardTiming, cardLifeBurst, cardCost, cardType, cardEffects;
	public Text textIamge, textName, textPower, textLevel, textClass, textColor, textTiming, textLifeBurst, textCost, textType, textEffects;
	// Use this for initialization
	void Start () {
		textName = cardName.GetComponent<Text>();
		textPower = cardPower.GetComponent<Text>();
		textLevel = cardLevel.GetComponent<Text>();
		textClass = cardClass.GetComponent<Text>();
		textColor = cardColor.GetComponent<Text>();
		textTiming = cardTiming.GetComponent<Text>();
		textLifeBurst = cardLifeBurst.GetComponent<Text>();
		textCost = cardCost.GetComponent<Text>();
		textType = cardType.GetComponent<Text>();
		textEffects = cardEffects.GetComponent<Text>();

		WWW www = new WWW("file://" + Constants.noCardFound);
		Image rend = cardIamge.GetComponent<Image>();
		RectTransform rect = cardIamge.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(265 , 390);

		//rend.sprite = Sprite.Create(www.texture , new Rect(0 , 0 , www.texture.width , www.texture.height) , new Vector2(0.5f , 0.5f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCardData(WixossCard card)
	{
		string pathTemp = "sets/setimages/" + card.CardSet + "/" + card.CardNumberInSet;
		

		textName.text = card.CardName;
		textPower.text = "Power: " + card.Power;
		textPower.text += card.PowerBoost >= 0 ? " ( + " + card.PowerBoost + " )" : " ( " + card.PowerBoost + " )";
		textLevel.text = "Level: " + card.Level;
		textClass.text = "Class: " + card.getClassStr();
		textColor.text = "Color: " + card.getColorStr();
		textTiming.text = "Timing: " + card.getTimingStr();
		textLifeBurst.text = "LifeBurst: " + card.LifeBurst;
		textCost.text = "Cost: " + card.getCostStr();
		textType.text = "Type: " + card.Type;
		textEffects.text = "Effect: " + card.CardEffect;

		WWW www = new WWW("file://" + WixossCard.GetImagePath(card));
		Image rend = cardIamge.GetComponent<Image>();
		RectTransform rect = cardIamge.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(265, 390);

		rend.sprite = Sprite.Create(www.texture , new Rect(0 , 0 , www.texture.width , www.texture.height) , new Vector2(0.5f , 0.5f));


	}
}
