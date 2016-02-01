using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitsMain : MonoBehaviour {

    public enum whatUnit {
        temp,
        fyFuksi,
        maFuksi
    }

    private List<GameManager.node> pathOfUnit;
    
    
    protected int side;
    protected int movement;
    protected int health;

    protected string nameOfUnit;
    protected int movementsRemaining;
        
    public whatUnit unitType;

	void Start () {
	}

    protected void initStuff() {
        movementsRemaining = movement;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public int getMovement() {
        return movement;
    }

    public int getSide() {
        return side;
    }
    public string getName() {
        return nameOfUnit;
    }

    public void setMovement(int movement) {
        this.movement = movement;
    }

    public void setPathOfUnit(List<GameManager.node> path) {
        pathOfUnit = path;
    }

    public void setMovementsRemaining(int movementLeft) {
        movementsRemaining = movementLeft;
    }

    public int getMovementLeft() {
        return movementsRemaining;
    }

    public whatUnit getUnitType() {
        return unitType;
    }

    public int getHealth() {
        return health;
    }

    public void addHealth(int modifier) {
        health = health + modifier;
    }

}
