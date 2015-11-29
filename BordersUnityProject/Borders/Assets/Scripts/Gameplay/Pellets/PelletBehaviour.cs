using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PelletBehaviour : MonoBehaviour {

    [Header("Managers")]
    [SerializeField]
    private UIManager uiScript;
    private ScoreManager smScript;

    [Header("Movement")]
    public float movementSpeed;
    private Rigidbody2D pelletRB;
    private Transform target;
    private SpriteRenderer sprite;

    private bool correctColor; //Determines if the pellet has been abosrbed by the correct colour 
    private int addedScore; //How much score is added on

    [Header("UI")]
    [SerializeField] private Text scoreText;

	// Use this for initialization
	void Start () {

        //Managers        
        uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        smScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        pelletRB = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    public void InitialValues()
    {
        if(pelletRB != null && sprite != null)
        {
            pelletRB.isKinematic = false;
            sprite.enabled = true;
            scoreText.color = sprite.color;
            scoreText.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

        MoveTowardsTarget();

        DetectShape();      
	
	}

    void DetectShape()
    {
        Vector3 _rayDirection = (transform.position - target.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -_rayDirection, Mathf.Infinity);
        Debug.DrawRay(transform.position, -_rayDirection, Color.red);

        if(hit.collider != null)
        {           

            if (hit.collider.tag == "CoreShape")
            {
                if(hit.distance < 0.1)
                {
                    if(hit.collider.GetComponent<SpriteRenderer>().color == sprite.color)
                    {
                        CoreShapeController cscScript = hit.transform.parent.root.GetComponent<CoreShapeController>();
                        cscScript.ShrinkShape();

                        float _points = 0;
                        _points = transform.localScale.x * 100;
                        int  _Roundedpoints = Mathf.RoundToInt(_points);
                        addedScore = _Roundedpoints;

                        smScript.AddOnPoints(_Roundedpoints);

                        correctColor = true;

                        StartCoroutine(DeathSequence());
                    }
                    else
                    {
                        float _healthToLose = 0;
                        _healthToLose = transform.localScale.x / 2;

                        uiScript.LoseHealth(_healthToLose);

                        correctColor = false;

                        gameObject.SetActive(false);
                    }

                    
                }
            }
        } 
    }

    IEnumerator DeathSequence()
    {
        if (correctColor)
        {
            sprite.enabled = false;
            scoreText.text = "+" + addedScore.ToString();
            scoreText.color = sprite.color;
            scoreText.enabled = true;
            pelletRB.isKinematic = true;
            
        }
        yield return new WaitForSeconds(0.1F);        
        gameObject.SetActive(false);
    }

    void MoveTowardsTarget()
    {
        Vector3 _targetDirection = Vector3.zero;

        _targetDirection = target.position - transform.position;
        pelletRB.AddRelativeForce(_targetDirection.normalized * movementSpeed, ForceMode2D.Force);

        Vector3 TargetLookAt = new Vector3(transform.position.x,
                                           transform.position.y,
                                           target.position.z);
        transform.LookAt(TargetLookAt);     
    }


}
