using UnityEngine;
using System.Collections;
using System.IO;

public class Screenshot : MonoBehaviour {	
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine(ScreenshotEncode());
	
	}

    IEnumerator ScreenshotEncode()
    {
        yield return new WaitForEndOfFrame();

        //create a 2d texture
        Texture2D _texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        //put buffer into Texture
        _texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        yield return 0;

        byte[] bytes = _texture.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/../screenshot " + PlayerPrefs.GetInt("ScreenshotCount") + ".png", bytes);
        PlayerPrefs.SetInt("ScreenshotCount", PlayerPrefs.GetInt("ScreenshotCount") + 1);

        Destroy(_texture);
    }
}
