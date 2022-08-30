using UnityEngine;

public class Observer : MonoBehaviour, IObserver
{
    //1

    public void Activate()
    {
        Debug.Log("Action activated"); 
    }


    //2

    private Subject _subject;

    private void OnEnable()
    {
        _subject.OnAction += ActivateAction;
    }

    private void ActivateAction()
    {
        Debug.Log("Action activated");
    }
}
