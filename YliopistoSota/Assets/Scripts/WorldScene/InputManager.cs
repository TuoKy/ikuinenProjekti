using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    private static InputManager _instance;
    public EventSystem EventSystemManager;

    private Ray ray;
    private RaycastHit info;
    public LayerMask mask;

    public static InputManager instance {
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<InputManager>();
            return _instance;
        }
    }

    public bool isItUiElement() {
        if (EventSystemManager.IsPointerOverGameObject())
            return true;
        else            
            return false;
    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0)) {
            
             if (!isItUiElement() && Physics.Raycast(ray, out info, 100f, mask)) {
                 Debug.Log(info.collider.tag);
                if(info.collider.tag == "HexClick"){
                     
                    if (GameManager.instance.getCurrentUnit() != null && GameManager.instance.getHexInPosition(info.point).isItPassable()) {
                         if (GameManager.instance.isMouseOnPath(info)) {
                             GameManager.instance.moveUnit(info);
                             GameManager.instance.clearPath();
                         }
                         else if(GameManager.instance.getHexInPosition(info.point).isHexOccupied() && !GameManager.instance.getHexInPosition(info.point).isHexOccupiedByEnemy()){
                             GameManager.instance.setCurrentUnit(GameManager.instance.getHexInPosition(info.point).getUnitInHex());
                         }
                         else {
                             GameManager.instance.clearPath();
                             GameManager.instance.setDestination(info.point);
                             GameManager.instance.setPath();
                             GameManager.instance.drawPath();
                         }
                     }                                          
                }            
             
                if (info.collider.tag == "Unit") {
                      GameManager.instance.setCurrentUnit(info.collider.gameObject.GetComponent<UnitsMain>());
                }
                                
                if (info.collider.tag == "Building") {
                      GameManager.instance.clearTargetAndDestination();
                      GameManager.instance.clearPath();
                      info.collider.gameObject.GetComponent<BuildMenu>().buildMenu();                        
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            GameManager.instance.clearTargetAndDestination();
            GameManager.instance.clearPath();
        }
    
    }
}

