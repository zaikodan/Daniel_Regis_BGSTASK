using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;

    bool interacting;
    IInteractable closeInteractable = null;
    Vector2 direction;
    Rigidbody2D rb;
    Animator animator;
    Inventory inventory;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        CheckInputs();
        Move();
    }

    private void CheckInputs()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        if (Input.GetAxis("Interact") == 1f)
        {
            Interact();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            inventory.OpenInventory();
        }
    }

    private void CheckDirection()
    {
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(Vector2.zero);
        }
        else if(direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(Vector2.up * 180);
        }
    }

    private void Move()
    {
        if (!interacting)
        {
            rb.velocity = moveSpeed * direction;

            animator.SetBool("Running", direction != Vector2.zero);
            CheckDirection();
        }
    }

    private void Interact()
    {
        if(closeInteractable != null && !interacting)
        {
            rb.velocity = Vector2.zero;
            interacting = true;
            closeInteractable.Interact();
        }
    }

    public void EndInteraction()
    {
        interacting= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        closeInteractable = collision.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        closeInteractable = null;
    }
}
