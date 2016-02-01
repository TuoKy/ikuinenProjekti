using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PassInformation : MonoBehaviour {

    private static PassInformation _instance;
    public static PassInformation instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PassInformation>();
            return _instance;
        }
    }



    public GameObject MainStage;

    List<UnitsMain> rightsideUnits;
    List<UnitsMain> leftsideUnits;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setLeftUnits(List<UnitsMain> units) {
        leftsideUnits = units;
    }
    public void setrightUnits(List<UnitsMain> units) {
        rightsideUnits = units;
    }

    public List<UnitsMain> getRightSideUnits() {
        return rightsideUnits;
    }

    public List<UnitsMain> getLeftSideUnits() {
        return leftsideUnits;
    }
}
