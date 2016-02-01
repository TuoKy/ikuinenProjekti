public class FyFuksi : UnitsMain {

	// Use this for initialization
	void Start () {
        movement = 10;
        side = 1;
        health = 8;
        nameOfUnit = "FyFuksi";
        unitType = whatUnit.fyFuksi;
        initStuff();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void plainAttack(UnitsMain enemy) {
        enemy.addHealth(-3);
    }

}
