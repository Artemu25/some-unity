using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;
    public float speedMove = 3;
    public float speedRotate = 180;
    public Gun gun;

    public float minY = -20;
    public float maxY = 20;
    private float currentY;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentY = Camera.main.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            float vertical = Input.GetAxis("Vertical");
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            if (vertical != 0)
            {
                controller.Move(transform.forward * vertical * speedMove * Time.deltaTime);
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            if (mx != 0)
            {
                transform.Rotate(transform.up * mx * speedRotate * Time.deltaTime);
            }
            if (my != 0)
            {
                currentY = Mathf.Clamp(currentY - my * speedRotate * Time.deltaTime, minY, maxY);
                Vector3 camrot = Camera.main.transform.rotation.eulerAngles;
                Camera.main.transform.rotation = Quaternion.Euler(currentY, camrot.y, camrot.z);
            }

            if (Input.GetMouseButton(0))
            {
                gun.Shoot();
                animator.SetBool("Shoot", true);
            }
            else
            {
                animator.SetBool("Shoot", false);
            }
        }
        controller.Move(Physics.gravity * Time.deltaTime);
    }

    public void damage()
    {

    }
}
