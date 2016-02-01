using UnityEngine;
using System.Collections;

public class LaitosInfo : MonoBehaviour {

    public GameObject ukkelit;
    public GameObject kurssit;
    public InitUkkeliMenu menuInit;
    public CourseMenu courseInit;
    public int side;

    private int laitosLvl;

	// Use this for initialization
	void Start () {
        laitosLvl = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void aktivoiUkkelit() {
        ukkelit.SetActive(true);
        menuInit.init(this);
    }

    public void aktivoiKurssit() {        
        kurssit.SetActive(true);
        courseInit.init(this);
    }

    public int getLaitosLvl() {
        return laitosLvl;
    }
    
    public void setLaitosLvl(int nousu) {
        laitosLvl += nousu;
    }
}
