using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;

    public GameObject PlayerVisual;

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(fixedJoystick.Horizontal * speed, rb.velocity.y, fixedJoystick.Vertical * speed);

        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity);
            float turnSpeed = 10;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("isPunching");
        }
    }
}