using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CourseMenu : MonoBehaviour {

    public List<VaadittuLvlNappula> nappulaLista = new List<VaadittuLvlNappula>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init(LaitosInfo info) {
        // tarkistaa onko laitoksella tarvittava lvl tiettyjen unittien / kurssien suorittamiseen
        for (int i = 0; i < nappulaLista.Count; i++) {
            if (nappulaLista[i].vaadittuLvl <= info.getLaitosLvl()) {
                nappulaLista[i].GetComponent<Button>().interactable = true;
            }
            else
                nappulaLista[i].GetComponent<Button>().interactable = false;
        }
    }
}
