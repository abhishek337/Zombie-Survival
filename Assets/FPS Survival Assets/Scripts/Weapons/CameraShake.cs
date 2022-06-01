using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator shake(float durartion, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float shakeTime = 0.0f;

        while(shakeTime < durartion)
        {
            float x = Random.Range(-.5f, .5f) * magnitude;
            float y = Random.Range(-.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            shakeTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
