using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CamMove : MonoBehaviour {

    public int Speed;
    public Transform cameraParent;
    public EventSystem EventSystemManager;

    private Vector2 oldMousePos;
    private float zoomlvl = 0.5f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * Time.deltaTime * Speed;
        cameraParent.Translate(movement, Space.Self);

        if (!EventSystemManager.IsPointerOverGameObject()) {
            float moveZoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * Speed;
            zoomlvl -= moveZoom;
            zoomlvl = Mathf.Clamp(zoomlvl, 0.0f, 1.0f);
            transform.position = cameraParent.position + transform.rotation * new Vector3(0, 0, -20 - zoomlvl * 40);
        }

          
        if (Input.GetMouseButton(2)) {                              
            cameraParent.Rotate(0, oldMousePos.x - Input.mousePosition.x, 0);            
        }
        oldMousePos = Input.mousePosition;



	}

    public void setSpeed(int value) {
        Speed = value;
    }

    public void setPosition(Transform kohde) {
        cameraParent.position = kohde.position;       
    }



}
