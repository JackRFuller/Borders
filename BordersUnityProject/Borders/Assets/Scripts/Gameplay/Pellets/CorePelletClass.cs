using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class CorePelletClass : MonoBehaviour {

    public enum pelletState
    {
        inFlight,
        hitCorrectColour,
        hitWrongColour,
    }

    public pelletState currentPelletState;

    //Managers
    public UIManager uiScript;
    public ScoreManager smScript;
    public ComboManager cmScript;

    [Header("Movement")]
    public float movementSpeed;
    public Rigidbody2D pelletRB;
    public Transform target;
    public SpriteRenderer sprite;

    public bool correctColor; //Determines if the pellet has been abosrbed by the correct colour 
    public int addedScore; //How much score is added on

    [Header("UI")]
    [SerializeField]
    private Text scoreText;
    public bool addedCombo;

    

    public void InitialiseManagersAndCoreData()
    {
        //Managers        
        uiScript = GameObject.Find("UIManager").GetComponent<UIManager>();
        smScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        cmScript = GameObject.Find("ComboManager").GetComponent<ComboManager>();

        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        pelletRB = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentPelletState = pelletState.inFlight;
    }

    public void InitialValues()
    {
        if (pelletRB != null && sprite != null)
        {
            pelletRB.isKinematic = false;
            sprite.enabled = true;
            scoreText.color = sprite.color;
            scoreText.enabled = false;
            currentPelletState = pelletState.inFlight;
        }
    }

    public void DetectShape()
    {
        Vector3 _rayDirection = (transform.position - target.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -_rayDirection, Mathf.Infinity);
        Debug.DrawRay(transform.position, -_rayDirection, Color.red);

        if (hit.collider != null)
        {

            if (hit.collider.tag == "CoreShape")
            {
                if (hit.distance < 0.1)
                {
                    if (hit.collider.GetComponent<SpriteRenderer>().color == sprite.color)
                    {
                        currentPelletState = pelletState.hitCorrectColour;
                    }
                    else
                    {
                        currentPelletState = pelletState.hitWrongColour;
                    }


                }
            }
        }
    }

    public void DetermineAddedScore()
    {
        float _points = 0;
        _points = transform.localScale.x * 100;
        int _Roundedpoints = Mathf.RoundToInt(_points);
        addedScore = _Roundedpoints;
        smScript.AddOnPoints(_Roundedpoints);
    }

    public void ShowScore()
    {
        scoreText.text = "+" + addedScore.ToString();
        scoreText.color = sprite.color;
        scoreText.enabled = true;
    }

    public void MoveTowardsTarget()
    {
        Vector3 _targetDirection = Vector3.zero;

        _targetDirection = target.position - transform.position;
        pelletRB.AddRelativeForce(_targetDirection.normalized * movementSpeed, ForceMode2D.Force);

        Vector3 TargetLookAt = new Vector3(transform.position.x,
                                           transform.position.y,
                                           target.position.z);
        transform.LookAt(TargetLookAt);
    }

    public IEnumerator DeathSequence()
    {
        if (correctColor)
        {
            sprite.enabled = false;

            if (gameObject.tag == "Pellet")
            {
                ShowScore();
            }

            pelletRB.isKinematic = true;

        }

        yield return new WaitForSeconds(0.1F);
        addedCombo = false;
        gameObject.SetActive(false);
    }
}
