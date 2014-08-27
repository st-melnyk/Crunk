using UnityEngine;
using System.Collections;

public enum heartHitState {
    notDefined,
    hitted,
    missed,
}

public class HeartScript : MonoBehaviour {

	// Use this for initialization

    public float heartHitSize;

    public float minXMove;
    public float maxXMove;

    public float maxYMove;
    public float minYMove;

    public float secondsMoveBezierBaseValue ;

    public heartHitState currentHitState;
    private Bezier moveBezier;

    private Vector3 startBezierMovePoint;
    private Vector3 endBezierMovePoint;
    private Vector3 middle1BezierMovePoint;
    private Vector3 middle2BezierMovePoint;

    public float moveBezierPercentage;

    private bool moveBezierRight;

    public bool isChpockAnimationPlaying;

    public int heartMoveCount;

    public int attemptsCount;
    public int maxAttamptsCount;

    Vector3 getRandomFirstPoint() {
        return new Vector3(minXMove, Random.Range( minYMove, maxYMove), transform.position.z);
    }

    Vector3 getRandomSecondPoint()
    {
        return new Vector3(maxXMove, Random.Range(minYMove, maxYMove), transform.position.z);
    }

    Vector3 getRandomMidllePoint() {
        return new Vector3(Random.Range (minXMove * 1.2f, maxXMove * 1.2f) , Random.Range( minYMove , maxYMove ), transform.position.z);
    }

    private void setBezierPoints(
            bool isMovingRight,
            out Vector3 startPoint,
            out Vector3 endPoint,
            out Vector3 mid1Point,
            out Vector3 mid2Point ) {
        Vector3 p1 = getRandomFirstPoint();
        Vector3 p2 = getRandomSecondPoint();
        Vector3 p3 = getRandomMidllePoint();
        Vector3 p4 = getRandomMidllePoint();

        if ( isMovingRight == true ) {
            startPoint = p1;
            endPoint = p2;

            if ( p3.x < p4.x ) {
                mid1Point = p3;
                mid2Point = p4;
            } else {
                mid1Point = p4;
                mid2Point = p3;
            }


        } else {
            startPoint = p2;
            endPoint = p1;

            if (p3.x > p4.x)
            {
                mid1Point = p3;
                mid2Point = p4;
            }
            else
            {
                mid1Point = p4;
                mid2Point = p3;
            }
        }
    }

    void setNewMoveBezier() {
        heartMoveCount ++;
        moveBezierPercentage = 0;
        moveBezierRight = !moveBezierRight;
        setBezierPoints(moveBezierRight, out startBezierMovePoint, out endBezierMovePoint, out middle1BezierMovePoint, out middle2BezierMovePoint);
        moveBezier = new Bezier(transform.position, endBezierMovePoint, middle1BezierMovePoint, middle2BezierMovePoint);
    }

    void Awake() {
        transform.position = new Vector3(minXMove, transform.position.y, transform.position.z);
        currentHitState = heartHitState.notDefined;

        if ( secondsMoveBezierBaseValue == 0 )
            secondsMoveBezierBaseValue = 3;

        heartMoveCount = 0;

        maxAttamptsCount = 4;
        attemptsCount = maxAttamptsCount;
    }

	void Start () {
       
	    isChpockAnimationPlaying = false;
	    moveBezierRight = false;
	    setNewMoveBezier();
	  //  animation.Play( "HeartBeat" );
	}

   public void endGame() {
        SceneScript.ShowGameOver();


        
    }

   public void restartGame() {
        attemptsCount = 4;
      

        transform.position = new Vector3(minXMove, transform.position.y, transform.position.z);
        currentHitState = heartHitState.notDefined;

        if (secondsMoveBezierBaseValue == 0)
            secondsMoveBezierBaseValue = 3;

        heartMoveCount = 0;

        maxAttamptsCount = 4;
        attemptsCount = maxAttamptsCount;

       SceneScript.Panel.totalScoreTextMesh.text = "0";
       SceneScript.Panel.hitsCountTextMesh.text = "0";

       SceneScript.Panel.score = 0;
       SceneScript.Panel.hitsCount = 0;

        SceneScript.HideGameOver();

    }
	
	// Update is called once per frame
	void Update () {

	    if ( moveBezierPercentage > 1 ) {
	        setNewMoveBezier();

            if (attemptsCount == 0)
                endGame();
	    }

	    if ( isChpockAnimationPlaying == true ) {
	        if ( Mathf.Abs( transform.position.x ) > 3.1f ) {
	            if ( transform.position.x > 0 ) {
	                transform.position = new Vector3(
	                        transform.position.x - 3,
	                        transform.position.y,
	                        transform.position.z );
	            } else {
                    transform.position = new Vector3(
                            transform.position.x + 3,
                            transform.position.y,
                            transform.position.z);
	            }
	        }
            return;
	    }
	   
	    
	    Vector3 newPosition =  moveBezier.GetBezierPointAtTime( moveBezierPercentage );


	    transform.position = newPosition;

	    float prevMoveBezierPercentage = moveBezierPercentage;
	    moveBezierPercentage +=  Time.deltaTime / PrototypeVersions.getSecondsMoveBezier( this );

	    if ( ( prevMoveBezierPercentage - 0.5f ) * ( moveBezierPercentage - 0.5f ) < 0 )
	        attemptsCount --;

	    if ( attemptsCount < 0 )
	        attemptsCount = 0;
	}



    public void setHitState() {
        if (currentHitState != heartHitState.notDefined)
            return;

        if ( Mathf.Abs( transform.position.x ) < heartHitSize ) {
            currentHitState = heartHitState.hitted;
        } else {
           currentHitState = heartHitState.missed;
        }

    }


}
