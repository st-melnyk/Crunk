using UnityEngine;
using System.Collections;

public class HandSpriteScript : MonoBehaviour {

    void animHandChpockFinished() {
        SceneScript.Hand.continueBackMoveHandAnimation();
    }

    public void startChpockAnimation() {
        if ( animation.isPlaying == false ) {
            Debug.Log( "Hand move chpock" );
            animation.Play( "HandMoveChpock" );
        }
    }
}
