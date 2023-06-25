using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;

    private UnityAction overAction;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void GoAhead(UnityAction action)
    {
        animator.SetTrigger("GoAhead");
        overAction = action;
    }
    public void GoBack(UnityAction action)
    {
        animator.SetTrigger("GoBack");
        overAction = action;
    }
    
    public void PlayOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
}
