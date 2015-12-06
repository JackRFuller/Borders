using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    [SerializeField] private MenuUIManager mumScript;

	// Use this for initialization
	void Start () {

        SetNumberofCoins();
	
	}   

    void InitialiseData()
    {
        SetNumberofCoins();
    }

    void SetNumberofCoins()
    {
        mumScript.UpdateCoins(PlayerPrefs.GetInt("Coins"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
