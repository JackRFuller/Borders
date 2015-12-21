using UnityEngine;
using System.Collections;

public class LevelSetupManager : ShapeData {

    [Header("Managers")]
    [SerializeField] private MenuUIManager mumScript;
    [SerializeField] private UIManager uiScript;
    [SerializeField] private InputManager imScript;
    [SerializeField] private LevelManager lmScript;
    [SerializeField] private ScoreManager smScript;
    [SerializeField] private SpawnManager spmScript;

    public int shapeCategoryID;
    public int coreShapeID;

    [Header("Level Setup")]
    [SerializeField] private Animation removeUI;

    private GameObject loadedCoreShape;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCoreObject(int _shapeCategory, int _coreShape)
    {
        shapeCategoryID = _shapeCategory;
        coreShapeID = _coreShape;
    }

    public void StartGame()
    {
        //Turn Off Shape Select UI
        mumScript.TurnOffUIToStartGame();

        //Load In Object
        loadedCoreShape = CoreShapeToSpawn();
        loadedCoreShape = Instantiate(loadedCoreShape) as GameObject;

        //Remove Rest of UI
        removeUI.Play("Start Game");

        StartCoroutine(GameLoadUpSequence());      
    }

    public IEnumerator GameLoadUpSequence()
    {
        imScript.FindCoreShape(loadedCoreShape);

        Animation _shapeAnim = loadedCoreShape.GetComponent<Animation>();

        _shapeAnim.Play("ShrinkToFiftyPercent");

        yield return new WaitForSeconds(_shapeAnim.clip.length);

        loadedCoreShape.GetComponent<CoreShapeController>().InitialiseLevel();       

        uiScript.TurnOnGameUI();
        uiScript.InitialiseData(lmScript);

        spmScript.InitialiseData();

        smScript.InitialiseData(lmScript, uiScript);

        //Start Game
        lmScript.StartLevel();
    }

    public void LoadInLevelSelect()
    {
        uiScript.TurnOffGameUI();
        Destroy(loadedCoreShape);
        removeUI.Play("Level Select Drop Down");
    }

    GameObject CoreShapeToSpawn()
    {
        return coreShapeData[shapeCategoryID].levelData[coreShapeID].coreObject;
    }
}
