using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeHexaGrid : MonoBehaviour {

    //TEMP
    public GameObject pathMark;

    public List<Renderer> inPassables = new List<Renderer>();
    public GameObject hexa;
    public Transform vanhemmat;

    public int xPituus;
    public int yPituus;

    private float xAxis;
    private float yAxis;
    private int vuorottelija = 1;
    private List<HexBehavior> hexaList = new List<HexBehavior>();
	
    // Use this for initialization
	void Start () {
        setHexes();
        setNonPassables();
       // test();
	}
	// Update is called once per frame
	void Update () {
	}

    private void setHexes(){
        //piirrä ruudukko
        xAxis = 1;
        yAxis = 1;

        while (yPituus > yAxis) {

            while (xPituus > xAxis) {

                GameObject tempX;

                if (vuorottelija % 2 == 0) {
                    tempX = Instantiate(hexa, new Vector3(xAxis + 1, 20f, yAxis), hexa.transform.rotation) as GameObject;
                }
                else {
                    tempX = Instantiate(hexa, new Vector3(xAxis, 20f, yAxis), hexa.transform.rotation) as GameObject;

                }
                hexaList.Add(tempX.GetComponent<HexBehavior>());
                // laitetaan hexat tyhjän objectin lapsiksi
                tempX.transform.parent = vanhemmat;
                // X suunnassa heksojen etäisyys toisistaan
                xAxis += +2.0f;
                // parilliset ja parittomat rivit

            }
            // Y suunnassa kappaleiden etäisyys
            yAxis += 1.5f;
            // alustetaan mistä rivi alkaa              
            xAxis = 1;
            GameManager.instance.hexaRivi.Add(hexaList);
            hexaList = new List<HexBehavior>();
            vuorottelija++;
        }
    }

    private void setNonPassables(){


        //Talot
        for (int i = 0; i < inPassables.Count; i++) {            
            for (int o = 0; o < GameManager.instance.hexaRivi.Count; o++) {
                for (int p = 0; p < GameManager.instance.hexaRivi[o].Count; p++) {
                    if (inPassables[i].bounds.Contains(GameManager.instance.hexaRivi[o][p].transform.position)) {
                        GameManager.instance.hexaRivi[o][p].setPassable(false);                      
                    }
                }
            }
        }

        //joki perkele
        int y = 0;
        int x = 41, xTemp = x;
        float actualX = 41;
        int toistoja = 9;

        //saadaan suurinpiirtein iso osa joesta
        while(y < 64){
            xTemp = x;
            toistoja = 9;
            while (toistoja > 0) {
                if (y == 24) {
                    y = 33;
                    actualX = actualX - 0.65f * 9;
                    break;
                }

                GameManager.instance.hexaRivi[y][xTemp].setPassable(false);

                toistoja--;
                xTemp++;
            }
            actualX = actualX - 0.65f;
            x = Mathf.RoundToInt(actualX);
            y++;
        }

        //Hienosäädöt päistä
        for (int i = 64; i < 66; i++)
            for (int p = 0; p < 7; p++) 
                GameManager.instance.hexaRivi[i][p].setPassable(false);
                           
        //sillan reunat
        for (int p = 0; p < 2; p++) 
            for (int i = 0; i < 7; i++) 
                GameManager.instance.hexaRivi[p + 24][i + 27].setPassable(false);
        for (int i = 0; i < 6; i++) 
            GameManager.instance.hexaRivi[26][i + 28].setPassable(false);
        for (int i = 0; i < 5; i++) 
            GameManager.instance.hexaRivi[27][i + 28].setPassable(false);
        for (int i = 0; i < 4; i++) 
            GameManager.instance.hexaRivi[28][i + 29].setPassable(false);
        for (int i = 0; i < 3; i++) 
            GameManager.instance.hexaRivi[29][i + 29].setPassable(false);
        GameManager.instance.hexaRivi[30][30].setPassable(false);
        GameManager.instance.hexaRivi[27][23].setPassable(false);
        for (int i = 0; i < 2; i++) 
            GameManager.instance.hexaRivi[28][i + 23].setPassable(false);
        for (int i = 0; i < 2; i++) 
            GameManager.instance.hexaRivi[29][i + 23].setPassable(false);
        for (int i = 0; i < 3; i++) 
            GameManager.instance.hexaRivi[30][i + 23].setPassable(false);
        for (int i = 0; i < 5; i++) 
            GameManager.instance.hexaRivi[31][i + 21].setPassable(false);
        for (int i = 0; i < 7; i++) 
            GameManager.instance.hexaRivi[32][i + 20].setPassable(false);
        for (int i = 0; i < 8; i++) 
            GameManager.instance.hexaRivi[33][i + 19].setPassable(false);   
        
    }

    private void test() {
        for (int o = 0; o < GameManager.instance.hexaRivi.Count; o++) {
            for (int p = 0; p < GameManager.instance.hexaRivi[o].Count; p++) {
                if (!GameManager.instance.hexaRivi[o][p].isItPassable()) {
                    Instantiate(pathMark, GameManager.instance.hexaRivi[o][p].transform.position, pathMark.transform.rotation);
                }
            }
        }
    }

}
