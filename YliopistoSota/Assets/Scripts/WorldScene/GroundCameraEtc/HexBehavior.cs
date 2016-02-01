using UnityEngine;
using System.Collections;

public class HexBehavior : MonoBehaviour {

    private UnitsMain unitInHex = null;
    private int movementCost;
    private bool passable = true;

	// Use this for initialization
    void Start() {
        movementCost = 2;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void setUnitInHex(UnitsMain unit){
        unitInHex = unit;
    }

    public bool isHexOccupied() {    
        if (unitInHex != null)
            return true;
        else
            return false;
    }

    public bool isHexOccupiedByEnemy() {

            if (GameManager.instance.getCurrentUnit() != null && getUnitInHex().getSide() != GameManager.instance.getCurrentUnit().getSide())
                return true;
            else
                return false;      
    }

    public UnitsMain getUnitInHex() {
        return unitInHex;
    }

    public int getMovementCost() {
        return movementCost;
    }

    public void setMovementCost(int newCost) {
        movementCost = newCost;
    }

    public void setPassable(bool state) {
        passable = state;
    }

    public bool isItPassable() {
        return passable;
    }

}
