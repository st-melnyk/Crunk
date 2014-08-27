using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour {

	// Use this for initialization
    public TextMesh hitsCountTextMesh;

    public int hitsCount ;
    public int score ;
//    public TextMesh scorePointsTextMesh;
//
//    private GameObject scorePoints;

    private HeartPulsBehaviour heartBeat;
   
    

    private Vector3 startScorePos;
    private Bezier scorePointsBezier;
    private float moveScorePointPercentage;


    public TextMesh totalScoreTextMesh;

    void Awake() {
        totalScoreTextMesh = GameObject.Find( "Score" ).GetComponent<TextMesh>();

        hitsCountTextMesh = GameObject.Find( "HitsCount" ).GetComponent<TextMesh>();
        heartBeat = GameObject.Find( "puls" ).GetComponent<HeartPulsBehaviour>();
//        scorePoints = GameObject.Find("ScorePoints");
//        scorePointsTextMesh = scorePoints.GetComponent<TextMesh>();
//
//        startScorePos = scorePoints.transform.position;
        Vector3 controlPoint1 = new Vector3(startScorePos.x - 80, startScorePos.y + 100, startScorePos.z);
        Vector3 controlPoint2 = new Vector3(hitsCountTextMesh.transform.position.x - 20, hitsCountTextMesh.transform.position.y - 20, hitsCountTextMesh.transform.position.z);

        scorePointsBezier = new Bezier( startScorePos, hitsCountTextMesh.transform.position, controlPoint1, controlPoint2 );
//       
//        scorePoints.SetActive( false );

        hitsCount = 0;
        score = 0;
        SceneScript.EndGameObject.transform.position = new Vector3(SceneScript.EndGameObject.transform.position.x, SceneScript.EndGameObject.transform.position.y, -10);
        SceneScript.HideGameOver();
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//	    if ( scorePoints.active == true ) {
//	        scorePoints.transform.position = scorePointsBezier.GetBezierPointAtTime( moveScorePointPercentage );
//	        moveScorePointPercentage += Time.deltaTime;
//
//	        if ( moveScorePointPercentage > 1.0 ) {
//	            scorePoints.SetActive( false );
//	        }
//	    }
	}

    public void hitHeart() {
        SceneScript.PVersion.panelHitHeart( this );

        moveScorePointPercentage = 0;

//        scorePoints.transform.position = startScorePos;
//        scorePoints.SetActive( true );

        hitsCountTextMesh.text = hitsCount.ToString();
        heartBeat.HitHeart();

        if ( totalScoreTextMesh != null ) {
            if ( SceneScript.Heart.attemptsCount > 2 ) {
                score += 10;
            } else if (SceneScript.Heart.attemptsCount > 1) {
                score += 5;
            }
            else  {
                score += 1;
            }

            totalScoreTextMesh.text = score.ToString();
        }

        if (SceneScript.Heart.moveBezierPercentage >= 0.5f)
             SceneScript.Heart.attemptsCount = 4;
        else {
            SceneScript.Heart.attemptsCount = 5;
        }
    }

    public void misHeart() {
        heartBeat.MissHeart();

        SceneScript.Heart.attemptsCount -= 1;
    }

    public float getCurrentHeartBeat() {
        return heartBeat.GetCurrentHeartBeat();
    }


}
