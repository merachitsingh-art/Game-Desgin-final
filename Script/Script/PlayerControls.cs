using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private bool isMoving; 
    private Vector2 input;

    private Animator animator;

    public LayerMask SolidObjectsLayer;
    public LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            // Prevents moving diagonally
            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                
                // Calculate destination position
                var targetPos = transform.position;
                targetPos.x += input.x * 0.1f;
                targetPos.y += input.y * 0.1f;

               
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }

            // Interact key check
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Interact();
            }
        } 
        
        animator.SetBool("isMoving", isMoving);
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            var npc = collider.GetComponent<NPCController>();
            if (npc != null)
            {
                npc.Interact();
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        
        if (Physics2D.OverlapCircle(targetPos, 0.2f, SolidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
}
