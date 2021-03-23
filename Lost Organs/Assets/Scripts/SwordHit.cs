using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public bool hit = false;
    public ParticleSystem boomParticle;
    public CameraShake shake;
    public GameObject cam;

    AudioSource au;

    void Start()
    {
        au = transform.GetChild(0).GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" && hit)
        {
            GameManager.gm.EarnPoint();
            au.Play();

            cam.GetComponent<FollowPlayer>().cameraIsShaken = true;
            StartCoroutine(Shake());
            StartCoroutine(shake.Shake(0.3f, 0));
            boomParticle.Play();
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Shake()
    {
        yield return new WaitForSeconds(0.3f);
        cam.GetComponent<FollowPlayer>().cameraIsShaken = false;
    }
}
