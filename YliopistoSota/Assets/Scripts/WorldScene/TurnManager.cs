using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

    public GameObject buttonUnit;
    public GameObject panel;

    private static TurnManager _instance;
    private int turn;
    private int side;
    private bool inMenu = false;

    private List<UnitsMain> mathUnits = new List<UnitsMain>();
    private List<UnitsMain> physicsUnits = new List<UnitsMain>();

    private List<GameObject> buttons = new List<GameObject>();

    public static TurnManager instance {
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<TurnManager>();
            return _instance;
        }
    }

    public void addMathUnit(UnitsMain unit) {
        mathUnits.Add(unit);
    }

    public void addPhyUnit(UnitsMain unit) {
        physicsUnits.Add(unit);
    }

	// Use this for initialization
	void Start () {
        side = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getSide() {
        return side;
    }

    public int getTurn() {
        return turn;
    }

    private void createButtons() {
        Transform buttonGrid = panel.transform.FindChild("UnitGrid");
        switch (side) {
            case 0:
                for (int i = 0; i < mathUnits.Count; i++) {
                    GameObject temp = Instantiate(buttonUnit) as GameObject;
                    Button tempButton = temp.GetComponent<Button>();
                    temp.transform.SetParent(buttonGrid, false);
                    UnitsMain tempUnit = mathUnits[i];
                    string tempString = mathUnits[i].getName();
                    tempString = tempString + " " + mathUnits[i].getMovementLeft().ToString();
                    tempButton.GetComponentInChildren<Text>().text = tempString;                    
                    tempButton.onClick.AddListener(() => { GameManager.instance.setDefinetelyRightUnit(tempUnit); });
                    buttons.Add(temp);                    
                }

                    break;
            case 1:
                    for (int i = 0; i < physicsUnits.Count; i++) {
                        GameObject temp = Instantiate(buttonUnit) as GameObject;
                        Button tempButton = temp.GetComponent<Button>();
                        temp.transform.SetParent(buttonGrid, false);
                        UnitsMain tempUnit = physicsUnits[i];
                        string tempString = physicsUnits[i].getName();
                        tempString = tempString + " " + physicsUnits[i].getMovementLeft().ToString();
                        tempButton.GetComponentInChildren<Text>().text = tempString; 
                        tempButton.onClick.AddListener(() => { GameManager.instance.setDefinetelyRightUnit(tempUnit); });
                        buttons.Add(temp);
                    }
                break;
        }
    }

    private void clearButtons() {

        for (int i = 0; i < buttons.Count; i++) {
            Destroy(buttons[i]);
        }
            buttons.Clear();
        
    }

    private void initMovementsRemaining(List<UnitsMain> units) {
        for (int i = 0; i < units.Count; i++) {
            units[i].setMovementsRemaining(units[i].getMovement());
        }
    }

    public void endTurn() {              
        turn++;
        if (side == 0)
            side = 1;
        else
            side = 0;

        initMovementsRemaining(mathUnits);
        initMovementsRemaining(physicsUnits);
        clearButtons();
        createButtons();
    }

    public void leaveMenu() {
        clearButtons();
        createButtons();
        inMenu = false;
    }

    public bool isPlayerInBuildMenu() {
        return inMenu;
    }

    public void updateUnitButton() {
        if (side == 0) {
            for (int i = 0; i < mathUnits.Count; i++) {
                if (GameManager.instance.getCurrentUnit().Equals(mathUnits[i])) {
                    Button tempButton = buttons[i].GetComponent<Button>();
                    string tempString = GameManager.instance.getCurrentUnit().getName();
                    tempString = tempString + " " + GameManager.instance.getCurrentUnit().getMovementLeft().ToString();
                    tempButton.GetComponentInChildren<Text>().text = tempString;
                }
            }
        }
        else {
            for (int i = 0; i < physicsUnits.Count; i++) {
                if (GameManager.instance.getCurrentUnit().Equals(physicsUnits[i])) {
                    Button tempButton = buttons[i].GetComponent<Button>();
                    string tempString = GameManager.instance.getCurrentUnit().getName();
                    tempString = tempString + " " + GameManager.instance.getCurrentUnit().getMovementLeft().ToString();
                    tempButton.GetComponentInChildren<Text>().text = tempString;
                }
            }
        }
    }
}
