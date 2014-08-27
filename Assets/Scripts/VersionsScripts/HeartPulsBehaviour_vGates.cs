using UnityEngine;
using System.Collections;

public class HeartPulsBehaviour_vGates : HeartPulsBehaviour {

    float pulsDist;
    private GameObject pulsDeathBackGround;
    private Vector3 pulsDeathBackGroundStartPos;
    private float attemptPulsDist;
    private SpriteRenderer pulsSpriteRenderer;
	// Use this for initialization
	void Awake () {
        puls0GO = GameObject.Find("puls0_vGates");
        puls1GO = GameObject.Find("puls1_vGates");
        puls2GO = GameObject.Find("puls2_vGates");
        puls3GO = GameObject.Find("puls3_vGates");
        puls4GO = GameObject.Find("puls4_vGates");


        pulsPartDist = puls3GO.transform.position.x - puls2GO.transform.position.x;
        pulsStartPostion = puls3GO.transform.position.x;

        GameObject pulsBackGroundObject = GameObject.Find("background_bar_vGates");
	    pulsSpriteRenderer = pulsBackGroundObject.GetComponent<SpriteRenderer>();

        pulsDeathBackGround = GameObject.Find("background_death");
	    pulsDeathBackGroundStartPos = pulsDeathBackGround.transform.position;
        pulsDist = pulsSpriteRenderer.sprite.bounds.size.x;

	    attemptPulsDist = pulsDist / 3;

	}
	
	// Update is called once per frame
	void Update () {
        if (puls3GO.transform.position.x > pulsStartPostion + pulsPartDist * 1.2f)
        {
            exchangePulsDiagrams();

        }

        float beatKoef = getBeatMoveKoefFromBeat();

        puls1GO.transform.position = new Vector3(
                puls1GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
                puls1GO.transform.position.y,
                puls1GO.transform.position.z);
        puls2GO.transform.position = new Vector3(
                puls2GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
                puls2GO.transform.position.y,
                puls2GO.transform.position.z);
        puls3GO.transform.position = new Vector3(
                puls3GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
                puls3GO.transform.position.y,
                puls3GO.transform.position.z);
        puls0GO.transform.position = new Vector3(
                puls0GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
                puls0GO.transform.position.y,
                puls0GO.transform.position.z);
        puls4GO.transform.position = new Vector3(
                puls4GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
                puls4GO.transform.position.y,
                puls4GO.transform.position.z);


	    float newX;

	    if ( SceneScript.Heart.attemptsCount > 3 ) {
           newX = pulsDeathBackGroundStartPos.x ;
            

	    } else if ( SceneScript.Heart.attemptsCount < 1 )
        {
             newX = pulsDeathBackGroundStartPos.x - pulsDist;
	    } else {
	        float heartMovePercentage = SceneScript.Heart.moveBezierPercentage;

	        if ( heartMovePercentage > 0.5f ) {
	            heartMovePercentage -= 0.5f;
	        } else {
	            heartMovePercentage += 0.5f;
	        }

	        newX = pulsDeathBackGroundStartPos.x - attemptPulsDist * ( 3 - SceneScript.Heart.attemptsCount ) -
	                     attemptPulsDist * heartMovePercentage;
	    }

        float prevX = pulsDeathBackGround.transform.position.x;
        float dX = (newX - prevX) / 10.0f;

        pulsDeathBackGround.transform.position = new Vector3(
                    pulsDeathBackGround.transform.position.x + dX,
                    pulsDeathBackGround.transform.position.y,
                    pulsDeathBackGround.transform.position.z);


	    float percentage = Mathf.Abs( pulsDeathBackGround.transform.position.x - pulsDeathBackGroundStartPos.x ) / pulsDist *
	                       2;

	    PrototypeVersions.HeartDeathAlphaValue_vGates = percentage;
        pulsSpriteRenderer.color = getColorWithPercentage(percentage);
        

	}

    Color getColorWithPercentage( float perc ) {
        float rValue;
        float gValue;
        float bValue = 0;

        if ( perc < 0.5f ) {
            gValue = 1.0f;
            rValue = perc * 2;

        } else {
            rValue = 1.0f;
            gValue = ( 1.0f - ( perc - 0.5f ) ) * 2;
        }

        return new Color(rValue, gValue, bValue, 1.0f);
    }


    void exchangePulsDiagrams()
    {
        GameObject tGO = puls4GO;

        puls4GO = puls3GO;
        puls3GO = puls2GO;
        puls2GO = puls1GO;
        puls1GO = puls0GO;
        puls0GO = tGO;

        puls0GO.transform.position = new Vector3(puls1GO.transform.position.x - pulsPartDist, puls1GO.transform.position.y, puls1GO.transform.position.z);
    }

    float getBeatMoveKoefFromBeat()
    {
        return SceneScript.Heart.attemptsCount;
    }

    
}
