using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    // Reference to the flashbang prefab with a light component
    public GameObject flashbangPrefab;
    public float flashbangDuration = 3f;
    public float throwDistance = 2f;
    public float flashIntensity = 5f;

    private bool isFlashbangActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFlashbangActive)
        {
            ThrowFlashbang();
        }
    }

    void ThrowFlashbang()
    {
        Vector3 throwPosition = transform.position + transform.forward * throwDistance;
        GameObject flashbang = Instantiate(flashbangPrefab, throwPosition, transform.rotation);
        flashbang.GetComponent<Rigidbody>().velocity = throwPosition * 10;
        Destroy(flashbang, flashbangDuration);

        StartCoroutine(EnableFlashbangEffect());
    }

    IEnumerator EnableFlashbangEffect()
    {
        isFlashbangActive = true;

        SetScreenBrightness(flashIntensity);

        yield return new WaitForSeconds(flashbangDuration);

        SetScreenBrightness(1f);

        isFlashbangActive = false;
    }

    void SetScreenBrightness(float brightness)
    {
        Material material = new Material(Shader.Find("Standard"));

        material.color = new Color(brightness, brightness, brightness, 1f);

        Graphics.Blit(null, material);
    }
}



/* Che miggy, para este tenes que crear un GameObject nuevo y añadirle un Light Component
   Con ese(que es tu flashbang prefab), asignas a la variable flashbangPrefab en el inspector.
   ajusta si queres los parametros pq medio overpowered me salio.
 */