using UnityEngine;
using System.Collections;

public class BackGroundScript : MonoBehaviour {

    void OnMouseDown() {
    
        if (SceneScript.EndGameObject.active == false)
             SceneScript.Hand.startHandMove();
        else {
            SceneScript.Heart.restartGame();
        }
    }
}
