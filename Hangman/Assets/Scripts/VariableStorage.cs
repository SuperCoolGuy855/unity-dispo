using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VariableStorage : MonoBehaviour {
	public int number = 0;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	public void SetCharNumber(int charnum)
	{
		number = charnum;
		SceneManager.LoadScene("Main");
	}
}
