using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

    [SerializeField] private Animation storeWindowAnimation;
    private bool isOpen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenAndCloseStoreWindow()
    {
        if (!storeWindowAnimation.isPlaying)
        {
            switch (isOpen)
            {
                case (true):
                    //Close Window
                    isOpen = false;
                    storeWindowAnimation.Play("StoreOut");
                    break;
                case (false):
                    //BringUpWindow
                    isOpen = true;
                    storeWindowAnimation.Play("StoreIn");
                    break;
            }
        }
        
    }
}
