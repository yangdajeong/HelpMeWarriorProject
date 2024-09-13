using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void Quit()
    {
        Application.Quit(); 
    }
}
