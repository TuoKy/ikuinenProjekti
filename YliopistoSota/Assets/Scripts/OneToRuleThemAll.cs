using UnityEngine;
using System.Collections;

public class OneToRuleThemAll : MonoBehaviour {

    private static OneToRuleThemAll _instance;
    public static OneToRuleThemAll instance {
        get {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<OneToRuleThemAll>();
            return _instance;
        }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        if (_instance != null) {
            Destroy(gameObject);
        }
    }
}
