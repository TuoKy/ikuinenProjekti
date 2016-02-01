using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    private LaitosInfo Valittu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void naytaValitunKurssit() {
        Valittu.aktivoiKurssit();
    }

    public void naytaValitunUkkelit() {
        Valittu.aktivoiUkkelit();
    }

    public void setValittu(LaitosInfo laitos) {
        Valittu = laitos;
    }
}
