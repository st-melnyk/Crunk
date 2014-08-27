using UnityEngine;
using System.Collections;

public class HeartShapeScript : MonoBehaviour {

    public AudioClip hitAudioClip;
    public AudioClip missedAudioClip;
    public AudioClip chpockAudioClip;


    public SpriteRenderer DeathHeartSpriteRenderer;
    public SpriteRenderer HealthHeartSpriteRenderer;

    void Awake()
    {
        DeathHeartSpriteRenderer = GameObject.Find("deathHeartSprite").GetComponent<SpriteRenderer>();
        HealthHeartSpriteRenderer = GetComponent<SpriteRenderer>();

       
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    float koef = PrototypeVersions.GetHeartDeathAlphaValue( this );
  
       // Debug.Log( "KOEF - " + koef );
       // DeathHeartSpriteRenderer.color = new Color(DeathHeartSpriteRenderer.color.r, DeathHeartSpriteRenderer.color.g, DeathHeartSpriteRenderer.color.b, 1.0f - koef);
     //   DeathHeartSpriteRenderer.color = new Color(DeathHeartSpriteRenderer.color.r, DeathHeartSpriteRenderer.color.g, DeathHeartSpriteRenderer.color.b, koef);

        
	}


    void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        
        if (otherCollider2D.name == "Hand")
        {
            SceneScript.Heart.setHitState();

            if (SceneScript.Heart.currentHitState == heartHitState.hitted)
            {

                if ( animation.isPlaying == false ) {
                    Debug.Log("start heart Chpock");
                    SceneScript.Heart.isChpockAnimationPlaying = true;
                    audio.clip = hitAudioClip;
                    audio.Play();
                    animation.Play( "HeartChpock" );
                    SceneScript.Panel.hitHeart();

                   // Debug.Log("heart chpock");
                }
            }
            else
            {
                if ( animation.isPlaying == false ) {
                    audio.clip = missedAudioClip;
                    audio.Play();
                    Debug.Log( "start miss heart" );
                    if ( transform.position.x < 0 ) {
                        animation.Play( "HeartRightMiss" );
                    } else {
                        animation.Play("HeartLeftMiss");
                    }

                    SceneScript.Panel.misHeart();
                }
            }
        }
    }

    void chpockFinished() {
        Debug.Log( "heart Chpock finished" );

        audio.clip = chpockAudioClip;
        audio.Play();
        SceneScript.Heart.isChpockAnimationPlaying = false;
    }
}
