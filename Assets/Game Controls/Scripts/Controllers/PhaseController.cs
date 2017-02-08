using UnityEngine;
using System.Collections;
using Assets.Game_Controls.Scripts;
using UnityEngine.UI;
using Assets.Utils;

public class PhaseController : MonoBehaviour {

	public GameObject StandPhase;
	public GameObject DrawPhase;
	public GameObject ClockPhase;
	public GameObject MainPhase;
	public GameObject ClimaxPhase;
	public GameObject AttackPhase;
	public GameObject EncorePhase;
	public GameObject EndPhase;

	public CardController cardController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(int intValueGamePhase)
	{
		if ( !Constants.isMyTurn )
			return;

		GamePhase newGamePhase = GamePhase.AttackPhase;
		GamePhase lastGamePhase = GamePhaseCounter.lastPhase;

		switch (intValueGamePhase)
		{
			case 0:
				newGamePhase = GamePhase.FirstTurn;
				break;
			case 1:
				newGamePhase = GamePhase.StandPhase;
				break;
			case 2:
				newGamePhase = GamePhase.DrawPhase;
				break;
			case 3:
				newGamePhase = GamePhase.ClockPhase;
				break;
			case 4:
				newGamePhase = GamePhase.MainPhase;
				break;
			case 5:
				newGamePhase = GamePhase.ClimaxPhase;
				break;
			case 6:
				newGamePhase = GamePhase.AttackPhase;
				break;
			case 7:
				newGamePhase = GamePhase.EncorePhase;
				break;
			case 8:
				newGamePhase = GamePhase.EndPhase;
				break;
			default:
				break;
		}

		cardController.UpdateGamePhase(newGamePhase);
	} 

	public static void ChangePhase(GamePhase newGamePhase, GamePhase lastGamePhase)
	{
		ChangeBtn(newGamePhase, true);

		if (newGamePhase != lastGamePhase)
			ChangeBtn(lastGamePhase, false);
	}

	private static void ChangeBtn(GamePhase gamePhase, bool enable)
	{
		PhaseController phaseBtns = GameObject.FindGameObjectWithTag("PhaseCounter").GetComponent<PhaseController>();
		Button buttonToChange = null;
		Color newColor = new Color();

		if ( enable )
			newColor = Constants.isMyTurn ? hexToColor("E7FF58FF") : hexToColor("993333");
		else
			newColor = hexToColor("BBBCBCFF");

		switch (gamePhase)
		{
			case GamePhase.StandPhase:
				buttonToChange = phaseBtns.StandPhase.GetComponent<Button>();
				break;
			case GamePhase.DrawPhase:
				buttonToChange = phaseBtns.DrawPhase.GetComponent<Button>();
				break;
			case GamePhase.ClockPhase:
				buttonToChange = phaseBtns.ClockPhase.GetComponent<Button>();
				break;
			case GamePhase.MainPhase:
				buttonToChange = phaseBtns.MainPhase.GetComponent<Button>();
				break;
			case GamePhase.ClimaxPhase:
				buttonToChange = phaseBtns.ClimaxPhase.GetComponent<Button>();
				break;
			case GamePhase.AttackPhase:
				buttonToChange = phaseBtns.AttackPhase.GetComponent<Button>();
				break;
			case GamePhase.EncorePhase:
				buttonToChange = phaseBtns.EncorePhase.GetComponent<Button>();
				break;
			case GamePhase.EndPhase:
				buttonToChange = phaseBtns.EndPhase.GetComponent<Button>();
				break;
			default:
				break;
		}

		if(buttonToChange != null)
		{
			buttonToChange.image.color = newColor;
		}
	}

	private static Color hexToColor(string hex) //http://answers.unity3d.com/questions/812240/convert-hex-int-to-colorcolor32.html
	{
		hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
		hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
		byte a = 255;//assume fully visible unless specified in hex
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		//Only use alpha if the string has enough characters
		if (hex.Length == 8)
		{
			//a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, a);
	}
}
