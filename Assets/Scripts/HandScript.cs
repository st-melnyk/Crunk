using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public float moveSpeed;

    private float moveHandForwardAnimationTime;
    private HandSpriteScript handSprite;


    private bool isMovingBack;

    void Awake() {
        handSprite = GameObject.Find( "HandSprite" ).GetComponent<HandSpriteScript>();
    }


    public void startHandMove() {
        if ( animation.isPlaying == false ) {
            audio.Play();

            isMovingBack = false;

            SceneScript.Heart.currentHitState = heartHitState.notDefined;

            animation[ "HandMoveForward" ].speed = moveSpeed;
            animation[ "HandMoveBack" ].speed = moveSpeed;
            animation.Play( "HandMoveForward" );
        }

    }

    void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        if (isMovingBack == true)
            return;

        isMovingBack = true;

        SceneScript.Heart.setHitState();
        
        if ( otherCollider2D.name == "heartSprite" ) {

            Debug.Log( "prepare for hit" );
         
            if ( SceneScript.Heart.currentHitState == heartHitState.hitted ) {
                Debug.Log( "MADE HEART HIT" );
                
                moveHandForwardAnimationTime = animation[ "HandMoveForward" ].time;
                animation.Stop( "HandMoveForward" );
                handSprite.startChpockAnimation();
            }
            else if ( SceneScript.Heart.currentHitState == heartHitState.missed ) {
                moveHandForwardAnimationTime = animation["HandMoveForward"].time;
                continueBackMoveHandAnimation();
            }
        }
    }


    void moveForwardFinished() {
        if (SceneScript.Heart.currentHitState == heartHitState.notDefined)
        {
            Debug.Log( "hand move back" );
            animation["HandMoveForward"].speed = moveSpeed * -1;
            animation.Play("HandMoveForward");

        }

    }

    void animMoveBackFinished() {
      //  isMovingBack = false;
    }

    public void continueBackMoveHandAnimation() {
        Debug.Log( "Continue move back" );
        
        animation.Stop("HandMoveForward");
        animation[ "HandMoveForward" ].time = moveHandForwardAnimationTime;
        animation[ "HandMoveForward" ].speed = moveSpeed * -1 ;
        animation.Play("HandMoveForward");
    }
}
