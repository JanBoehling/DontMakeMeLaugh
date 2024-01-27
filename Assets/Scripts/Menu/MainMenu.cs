using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _startCanvas;
    [SerializeField] 
    private GameObject _settingsCanvas;
    [SerializeField]
    private GameObject _creditsCanvas;
    [SerializeField]
    private CinemachineDollyCart _dollyCart;
    [SerializeField]
    private AudioMixer _mixer;

    private int _currentPage = 0;

    public void QuitGame()
    {
#if UNITY_EDITOR
        Application.Quit();
#else
        Application.Quit();
#endif
    }

    public void LateUpdate()
    {
        if(_dollyCart.m_Path.MaxPos - 1f <= _dollyCart.m_Position)
        {
            // Start Game 1 (RockPaperSiccors)
        }
    }

    public void OpenUrl(string url)
    {
        try
        {
            System.Diagnostics.Process.Start(url);
        }
        catch (System.Exception)
        {
            Debug.LogError("Could not open URL");
        }
    }

    public void SetVolume(float volume) => _mixer.SetFloat("MasterVolume", volume);

    public void OnStartPressed()
    {
        _dollyCart.m_Speed = 0.1f;
        DisableAllPages();
    }

    public void ChangePage(int index)
    {
        _currentPage = index;
        DisableAllPages();
        switch (_currentPage)
        {
            case 0: // Start Menu
                ChangeToStartPage();
                break;
            case 1: // Settings
                ChangeToSettingsPage();
                break;
            case 2: // Credits
                ChangeToCreditsPage();
                break;
        }
    }

    private void DisableAllPages()
    {
        _startCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);
        _creditsCanvas.SetActive(false);
    }

    private void ChangeToStartPage()
    {
        _startCanvas.SetActive(true);
    }

    private void ChangeToSettingsPage()
    {
        _settingsCanvas.SetActive(true);
    }

    private void ChangeToCreditsPage()
    {
        _creditsCanvas.SetActive(true);
    }
}
