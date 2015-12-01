using UnityEngine;
using System.Collections;

public class NavigationManager : MonoBehaviour {

	public enum menuScreen
	{
		TitleScreen,
		LevelSelectScreen,
	}

	public menuScreen currentMenuScreen;

	[Header("Animations")]
	[SerializeField] private Animation titleScreen;
	[SerializeField] private string titleScreenIn;
	[SerializeField] private string titleScreenOut;

	[SerializeField] private Animation levelSelect;
	[SerializeField] private string levelSelectIn;


	public void MoveToLevelSelect()
	{
		titleScreen.Play(titleScreenOut);
		levelSelect.Play(levelSelectIn);

		currentMenuScreen = menuScreen.LevelSelectScreen;
	}
}
