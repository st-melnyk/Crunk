using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class HeartPulsBehaviour : MonoBehaviour {

    public int maxPuls;
    public int moveDiagramSpeedKoef;
    public float pulsDecreaseForSecond;

    private float currentHeartBeatF;
    private int currentHeartBeat;
	// Use this for initialization

    protected GameObject puls0GO;
    protected GameObject puls1GO;
    protected GameObject puls2GO;
    protected GameObject puls3GO;
    protected GameObject puls4GO;

    protected float pulsPartDist;
    protected float pulsStartPostion;

    private TextMesh heartBeatTextMesh;

    public void HitHeart() {
        float k = 2.0f;
        if ( currentHeartBeatF < 30 ) {
            currentHeartBeatF += 30 * k;
        }
        else if ( currentHeartBeatF < 60 ) {
            currentHeartBeatF += 40 * k;
        } else {
            currentHeartBeatF += 60 * k;
        }

        if ( currentHeartBeatF > maxPuls )
            currentHeartBeatF = maxPuls;

    }

    public void MissHeart() {
        currentHeartBeatF -= 10;
    }

    public float GetCurrentHeartBeat() {
        return currentHeartBeatF;
    }

    void Awake() {


        if ( currentHeartBeat == 0 ) {
            currentHeartBeat = 60;
            currentHeartBeatF = currentHeartBeat;
        }

        if (moveDiagramSpeedKoef == 0)
            moveDiagramSpeedKoef = 1;

        if ( pulsDecreaseForSecond == 0 )
            pulsDecreaseForSecond = 10;

        if ( maxPuls == 0 )
            maxPuls = 300;

        puls0GO = GameObject.Find("puls0");
        puls1GO = GameObject.Find( "puls1" );
        puls2GO = GameObject.Find( "puls2" );
        puls3GO = GameObject.Find( "puls3" );

        

        pulsPartDist = puls3GO.transform.position.x - puls2GO.transform.position.x;
        pulsStartPostion = puls3GO.transform.position.x;

        heartBeatTextMesh = GameObject.Find( "HeartBeatText" ).GetComponent<TextMesh>();
    }

    float getBeatMoveKoefFromBeat() {
        return currentHeartBeat / 10.0f;
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if ( puls3GO.transform.position.x > pulsStartPostion + pulsPartDist * 1.2f ) {
	        exchangePulsDiagrams();

	    }
	    float beatKoef = getBeatMoveKoefFromBeat();

	        puls1GO.transform.position = new Vector3(
	                puls1GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
	                puls1GO.transform.position.y,
	                puls1GO.transform.position.z );
	        puls2GO.transform.position = new Vector3(
	                puls2GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
	                puls2GO.transform.position.y,
	                puls2GO.transform.position.z );
	        puls3GO.transform.position = new Vector3(
	                puls3GO.transform.position.x + Time.deltaTime * moveDiagramSpeedKoef * 60 * beatKoef,
	                puls3GO.transform.position.y,
	                puls3GO.transform.position.z );



	    currentHeartBeatF -= pulsDecreaseForSecond * Time.deltaTime;

	    if ( currentHeartBeatF < 0 )
	        currentHeartBeatF = 0;

	    currentHeartBeat = (int) currentHeartBeatF;

        if ((currentHeartBeat % 2) == 0)
	     heartBeatTextMesh.text = currentHeartBeat.ToString();



	}

    void exchangePulsDiagrams() {
        GameObject tGO = puls3GO;

        puls3GO = puls2GO;
        puls2GO = puls1GO;
        puls1GO = tGO;

        puls1GO.transform.position = new Vector3(puls2GO.transform.position.x - pulsPartDist, puls2GO.transform.position.y, puls2GO.transform.position.z);
    }
}
