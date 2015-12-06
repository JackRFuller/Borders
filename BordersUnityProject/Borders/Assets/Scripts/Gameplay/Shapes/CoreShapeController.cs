using UnityEngine;
using System.Collections;

public class CoreShapeController : MonoBehaviour {

    [Header("Managers")]
    public LevelManager lmScript;

    [Header("Movement")]
    [SerializeField] private float rotSpeed;
    [HideInInspector] public string rotDirection;
    [HideInInspector] public bool rotating;

    [Header("Growth")]
    [SerializeField] private float growthRate;
    [SerializeField] private float maxSize;

    [Header("Shrinkage")]
    [SerializeField] private float shrinkRate;
    [SerializeField] private float minSize;

    //LerpingVariables
    private float timeTakenDuringLerp = 0.5F;
    private bool isLerping;
    private Vector3 startSize;
    private Vector3 endSize;
    private float timeStartedLerping;

	// Use this for initialization
	void Start () {

        InitialiseLevel();
	
	}

    /// <summary>
    /// Setups the core data to start the level
    /// </summary>
    public void InitialiseLevel()
    {
        minSize = transform.localScale.x / 2;
        lmScript = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if(lmScript.currentGameState == LevelManager.gameState.InProgress)
        {
            if (rotating)
            {
                RotateShape();
            }

            GrowShape();
        }

        if (isLerping)
        {
            LerpShrinkage();
        }
	}

    /// <summary>
    /// Controls the growth of the shape
    /// </summary>
    void GrowShape()
    {
        Vector3 _newSize = new Vector3(transform.localScale.x + growthRate,
                                        transform.localScale.y + growthRate,
                                        transform.localScale.z + growthRate);

        transform.localScale = _newSize;

        CheckSize();                        
        
    }

    public void ShrinkShape()
    {
        if(transform.localScale.x >= minSize)
        {
            isLerping = true;
            timeStartedLerping = Time.time;

            startSize = transform.localScale;
            endSize = new Vector3(transform.localScale.x - shrinkRate,
                                           transform.localScale.y - shrinkRate,
                                           transform.localScale.z - shrinkRate);            
        }       
    }

    void LerpShrinkage()
    {
        float _timeSinceStarted = Time.time - timeStartedLerping;
        float _percentageComplete = _timeSinceStarted / timeTakenDuringLerp;

        transform.localScale = Vector3.Lerp(startSize, endSize, _percentageComplete);

        if(_percentageComplete >= 1.0F)
        {
            isLerping = false;
        }
    }

    /// <summary>
    /// Checks to make sure the shape hasn't hit its max size
    /// </summary>
    void CheckSize()
    {
        if(transform.localScale.x >= maxSize)
        {
            Debug.Log("Game Over!!");
            lmScript.GameOver();
        }
    }

    /// <summary>
    /// Rotates the shape from left - right and vice versa
    /// </summary>
    void RotateShape()
    {
        Vector3 _rotDirection = Vector3.zero;

        if(rotDirection == "Left")
        {
            _rotDirection = Vector3.forward;
        }

        if (rotDirection == "Right")
        {
            _rotDirection = Vector3.back;
        }
               
        transform.Rotate(_rotDirection * rotSpeed);
    }
}
