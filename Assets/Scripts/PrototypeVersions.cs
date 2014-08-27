using System.ComponentModel.Design.Serialization;
using UnityEngine;
using System.Collections;


public enum prototypeVersion {
    heartBeat,
    gates,
}

public class PrototypeVersions : MonoBehaviour {

    public static float HeartDeathAlphaValue_vGates;

    public prototypeVersion PrototypeVersion;

    public static PrototypeVersions instance;

    public static PanelScript FindPanelScript() {

        if ( instance == null )
            instance = GameObject.Find( "PrototypeVersions" ).GetComponent<PrototypeVersions>();


        if ( instance.PrototypeVersion == prototypeVersion.heartBeat ) {
            return FindPanelScript_vHeartBeat();
        }
        else if (instance.PrototypeVersion == prototypeVersion.gates) {
            return FindPanelScript_vGates();
        } else {
            return null;
        }

        
    }

    public static PanelScript FindPanelScript_vGates() {

        GameObject panelGO = null;
        GameObject offPanelGO = null;

       panelGO = GameObject.Find("TopPanel_vGates");
       offPanelGO = GameObject.Find("TopPanel_vHeartBeat");

        offPanelGO.SetActive(false);

        return panelGO.GetComponent<PanelScript>();
    }

    public static PanelScript FindPanelScript_vHeartBeat()
    {
        GameObject panelGO = null;
        GameObject offPanelGO = null;

        panelGO = GameObject.Find("TopPanel_vHeartBeat");
        offPanelGO = GameObject.Find("TopPanel_vGates");

        offPanelGO.SetActive(false);

        return panelGO.GetComponent<PanelScript>();
    }

	public void panelHitHeart (PanelScript delegatePanelScript)
    {


        if (instance.PrototypeVersion == prototypeVersion.heartBeat)
        {
            panelHitHeart_vHeartBeat(delegatePanelScript);
        }
        else if (instance.PrototypeVersion == prototypeVersion.gates) {
            panelHitHeart_vGates(delegatePanelScript);
        }


       
    }


    private void panelHitHeart_vHeartBeat(PanelScript delegatePanelScript)
    {
        int scoreHeartBeat = (int)SceneScript.Panel.getCurrentHeartBeat();
        delegatePanelScript.hitsCount += scoreHeartBeat;
//        delegatePanelScript.scorePointsTextMesh.text = scoreHeartBeat.ToString();
    }

    private void panelHitHeart_vGates(PanelScript delegatePanelScript)
    {
        delegatePanelScript.hitsCount ++;
//        delegatePanelScript.scorePointsTextMesh.text = 1.ToString();
    }

    public static float getSecondsMoveBezier(HeartScript delegateHeartScript)
    {
        if (instance.PrototypeVersion == prototypeVersion.heartBeat)
        {
            return getSecondsMoveBezier_vHeartBeat(delegateHeartScript);
        }
        else if (instance.PrototypeVersion == prototypeVersion.gates)
        {
            return getSecondsMoveBezier_vGates(delegateHeartScript);
        }
        else
        {
            return 0;
        }
    }

    private static float getSecondsMoveBezier_vHeartBeat(HeartScript delegateHeartScript)
    {
        float moveBoost = delegateHeartScript.heartMoveCount / 100.0f;
        if (moveBoost > 1.5f)
            moveBoost = 1.5f;

        float logPar = SceneScript.Panel.getCurrentHeartBeat();

        if (logPar <= 1)
        {
            logPar = 1;
        }

        float result = delegateHeartScript.secondsMoveBezierBaseValue - Mathf.Log(logPar) / 8 - moveBoost;
        //  Debug.Log( "Seconds move bezier - " + result );
        return result;
    }

    private static float getSecondsMoveBezier_vGates(HeartScript delegateHeartScript) {
        float moveBoost = delegateHeartScript.heartMoveCount / 100.0f;
        if (moveBoost > 1.5f)
            moveBoost = 1.5f;

        float result = delegateHeartScript.secondsMoveBezierBaseValue  - moveBoost;
        //  Debug.Log( "Seconds move bezier - " + result );
        return result;
    }

    public static float GetHeartDeathAlphaValue(HeartShapeScript delegatHeartShapeScript) {
        if (instance.PrototypeVersion == prototypeVersion.heartBeat) {
            return GetHeartDeathAlphaValue_vHeartBeat( delegatHeartShapeScript );
        }
        else if (instance.PrototypeVersion == prototypeVersion.gates) {
            return GetHeartDeathAlphaValue_vGates( delegatHeartShapeScript );
        }

        return 0;
    }

    private static float GetHeartDeathAlphaValue_vGates( HeartShapeScript delegatHeartShapeScript ) {
        return  HeartDeathAlphaValue_vGates;
    }

    private static float GetHeartDeathAlphaValue_vHeartBeat(HeartShapeScript delegatHeartShapeScript)
    {
        float heartBeat = SceneScript.Panel.getCurrentHeartBeat();
        float koef = (300.001f - heartBeat) / 300.0f;
        return koef;

    }

}
