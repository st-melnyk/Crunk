using UnityEngine;
using System.Collections;

public class SceneScript {

    private static PrototypeVersions _pVersions = GameObject.Find("PrototypeVersions").GetComponent<PrototypeVersions>();
    public static PrototypeVersions PVersion {
        get {
            
            return _pVersions;
        }
    }

    private static SceneScript _instance = new SceneScript();
    public static SceneScript Insance {
        get { return _instance; }
    }

    private static HeartScript _heart = GameObject.Find("heart").GetComponent<HeartScript>();
    public static HeartScript Heart
    {
        get {
            return _heart;
        }
    }


    private static HandScript _hand = GameObject.Find("Hand").GetComponent<HandScript>();
    public static HandScript Hand
    {
        get {   
            return _hand;
        }
    }

    private static PanelScript _panel = PrototypeVersions.FindPanelScript();
    public static PanelScript Panel
    {
        get
        {
            return _panel;
        }
    }

    private static GameObject _endGameObject = GameObject.Find( "EndGame" );
  
    public static GameObject EndGameObject {
        get {
            
            return _endGameObject;
        }
   
    }

    public static void ShowGameOver() {
        _endGameObject.SetActive( true );
    }

    public static void HideGameOver()
    {
        _endGameObject.SetActive(false);
    }
}
