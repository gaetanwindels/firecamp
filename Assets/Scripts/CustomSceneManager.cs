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
        SceneManager.LoadScene("Game Scene");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}