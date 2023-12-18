using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float moveMagnitude = playerController.CurMovementInput.magnitude;

        // walk �ִϸ��̼�
        if (moveMagnitude > 0.01)
        {
            animator.SetFloat("Speed", moveMagnitude);
        }
        // idle �ִϸ��̼�
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
