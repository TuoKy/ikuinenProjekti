using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class FightInputManager : MonoBehaviour {

    private static FightInputManager _instance;

    public EventSystem EventSystemManager;

    private Ray ray;
    private RaycastHit info;
    public LayerMask mask;

    public FightMenu fiteMenu;

    public static FightInputManager instance {
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<FightInputManager>();
            return _instance;
        }
    }


	// Use this for initialization
	void Start () {
	
	}

    public bool isItUiElement() {
        if (EventSystemManager.IsPointerOverGameObject())
            return true;
        else
            return false;
    }

	// Update is called once per frame
	void Update () {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {

            if (!isItUiElement() && Physics.Raycast(ray, out info, 100f, mask)) {

                if (info.collider.tag == "Unit") {
                    fiteMenu.setCurrentTarget(info.collider.gameObject.GetComponent<UnitsMain>());
                }

            }

        }

	}
}
