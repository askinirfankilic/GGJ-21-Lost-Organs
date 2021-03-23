using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Sword;
    public GameObject PickUpSword;
    public GameObject cam;
    
    Vector3 walk;
    Quaternion rotation;

    public float armedSpeed = 6.2f;
    public float swordedSpeed = 6.2f;

    public bool swordIsStolen = false;
    bool gameOver = false;

    Vector3[] spawnPoints;

    AudioSource au;
    Animator animator;

    void Start()
    {
        walk = Vector3.zero;
        au = GetComponent<AudioSource>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        spawnPoints = new Vector3[10];
        spawnPoints[0] = new Vector3(58, 1, 75.6f);
        spawnPoints[1] = new Vector3(44.4f, 1, 96);
        spawnPoints[2] = new Vector3(49.5f, 1, 106.5f);
        spawnPoints[3] = new Vector3(43, 1, 72.4f);
        spawnPoints[4] = new Vector3(10, 1.24f, 74.6f);
        spawnPoints[5] = new Vector3(12, 1.24f, 88.8f);
        spawnPoints[6] = new Vector3(25, 1.24f, 106.8f);
        spawnPoints[7] = new Vector3(3.4f, 1.24f, 111.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && swordIsStolen == false)
            {
                if(!Sword.GetComponent<SwordHit>().hit)
                {
                    animator.SetTrigger("AttackTrigger");
                    StartCoroutine(HitFinish());
                }
            }

            //Basinda eksi var!!!

            walk.x = -Input.GetAxis("Horizontal");
            walk.z = -Input.GetAxis("Vertical");


            if (swordIsStolen == false)
            {
                transform.position += walk * Time.deltaTime * swordedSpeed;

                if (walk.magnitude != 0)
                {
                    animator.SetBool("Moving", true);
                    if (!au.isPlaying)
                    {
                        au.Play();
                    }
                    rotation = Quaternion.LookRotation(walk);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);
                }

                else
                {
                    animator.SetBool("Moving", false);
                    au.Stop();
                }

            }
            else if (swordIsStolen == true)
            {
                transform.position += walk * Time.deltaTime * armedSpeed;
                if (walk.magnitude != 0)
                {
                    animator.SetBool("Moving", true);
                    if (!au.isPlaying)
                    {
                        au.Play();
                    }
                       
                    rotation = Quaternion.LookRotation(walk);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);
                }

                else
                {
                    animator.SetBool("Moving", false);
                    au.Stop();
                }
            }
        }
    }

    private IEnumerator HitFinish()
    {
        Sword.GetComponent<SwordHit>().hit = true;
        yield return new WaitForSeconds(0.2f);
        Sword.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.1f);
        Sword.GetComponent<SwordHit>().hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            if (swordIsStolen)
            {
                if (!gameOver)
                    GameManager.gm.GameOver();
                gameOver = true;
                animator.SetTrigger("Die");
                au.Stop();
                transform.GetChild(2).gameObject.SetActive(false);

                Destroy(collision.collider.gameObject);
            }
            else //unarmed
            {
                swordIsStolen = true;
                animator.SetBool("LostSword", true);
                au.pitch = 1.25f;

                //Kilici birak, rastgele bir noktaya koy. Animasyon armed geçiþi.
                Sword.SetActive(false);
                cam.GetComponent<FollowPlayer>().cameraIsShaken = false;
                Instantiate(PickUpSword, spawnPoints[Random.Range(0,8)], Quaternion.identity);
                Destroy(collision.collider.gameObject);
            }
        }
        else if(collision.collider.name == "Sword(Clone)") // back to armed
        {
            swordIsStolen = false;
            animator.SetBool("LostSword", false);
            au.pitch = 1;

            Sword.SetActive(true);
            Destroy(collision.collider.gameObject);
        }
    }
}
