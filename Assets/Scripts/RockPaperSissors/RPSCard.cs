using Unity.VisualScripting;
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

    public GameObject printQuad;

    public UnityEvent<Transform> OnInteracted => onInteracted;
    private UnityEvent<Transform> onInteracted = new UnityEvent<Transform>();

    public void ChangeCardType()
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
