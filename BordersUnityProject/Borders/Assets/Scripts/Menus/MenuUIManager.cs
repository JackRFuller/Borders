using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuUIManager : MonoBehaviour {

    [Header("Coins")]
    [SerializeField] private Text coinText;

    [Header("UI To Turn Off")]
    [SerializeField] private GameObject shapeSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateCoins(int _numOfCoins)
    {
        coinText.text = _numOfCoins + "x";
    }

    public void TurnOffUIToStartGame()
    {
        shapeSelect.SetActive(false);
    }
}
