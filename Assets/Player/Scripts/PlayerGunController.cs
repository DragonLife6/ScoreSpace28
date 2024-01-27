using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("Rotation parameters")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform gunTransform;
    [SerializeField] float rotationSpeed = 5f;


    [Header("Shooting parameters")]
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletsSpawnPosition;


    Vector3 mousePos;

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gunTransform.position.z - Camera.main.transform.position.z));
        RotatePlayerWithMouse();
        RotateGunWithMouse();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shooting());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            Vector3 direction = (mousePos - gunTransform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Instantiate(bulletPrefab, bulletsSpawnPosition.position, Quaternion.AngleAxis(angle, Vector3.forward));

            yield return new WaitForSeconds(shootDelay);
        }
    }



    void RotatePlayerWithMouse()
    {
        if ((mousePos.x < playerTransform.position.x && playerTransform.localScale.x > 0) || (mousePos.x > playerTransform.position.x && playerTransform.localScale.x < 0))
        {
            playerTransform.localScale = new Vector3(-playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        }
    }

    void RotateGunWithMouse()
    {
        Vector3 direction = (mousePos - gunTransform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(gunTransform.localScale.x < 0)
        {
            angle += 180f;
        }
        
        if ((mousePos.x < playerTransform.position.x && gunTransform.localScale.x > 0) || (mousePos.x > playerTransform.position.x && gunTransform.localScale.x < 0))
        {
            gunTransform.localScale = new Vector3(-gunTransform.localScale.x, gunTransform.localScale.y, gunTransform.localScale.z);
            gunTransform.rotation = Quaternion.identity;
        } else
        {
            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
        }
    }
}
