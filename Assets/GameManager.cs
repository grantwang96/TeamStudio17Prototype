using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int state = 1;
	public int rounds = 0;
	public int roundcount = 0;
	public int num_of_players = 0;
	public List<string> players = new List<string>();

	public GameObject canvas1;
	public GameObject canvas2;
	public GameObject canvas3;
	public GameObject canvas4;

	public GameObject roundsButton;
	public GameObject playersButton;

	public bool player_win = true;

	// Use this for initialization
	void Start () {
		state = 1;
		roundcount = 1;
		canvas1.SetActive (false);
		canvas2.SetActive (false);
		canvas3.SetActive (false);
		canvas4.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1){
			canvas1.SetActive (true);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
      
			roundsButton.transform.Find("Text").GetComponent<Text>().text = "Rounds: " + rounds.ToString ();
			playersButton.transform.Find("Text").GetComponent<Text>().text = "Players: " + num_of_players.ToString ();

		}

		else if (state == 2){
			canvas1.SetActive (false);
			canvas2.SetActive (true);
			canvas3.SetActive (false);
			canvas4.SetActive (false);
			if(players.Count == num_of_players){
				state = 3;
			}

		}
		else if (state == 3){
			canvas1.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (true);
			canvas4.SetActive (false);

			canvas3.GetComponentInChildren<Text>().text = "Round: " + roundcount.ToString ();

			if(player_win){ //calls a bool from somewhere... need to fill in
				state = 4;
			}

		}
		else if (state == 4){
			canvas1.SetActive (false);
			canvas2.SetActive (false);
			canvas3.SetActive (false);
			canvas4.SetActive (true);
		}
		else{
			Debug.Log ("error: state not found"); 
		}
	}

	void button_start(){
		state = 2;
	}

	void end_canvas(){
		state = 1;
		rounds = 0;
		num_of_players = 0;
		roundcount = 1;
	}

	void player_plus(){
		num_of_players++;
	}

	void player_minus(){
		num_of_players--;
	}

	void round_plus(){
		rounds++;
	}

	void round_minus(){
		rounds--;
	}
		
	void apress(){
		players.Add ("a");
	}

	void bpress(){
		players.Add ("B");
	}

	void cpress(){
		players.Add ("C");
	}
}
