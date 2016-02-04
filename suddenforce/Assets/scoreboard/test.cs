using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();


	}
	
	// Update is called once per frame
	void Update () {
		scoreManager.SetScore ("ejs", "kills", 10);
		scoreManager.SetScore ("ejs", "deaths", 5);
		scoreManager.SetScore ("ejs", "assist", 5);
		scoreManager.SetScore ("bjh", "kills", 10);
		scoreManager.SetScore ("bjh", "deaths", 5);
		scoreManager.SetScore ("bjh", "assist", 5);
		scoreManager.SetScore ("www", "kills", 10);
		scoreManager.SetScore ("www", "deaths", 5);
		scoreManager.SetScore ("www", "assist", 5);
	}
}
