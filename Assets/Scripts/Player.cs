using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float z_MoveSpeed = 1;
    private BoxCollider2D z_BoxCollider;
    private Animator z_Animator;

    private bool isFrozen;

    private void Start()
    {
        z_BoxCollider = GetComponent<BoxCollider2D>();
        z_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDelta = new Vector2(moveX, moveY);

        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        RaycastHit2D castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(moveX, 0), Mathf.Abs(moveX * Time.deltaTime * z_MoveSpeed), LayerMask.GetMask("Enemy", "BlockMove"));
        if (castResult.collider)
        {
            moveDelta.x = 0;
        }

        castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(0, moveY), Mathf.Abs(moveY * Time.deltaTime * z_MoveSpeed), LayerMask.GetMask("Enemy", "BlockMove"));
        if (castResult.collider)
        {
            moveDelta.y = 0;
        }

        bool isWalking = moveDelta.magnitude > 0;
        z_Animator.SetBool("IsWalking", isWalking);

        transform.Translate(moveDelta * Time.fixedDeltaTime * z_MoveSpeed);
    }

    public void FreezePlayer()
    {
        isFrozen = true;
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;
    }
}
