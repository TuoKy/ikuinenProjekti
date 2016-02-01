using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {

    public CamMove purkka;
    public GameObject Menu;
    public MenuManager manager;
    public GameObject TurnMenu;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

     public void buildMenu() {
        if (gameObject.GetComponent<LaitosInfo>().side == TurnManager.instance.getSide() && !TurnManager.instance.isPlayerInBuildMenu()) {
            TurnMenu.SetActive(false);
            Menu.SetActive(true);
            purkka.setPosition(transform);
            purkka.setSpeed(0);
            manager.setValittu(gameObject.GetComponent<LaitosInfo>());
        }
    }



}
