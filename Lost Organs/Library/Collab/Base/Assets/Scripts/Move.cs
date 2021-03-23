using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 walk;
    Quaternion rotation;

    public float speed = 6.2f;

    void Start()
    {
        walk = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("AttackTrigger");
        }

        //Basinda eksi var!!!
        walk.x = -Input.GetAxis("Horizontal");
        walk.z = -Input.GetAxis("Vertical");
        transform.position += walk * Time.deltaTime * speed;


        if (walk.magnitude != 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Moving", true);
            rotation = Quaternion.LookRotation(walk);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);
        }

        else
            transform.GetChild(0).GetComponent<Animator>().SetBool("Moving", false);
    }
}
