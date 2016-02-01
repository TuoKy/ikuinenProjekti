using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public class node {
        public HexBehavior current;
        public node  previous;
        public float hDistance;
        public float gDistance;
        public float distance;       

        public node(HexBehavior current, node previous, float hDistance, float gDistance) {
            this.current = current;
            this.previous = previous;
            this.hDistance = hDistance;
            this.gDistance = gDistance;
            distance = hDistance + gDistance;
        }

    }

    //sisältää rivit hexoista, jokaisella rivillä on oman rivinsä hexat järjestyksessä
    public List<List<HexBehavior>> hexaRivi = new List<List<HexBehavior>>(); 
        
    private static GameManager _instance;
    private UnitsMain currentUnit;
    private Vector3 destination;
    private List<node> path = new List<node>();
    private List<GameObject> pathMarks = new List<GameObject>();
    public GameObject pathMark;
    public CamMove cam;
    

    public static GameManager instance {
        get {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
              
	}

    private int[] countHexDim(Vector3 sijainti) {

        int[] vastaus = new int[2]; 

        int z = Mathf.RoundToInt((sijainti.z - 1) / 1.5f); //rivi eli eka dimensio. Sijainti jaetaan koolla
        int x = 0;                                         //monesko rivissä eli toinen dimensio

        if (z % 2 == 0) {
            x = Mathf.RoundToInt((sijainti.x - 1) / 2);
        }
        else {
            x = Mathf.RoundToInt((sijainti.x - 2) / 2);
        }

        vastaus[0] = z;
        vastaus[1] = x;

        return vastaus;
    }

    public HexBehavior getHexInPosition(Vector3 sijainti) {

        int[] hex = countHexDim(sijainti);

        if (hex[0] >= 0 && hex[1] >= 0)
            return hexaRivi[hex[0]][hex[1]];
        else
            return null;
    }

    private List<HexBehavior> getHexNeighbours(Vector3 sijainti) {

        List<HexBehavior> neighbours = new List<HexBehavior>();

        int[] hex = countHexDim(sijainti);
       
        if(hex == null) {
            return null;
        }
        else if (hex[0] == 0) {
            if (hex[1] == 0) {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
            }
            else if (hex[1] == 49) {
                neighbours.Add(hexaRivi[hex[0] +1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
            }
            else {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] - 1]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] - 1]);
            }
        }
        else if (hex[0] == 65) {
            if (hex[1] == 0) {
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
            }
            else if (hex[1] == 49) {
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
            }
            else {
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1] - 1]);
            }          
        }
        else if (hex[1] == 0) {
            if (hex[0] % 2 == 0) {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
            }
            else {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] + 1]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1] + 1]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
            }
        }
        else if (hex[1] == 49) {
            if (hex[0] % 2 == 0) {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
            }
            else {
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] - 1]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1] - 1]);
                neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
            }
        }
        else {
            neighbours.Add(hexaRivi[hex[0]][hex[1] - 1]);
            neighbours.Add(hexaRivi[hex[0]][hex[1] + 1]);
            if (hex[0] % 2 == 0) {
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1] - 1]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] - 1]);
            }
            else {
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] - 1][hex[1] + 1]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1]]);
                neighbours.Add(hexaRivi[hex[0] + 1][hex[1] + 1]);
            }
        }
        return neighbours;
    }

    public void setCurrentUnit(UnitsMain unit) {
        if (unit.getSide() == TurnManager.instance.getSide()) {
            currentUnit = unit;
        }       
    }

    public void setDefinetelyRightUnit(UnitsMain unit) {
        currentUnit = unit;
        Debug.Log(unit.transform.position);
        Debug.Log(TurnManager.instance.getSide());
        cam.setPosition(unit.transform);
    }

    public UnitsMain getCurrentUnit() {
        return currentUnit;
    }

    private bool isNodeInList(List<node> list, node tryMe) {

        for (int i = 0; i < list.Count; i++) {
            if (list[i].current == tryMe.current)
                return true;
        }
        return false;
    }

    private node getClosestToDestination(List<node> lista){
        float tempDistance = 9999;
        node temp = lista[0];

        for (int i = 0; i < lista.Count; i++) {
            if (lista[i].distance < tempDistance) {
                tempDistance = lista[i].distance;
                temp = lista[i];
            }
        }
        return temp;
    }

    public void setPath() {
        path = FindPath(); 
    }

    private List<node> FindPath() {

        bool pathFound= false;

        List<node> openList = new List<node>();
        List<node> closedList = new List<node>();

        float hDistance = (destination - currentUnit.transform.position).magnitude;

        openList.Add(new node(getHexInPosition(currentUnit.transform.position),null, hDistance ,0));

        while (openList.Count > 0) {

            node current = getClosestToDestination(openList);
            openList.Remove(current);
            closedList.Add(current);
            
            if (getHexInPosition(current.current.transform.position).Equals(getHexInPosition(destination))){
                pathFound = true;
                break;
            }

            for (int i = 0; i < getHexNeighbours(current.current.transform.position).Count; i++) {
                hDistance = (destination - getHexNeighbours(current.current.transform.position)[i].transform.position).magnitude;
                node temp = new node(getHexNeighbours(current.current.transform.position)[i],current, hDistance, getHexNeighbours(current.current.transform.position)[i].getMovementCost() + current.gDistance);

                if (!isNodeInList(closedList, temp) && canImoveThere(temp) && temp.current.isItPassable()) {
                    if (!isNodeInList(openList, temp)) {
                        openList.Add(temp);
                    }
                    else {
                        if (current.previous.gDistance + temp.current.getMovementCost() < current.gDistance) {
                            temp.previous = current.previous;
                        }
                    }
                }
            }
        }
        if (pathFound) {

            List<node> path = new List<node>();

            node current = closedList[closedList.Count - 1];

            while (current.previous != null) {
                path.Add(current);
                current = current.previous;
            }

            return path;
        }
        Debug.Log("This is bug area");
        return null;        
    }

    private bool canImoveThere(node temp) {

        if (!temp.current.isHexOccupied())
            return true;
        else if (temp.current.isHexOccupied() && temp.current.isHexOccupiedByEnemy())
            return true;
        else
            return false;
    }

    public void setDestination(Vector3 target) {
       this.destination = target;
    }

    public Vector3 getDestination() {
        return this.destination;
    }

    public void drawPath() {
        for (int i = 0; i < path.Count; i++) {
            GameObject temp = Instantiate(pathMark, path[i].current.transform.position, pathMark.transform.rotation) as GameObject;
            pathMarks.Add(temp);
        }
    }

    public void clearPath() {
        path.Clear();

        for (int i = 0; i < pathMarks.Count;i++ ) {
            Destroy(pathMarks[i]);
        }
        pathMarks.Clear();
    }

    public void moveUnit(RaycastHit info) {
        int howFar = currentUnit.getMovementLeft();
        int moveCosted = 0;

        getHexInPosition(currentUnit.transform.position).setUnitInHex(null);

        if (howFar != 0) { 
             for (int i = 0; i < path.Count; i++) {
                 if (path[path.Count - 1 - i].current.Equals(getHexInPosition(info.point))){
                      currentUnit.transform.position = path[path.Count - 1 - i].current.transform.position;
                      moveCosted = (int) path[path.Count - 1 - i].gDistance;
                      break;
                 }            
                 if (path[path.Count - 1 - i].gDistance == howFar) {
                     currentUnit.transform.position = path[path.Count - 1 - i].current.transform.position;
                     moveCosted = (int) path[path.Count - 1 - i].gDistance;
                     break;
                 }
             }
        }
        currentUnit.setMovementsRemaining(howFar - moveCosted);

        if (getHexInPosition(currentUnit.transform.position).isHexOccupied() && getHexInPosition(currentUnit.transform.position).isHexOccupiedByEnemy()) {

            StartCoroutine(loadFiteScene());

        }
        else {
            getHexInPosition(currentUnit.transform.position).setUnitInHex(currentUnit);
        }
        

        TurnManager.instance.updateUnitButton();
    }

    public bool isMouseOnPath(RaycastHit info) {

        for (int i=0; i< path.Count; i++){
            if (getHexInPosition(info.point).Equals(getHexInPosition(path[i].current.transform.position))) {
            return true;
            }
        }

        return false;
    }

    private IEnumerator loadFiteScene() {
       yield return new WaitForEndOfFrame();

       Application.LoadLevelAdditive("fiteScene");

       Debug.Log(currentUnit.getUnitType());

        List<UnitsMain> temp = new List<UnitsMain>();
        temp.Add(currentUnit);

        PassInformation.instance.setrightUnits(temp);

        temp = new List<UnitsMain>();
        temp.Add(getHexInPosition(currentUnit.transform.position).getUnitInHex());
        
        PassInformation.instance.setLeftUnits(temp);


      PassInformation.instance.MainStage.SetActive(false);
    }

    public void clearTargetAndDestination() {
        destination = Vector3.zero;
        currentUnit = null;
    }
}
