using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject decal;
    public AudioSource audio;
    public float waitTime = 0.15f;
    private float wait = 0;


	public void Shoot()
    {
        if (wait <= 0)
        {
            wait = waitTime;
            audio.Play();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.transform.SendMessage("damage");
                }else
                {
                    GameObject curDecal = Instantiate<GameObject>(decal);
                    curDecal.transform.position = hit.point + hit.normal * 0.01f;
                    curDecal.transform.rotation = Quaternion.LookRotation(-hit.normal);
                    curDecal.transform.SetParent(hit.transform);

                    Rigidbody r = hit.transform.GetComponent<Rigidbody>();
                    if (r != null)
                    {
                        r.AddForceAtPosition(-hit.normal * .25f, hit.transform.InverseTransformPoint(hit.point), ForceMode.Impulse);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (wait>0)
        {
            wait -= Time.deltaTime;
        }
    }
}
