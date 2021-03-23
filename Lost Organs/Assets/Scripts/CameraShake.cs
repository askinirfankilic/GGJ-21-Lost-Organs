using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public IEnumerator Shake( float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-0.1f, 0.1f);
            float z = Random.Range(-0.1f, 0.1f);
            transform.localPosition += new Vector3(x, 0, z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
