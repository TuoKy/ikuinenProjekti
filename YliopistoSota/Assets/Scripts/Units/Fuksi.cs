public class Fuksi : UnitsMain {

    void Start() {
       movement = 200;
       side = 0;
       health = 12;
       nameOfUnit = "MaFuksi";
       unitType = whatUnit.maFuksi;
       initStuff();
    }

    void Update() {

    }

    public void plainAttack(UnitsMain enemy) {
        enemy.addHealth(-5);
    }

}
