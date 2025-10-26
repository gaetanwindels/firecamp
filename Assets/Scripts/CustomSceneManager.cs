using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {

    }

    public void GoToPlay()
    {
        StartCoroutine(LoadScene());
    }
    
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Game Scene");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}