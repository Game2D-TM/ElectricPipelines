using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QRCode : MonoBehaviour
{
    public void StartGame()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void RestartGame()
    {
            SceneManager.LoadScene("MainMenu");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
