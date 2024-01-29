using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class OpenLink : MonoBehaviour
{
    public string url;

    void Start()
    {
        // Add a listener to the button component to handle clicks
        GetComponent<Button>().onClick.AddListener(OpenURL);
    }

    void OpenURL()
    {
        // Open the URL when the button is clicked
        Process.Start(url);

    }
}
