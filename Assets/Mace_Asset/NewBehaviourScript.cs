using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] test;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadSceneASync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnLoadScene(string sceneName)
    {
        Debug.Log("wall Collided");

        for (int i = 0; i < test.Length; i++)
        {
            DontDestroyOnLoad(test[i].gameObject);
        }

        SceneManager.UnloadSceneAsync(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level Change")
        {
            LoadSceneASync("Game");
        }
        if (other.tag == "Destroy")
        {
            UnLoadScene("SampleScene");
        }
        if (other.tag == "Level Change_1")
        {
            LoadSceneASync("Game1");
        }
        if (other.tag == "Destroy_1")
        {
            UnLoadScene("Game");
        }
    }
}