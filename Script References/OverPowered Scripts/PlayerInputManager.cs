using UnityEngine;
using System.Collections;

public class PlayerInputManager : OmonoPehaviour {
	public DPadButtons dPadScript;
	public PlayerAction pActionScript;
	bool pActionSet;
	private string HorizontalInput;
	private string VerticalInput;
	//private string LeftInput;
	//private string RightInput;
	//private string UpInput;
	private string JumpInput;
	private string DashInput;
	//private string DownInput;
	private string MeleeInput;
	private string RangeInput;
	private float dashInputDelay = 0.25f; //how long between double presses before it's no longer interpreted as a DashInput
	private float leftTime;
	private float rightTime;
	private int numOfPresses; //store the num of melee presses there were over a short time
	private int minPressesPerSequence = 4; //this number of consecutive presses to be considered a mash input
	private float lastPressTime; //store the last time melee button was pressed so we can check for mash air
	private float currPressTime;
	private float consecutivePressWindow = .25f; //if a melee press is > this time window, not considered consecutive
	public WhichCharacter whoAmI;  //have to set for respective players so the script can figure out which controls operate gameObject  

	// Use this for initialization
	protected override void OnStart () {
		if (pActionScript == null)
		{
			pActionSet = false;
		}
		else
		{
			pActionSet = true;
		}
		//should grab controls from PlayerData because character can be Player1 or Player2
		numOfPresses = 0;
		currPressTime = 0;
		lastPressTime = 0;
		PlayerData.GetControls(whoAmI,out HorizontalInput, out VerticalInput, out RangeInput, out MeleeInput, out JumpInput, out DashInput);
		dPadScript.SetAxisControls(HorizontalInput,VerticalInput);
		PlayerData.SetCharacterLevelInstance(gameObject,whoAmI);
		Utilities.SetDirectCharacterInstance (gameObject, whoAmI);
		Utilities.SendToListeners(new CharacterSpawnMessage(gameObject, OmonoPehaviour.ms_CHARSPAWNED,whoAmI));
	}

	protected override void OnDestroyOverride()
	{
		Utilities.SendToListeners(new CharacterDeathMessage(gameObject,OmonoPehaviour.ms_CHARDIED,whoAmI));
	}
	
	// Update is called once per frame
	void Update () {
		if (pActionSet) {
			float v = Input.GetAxis(VerticalInput);
			rightTime += Time.deltaTime;
			leftTime += Time.deltaTime;
			/*if (Input.GetButton(RightInput)) {
				pActionScript.HorizontalInput(1);
			}
			else if (Input.GetButton(LeftInput)) {
				pActionScript.HorizontalInput(-1);
			}
			else {
				pActionScript.HorizontalInput(0);
			}*/
			pActionScript.HorizontalInput(Input.GetAxis(HorizontalInput));
			
			if (dPadScript.RightPressed()) {
			//if (Input.GetButtonDown(RightInput)) {
			leftTime = 10f; //set left time to some high number, prevents quick left right left input being read as dash input
				if (rightTime < dashInputDelay)	{
					pActionScript.DashInput();
				}
				else {
					rightTime = 0f;
				}
			}
			else if (dPadScript.LeftPressed()) {
				//else if (Input.GetButtonDown(LeftInput)) {
				rightTime = 10f; //set right time to some high number, prevents quick right left right input being read as dash attempt
				if (leftTime < dashInputDelay) {
					pActionScript.DashInput();
				}
				else {
					leftTime = 0f;
				}
			}
			
			if (Input.GetButtonDown(DashInput))
			{
				pActionScript.DashInput();
			}

			if((v < -.1f) && Input.GetButtonDown(JumpInput))
			//if((Input.GetButton(DownInput)) && Input.GetButtonDown(JumpInput))
			{
				pActionScript.DropThrough();
			}
			else if (Input.GetButtonDown(JumpInput)) {
				//have to make this an else if so that player won't jump while dropping thru a platform
				pActionScript.JumpInput();
			}
			
			if ((v < -.1f) && Input.GetButtonDown(MeleeInput))
			//if (Input.GetButton(DownInput) && Input.GetButtonDown(MeleeInput))
			{
				pActionScript.DiveMeleeInput();
			}
			else if ((v > .1f) && Input.GetButtonDown(MeleeInput))
			{
				pActionScript.LaunchMeleeInput();
			}
			else if (Input.GetButtonDown(MeleeInput)) {
				pActionScript.MeleeInput();
			}
			/*
			 * Test for multiple mashe input on melee
			 */ 
			if (Input.GetButtonDown(MeleeInput))
			{
				currPressTime = Time.time; //compare last time the button was pressed to right now
				if (currPressTime - lastPressTime < consecutivePressWindow)
				{
					numOfPresses++; //if the num of presses is enouugh, trigger input read
					if (numOfPresses == minPressesPerSequence)
					{
						pActionScript.MashMeleeAirInput();
					}
				}
				else
				{
					numOfPresses = 1; //set that press to be the first press in new sequence
				}
				lastPressTime = Time.time; //update the new last time to compare next time the buttons is pressed
			}

			if (Input.GetButtonDown(RangeInput)) {
				pActionScript.RangeInput();
			}
		}
	}
}
