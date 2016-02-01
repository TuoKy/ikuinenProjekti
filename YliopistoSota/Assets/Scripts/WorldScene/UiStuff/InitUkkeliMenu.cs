using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InitUkkeliMenu : MonoBehaviour {

    public List<VaadittuLvlNappula> nappulaLista = new List<VaadittuLvlNappula>();
    public GameObject Fuksi;
    public List<GameObject> spawnPointList = new List<GameObject>();
    public Transform parent;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnFuksi() {

        for (int i = 0; i < spawnPointList.Count; i++) {
            if (GameManager.instance.getHexInPosition(spawnPointList[i].gameObject.transform.position).isHexOccupied() == false) {
                GameObject tempUnit = Instantiate(Fuksi, GameManager.instance.getHexInPosition(spawnPointList[i].gameObject.transform.position).transform.position, Fuksi.transform.rotation) as GameObject;
                GameManager.instance.getHexInPosition(spawnPointList[i].gameObject.transform.position).setUnitInHex(tempUnit.GetComponent<UnitsMain>());
                addUnitToList(tempUnit.GetComponent<UnitsMain>());
                tempUnit.transform.parent = parent;
                break;
            }
        }
    }

    private void addUnitToList(UnitsMain unit) {
        if (TurnManager.instance.getSide() == 0)
            TurnManager.instance.addMathUnit(unit);
        
        if (TurnManager.instance.getSide() == 1)
            TurnManager.instance.addPhyUnit(unit);

        
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
