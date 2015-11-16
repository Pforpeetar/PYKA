using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class StartOptions : MonoBehaviour {

	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels
	public bool changeScenes;	
	
	[HideInInspector] public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
	[HideInInspector] public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes

	public void StartButtonClicked()
	{
		//If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
		if (changeScenes) {
			//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
			Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);
			
			//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
			animColorFade.SetTrigger ("fade");
		} else {
			Application.LoadLevel (2);
		}
	}

		public void LoadDelayed()
		{			
			//Hide the main menu UI element
		showPanels.HideMenu ();
			
			//Load the selected scene, by scene index number in build settings
			Application.LoadLevel (2);
		}

		public void 
}
