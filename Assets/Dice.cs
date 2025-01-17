using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {
	
	public int dice1 = 0;
	public int dice2 = 0;
	public int diceResult = 0;
	
	public int playTimes = 0;
	public int playAgainTimes = 0;
	
	public bool inJail=false;
	public int jailTimes=0;
	
	public Transform playerTransform;
	public Transform cameraTransform;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Box (new Rect(10,10,100,90), "Options Menu");
		GUI.Box (new Rect(10,130,120,20), "Dice 1: "+dice1);
		GUI.Box (new Rect(10,150,120,20), "Dice 2: "+dice2);
		GUI.Box (new Rect(10,170,120,20), "In Jail: "+inJail);	
		GUI.Box (new Rect(10,200,120,20), "Final Result: "+diceResult);	
		
	
	    // BUTTON - Roll Dice
		if (GUI.Button (new Rect(20,40,80,20), "Roll Dice")) {
			//set camera to follow piece
			MovePiece piece = (MovePiece)playerTransform.GetComponent(typeof(MovePiece));
			SmoothFollow follow = (SmoothFollow)cameraTransform.GetComponent(typeof(SmoothFollow));
			follow.target = piece.transform;
			
			// Check to see if player is in jail
	        if(!inJail){
				RollDice();     // Roll the dice
				piece.Move(diceResult);
	            // If doubles
				if(dice1 == dice2){
					playAgainTimes+=1;      // Add to combo of doubles
					Debug.Log("Double combo: " + playAgainTimes);
				}
	            // If 3 doubles in a row
				if(playAgainTimes == 3){
					Debug.Log("Sent to jail for 3 doubles");
					inJail=true;        // Send to jail
				}else if(dice1 != dice2){
					playAgainTimes=0;       // Reset combo
				}
			} else {
				RollDice();     // Roll the dice
	            // If doubles
				if(dice1==dice2){
	                Debug.Log("Out of jail");
					inJail=false;       // Get out of jail
				} else if(dice1 != dice2){
	                jailTimes++;        // Add to number of rolls in jail
	            }
	            // If rolled 3 times in jail
				if(jailTimes==3){
	                Debug.Log("Out of jail");
					jailTimes=0;        // Reset jail roll count
					inJail=false;       // Get out of jail
				}
			}
		}
	
		// BUTTON - Send to Jail
		if (GUI.Button (new Rect (20,70,80,20), "Send to jail")) {
	        Debug.Log("Sent to jail");
			inJail=true;
		}
	}
	
	void RollDice()
	{
		dice1 = Random.Range(1, 7);
		dice2 = Random.Range(1, 7);
		diceResult=dice1+dice2;
	    playTimes++;
	}
}
