using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager sinstance;
    public static GameManager Instance
    {
        get
        {
            if (sinstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sinstance = newGameObject.AddComponent<GameManager>();
            }
            return sinstance;
        }
    }

    public int changeScene = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       // print("Call");
    }

    public void ChangeScene(string _Name)
    {
        SceneManager.LoadScene(_Name);
    }
}
