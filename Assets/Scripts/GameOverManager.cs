using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene(0);
    }
}
