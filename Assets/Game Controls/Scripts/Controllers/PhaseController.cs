using UnityEngine;
using System.Collections;
using Assets.Game_Controls.Scripts;
using UnityEngine.UI;
using Assets.Utils;
using System;

public class PhaseController : MonoBehaviour {

	public GameObject StandPhase;
	public GameObject DrawPhase;
    public GameObject EnerPhase;
    public GameObject GrowPhase;
    //public GameObject ClockPhase;
	public GameObject MainPhase;
	//public GameObject ClimaxPhase;
	public GameObject AttackPhase;
    //public GameObject EncorePhase;
    public GameObject ARTSUseStep;
    public GameObject SigniAttackStep;
    public GameObject LRIGAttackStep;
	public GameObject EndPhase;
    public GameObject GuardStep;

	public CardController cardController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(int intValueGamePhase)
    //Currently Pass in value of Button Clicked should change to take current phase and on end turn change button to opp turn
    {
        if ( !Constants.isMyTurn )
			return;

		GamePhase curGamePhase = GamePhaseCounter.currentPhase;
		GamePhase lastGamePhase = GamePhaseCounter.lastPhase;

		switch (curGamePhase)
		{
			case GamePhase.FirstTurn:
				curGamePhase = GamePhase.FirstTurn;
				break;
			case GamePhase.OppPhase:
				curGamePhase = GamePhase.StandPhase;
				break;
			case GamePhase.StandPhase:
				curGamePhase = GamePhase.DrawPhase;
				break;
			case GamePhase.DrawPhase:
				curGamePhase = GamePhase.EnerPhase;
				break;
			case GamePhase.GrowPhase:
				curGamePhase = GamePhase.MainPhase;
				break;
			case GamePhase.EnerPhase:
				curGamePhase = GamePhase.GrowPhase;
				break;
			case GamePhase.MainPhase:
				curGamePhase = GamePhase.AttackPhase;
				break;
			case GamePhase.ARTSUseStep:
				curGamePhase = GamePhase.SigniAttackStep;
				break;
		    case GamePhase.GuardStep:
				curGamePhase = GamePhase.EndPhase;
				break;
            case GamePhase.AttackPhase:
                curGamePhase = GamePhase.ARTSUseStep;
                break;
            case GamePhase.SigniAttackStep:
                curGamePhase = GamePhase.LRIGAttackStep;
                break;
            case GamePhase.LRIGAttackStep:
                curGamePhase = GamePhase.GuardStep;
                break;
            case GamePhase.EndPhase:
                curGamePhase = GamePhase.OppPhase;
                break;
			default:
				break;
		}

		cardController.UpdateGamePhase(curGamePhase);
	} 

	public static void ChangePhase(GamePhase newGamePhase, GamePhase lastGamePhase)
	{
		ChangeBtn(newGamePhase, true);

		if (newGamePhase != lastGamePhase)
			ChangeBtn(lastGamePhase, false);
	}

	private static void ChangeBtn(GamePhase gamePhase, bool enable)
	{
        PhaseController phaseBtns = null;
        try {
            phaseBtns = GameObject.FindGameObjectWithTag("PhaseCounter").GetComponent<PhaseController>();
        }catch (Exception ex )
        {
            Debug.logger.Log("Please add back the phase counter!!!! \n throws: " + ex);
            return;
        }

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
			case GamePhase.EnerPhase:
				buttonToChange = phaseBtns.EnerPhase.GetComponent<Button>();
				break;
			case GamePhase.MainPhase:
				buttonToChange = phaseBtns.MainPhase.GetComponent<Button>();
				break;
			case GamePhase.GrowPhase:
				buttonToChange = phaseBtns.GrowPhase.GetComponent<Button>();
				break;
			case GamePhase.AttackPhase:
				buttonToChange = phaseBtns.AttackPhase.GetComponent<Button>();
				break;
			case GamePhase.LRIGAttackStep:
				buttonToChange = phaseBtns.LRIGAttackStep.GetComponent<Button>();
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
