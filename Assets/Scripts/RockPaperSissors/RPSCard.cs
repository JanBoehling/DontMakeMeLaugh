using UnityEngine;
using UnityEngine.Events;

public class RPSCard : MonoBehaviour, IInteractable
{
    [SerializeField]
    public Texture2D rockPrint;

    [SerializeField]
    public Texture2D paperPrint;

    [SerializeField]
    public Texture2D scissorPrint;

    [SerializeField]
    public RPSCardTypes cardType;

    [SerializeField]
    public GameObject printQuad;

    public UnityEvent OnInteracted { get; } = new UnityEvent();

    UnityEvent<Transform> IInteractable.OnInteracted => throw new System.NotImplementedException();

    private void Update()
    {
        switch(cardType)
        {
            case RPSCardTypes.Rock:
                printQuad.GetComponent<Renderer>().material.mainTexture = rockPrint;
                break; 
            case RPSCardTypes.Paper:
                printQuad.GetComponent<Renderer>().material.mainTexture = paperPrint;
                break; 
            case RPSCardTypes.Scissors:
                printQuad.GetComponent<Renderer>().material.mainTexture = scissorPrint;
                break;
        }
    }
}
