using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorAnimated : MonoBehaviour ,IDoor
{
    private Animator animator;
    private bool isOpen = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        animator.SetBool("Open",true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open",false);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
    }
}

