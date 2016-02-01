using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FightMenu : MonoBehaviour {

    private static FightMenu _instance;

    //This is the public reference that other classes will use
    public static FightMenu instance {
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<FightMenu>();
            return _instance;
        }
    }

    public GameObject fiteScene;
    public GameObject button;
    public GameObject panel;
    public Text winnerText;

    private List<UnitsMain> rightUnits;
    private List<UnitsMain> leftUnits;
    private List<UnitsMain> realRightUnits;
    private List<UnitsMain> realLeftUnits;

    public List<GameObject> spawnPointList = new List<GameObject>();

    private UnitsMain.whatUnit current = UnitsMain.whatUnit.temp;

    private List<UnitsMain> units;
    private int turnOrder;
    private List<GameObject> buttons = new List<GameObject>();

    public GameObject enemyBg;
    public GameObject currentUnitBg;

    private UnitsMain currentTarget;

    private Transform buttonGrid;



	// Use this for initialization
	void Start () {

        rightUnits = new List<UnitsMain>();
        leftUnits = new List<UnitsMain>();

        turnOrder = 0;

        realRightUnits = PassInformation.instance.getRightSideUnits();
        realLeftUnits = PassInformation.instance.getLeftSideUnits();

        units = new List<UnitsMain>();

        for (int i = 0; i < realRightUnits.Count; i++) {
            GameObject temp = Instantiate(realRightUnits[i].gameObject, spawnPointList[i].transform.position, realRightUnits[i].transform.rotation) as GameObject;
            units.Add(temp.GetComponent<UnitsMain>());
            rightUnits.Add(temp.GetComponent<UnitsMain>());
            temp.gameObject.transform.parent = fiteScene.transform;
        }

        for (int i = 0; i < realLeftUnits.Count; i++) {

            GameObject temp = Instantiate(realLeftUnits[i].gameObject, spawnPointList[i + 4].transform.position, realLeftUnits[i].transform.rotation) as GameObject;
            units.Add(temp.GetComponent<UnitsMain>());
            leftUnits.Add(temp.GetComponent<UnitsMain>());
            temp.gameObject.transform.parent = fiteScene.transform;
        }

        winnerText.text = "";

        currentUnitBg = Instantiate(currentUnitBg, units[0].transform.position, currentUnitBg.transform.rotation) as GameObject;
        currentUnitBg.transform.parent = fiteScene.transform;
        enemyBg = Instantiate(enemyBg, new Vector3(100, 100, 100), enemyBg.transform.rotation) as GameObject;
        enemyBg.transform.parent = fiteScene.transform;

        buttonGrid = panel.transform.FindChild("Grid");

        setCurrentMenu(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void clearPreviousButtons() {
        for (int i = 0; i < buttons.Count; i++) {
            Destroy(buttons[i]);
        }
        buttons.Clear();
    }


    public void setCurrentMenu() {

        checkDeadPeople();

        if (chekWinConditions()) {
            
            clearPreviousButtons();

            current = units[turnOrder].getUnitType();

            currentUnitBg.transform.position = units[turnOrder].transform.position;
            enemyBg.transform.position = new Vector3(100, 100, 100);

            createAttackButton(units[turnOrder]);

            turnOrder++;
            if (turnOrder == units.Count) {
                turnOrder = 0;
            }
        }
        else {
            concludeFight();
        }

    }

    public void setCurrentTarget(UnitsMain target) {
        currentTarget = target;
        enemyBg.transform.position = target.transform.position;
    }

    public void createAttackButton(UnitsMain currentUnit) {

        GameObject temp = Instantiate(button) as GameObject;       
        temp.transform.SetParent(buttonGrid, false);
        addSpecialties(temp);
        buttons.Add(temp);
    }

    private void addSpecialties(GameObject temp) {

        Button tempButton = temp.GetComponent<Button>();

        switch (current) {

            case UnitsMain.whatUnit.fyFuksi:

                string tempString = "Fysiikka";

                temp.GetComponentInChildren<Text>().text = tempString;
                FyFuksi tempFyFuksi = units[turnOrder].gameObject.GetComponent<FyFuksi>();
                tempButton.onClick.AddListener(() => { tempFyFuksi.plainAttack(currentTarget); });
                tempButton.onClick.AddListener(() => { setCurrentMenu(); });
            
            break;

            case UnitsMain.whatUnit.maFuksi:

                tempString = "Matikka";

                temp.GetComponentInChildren<Text>().text = tempString;
                Fuksi tempFuksi = units[turnOrder].gameObject.GetComponent<Fuksi>();
                tempButton.onClick.AddListener(() => { tempFuksi.plainAttack(currentTarget); });
                tempButton.onClick.AddListener(() => { setCurrentMenu(); });
            break;

            default:
                   Debug.Log("Mithän helvettiä");
            break;
        }
    }

    private void checkDeadPeople() {

        for (int i = units.Count -1; i >= 0; i--) {
            if (units[i].getHealth() < 0) {
                UnitsMain temp = units[i];
                rightUnits.Remove(units[i]);
                leftUnits.Remove(units[i]);
                units.Remove(units[i]);
                Destroy(temp.gameObject);                
            }
        }
    }

    private bool chekWinConditions() {
        if (leftUnits.Count == 0) {
            winnerText.text = "Rightside wins, yay";
            return false;
        }
        if (rightUnits.Count == 0) {
            winnerText.text = "Leftside wins, yay";
            return false;
        }
        return true;
    }

    private void concludeFight() {

        Destroy(fiteScene);
        PassInformation.instance.MainStage.SetActive(true);
       
    }

}
