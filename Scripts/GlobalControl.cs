using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//created by Yuanguo Lang
//05/10/2018
//All Rights Reserved.
public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instances;

	public int scoreCount;
	public bool isTriggeredBefore;
	public bool isTriggeredAfter;
	public bool isSupposeToShoot;

	public string[] sceneDescrip = new string[]{
		//1. Jhayden (a) and Drew (b) “Pockets” (needs 6 sec in beginning)
		"You have pulled over a vehicle that drove through a stop sign. You approach the driver side window. ",
		//2. Paul (a) (needs 7 sec in beginning)  and Dakari (b) “Ambush” (needs 4 sec in beginning and 1 sec at the end)
		"You and your partner are on a stakeout in an undercover car. You are searching for a suspect on the loose who is wanted for the shooting of a police officer. He is expected to be armed and dangerous.",
		//3. Matt (a) and Darius (b) “Pants”
		"Following a routine traffic stop, you detect the smell of marijuana and notice some drug paraphernalia in the car.  You ask the driver to step out of the car and put his hands on top of the car so that you can search him for weapons.  The driver cooperates, but appears irritated.  ",
		//4. Dakari (a) (needs 5 sec in beginning and 0.5 sec in end) and Paul (b) “Domestic”
		"You are responding to an apparent domestic disturbance.  A concerned citizen overheard a young woman threatening someone on the other end of her cellphone and called the police.  The young woman was overheard saying: “If you come over I will kill you myself!” ",
		//5. Max (a) and Wasim (b) “Flowers”
		"You are responding to an emergency call from a young woman who states that her ex-boyfriend was on his way to her house even though she has a restraining order against him.  When you arrive on the scene, the young woman is sitting on her porch stairs and appears to be crying.",
		//6. Nito (a) and Cale (b) “Suicide”
		"You have been called to the home of a male who is actively suicidal.  The man is armed and is threatening to shoot himself. ",
		//7. Hesh (a) and Marquis (b) “Glove”
		"You have pulled over a vehicle that was speeding.  You approach the driver side window.",
		//8. Marquis (a) and Hesh (b) “Phone”
		"You are interviewing a witness to an armed robbery.  According to witnesses, a male with a gun approached the victim and demanded that he give him his wallet, car keys, and phone.  Your witness claims to be able to provide information about the suspect’s appearance. ",
		//9. Wasim (a) and Max (b) “Registration”
		"While doing a detail, you notice a driver sitting in a car that matches the description of a car that was recently reported as stolen.  You walk over the the driver side window to question the driver.",
		//10. Drew (a) and Jhayden (b) “Flee”
		"You have approached a man who fit the description of a man accused of grabbing a woman’s purse.  The description is close enough that you decide to arrest the suspect.  ",
		//11. Cale (a) and Nito (b) “Dash”
		"You have entered the home of a suspect on a warrant to search and seize narcotics and firearms.  The suspect is well known to police as dangerous and as a flight risk.  You have been asked to keep your eye on the suspect in the living room while the other officers search the apartment.  ",
		//12. Darius (a) and Matt (b) “Run”
		"You have approached an individual who appears to match the description of a man wanted for stealing a wallet.  You begin to question the man while you wait for backup. " 
			
	};






	void Awake(){

		if (Instances == null) {
			DontDestroyOnLoad (this.gameObject);
			Instances = this;
		} else if (Instances != null) {
			Destroy (this.gameObject);
		}

	}

}
