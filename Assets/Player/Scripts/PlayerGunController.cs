using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("Rotation parameters")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform gunTransform;
    [SerializeField] float rotationSpeed = 5f;


    [Header("Shooting parameters")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletsSpawnPosition;
    public bool isShooting = false;

    [Header("Upgradable parameters")]
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] float critChance = 0.05f;
    [SerializeField] float damage = 10f;
    [SerializeField] float bulletAngleSpread = 10f;
    [SerializeField] int bulletsAtTheTime = 1;


    public List<float> GetGunData()
    {
        List<float> dataList = new List<float>
        {
            shootDelay,
            critChance,
            damage,
            bulletAngleSpread,
            bulletsAtTheTime,
            0f // empty slot for future)
        };

        return dataList;
    } 

    public void UpgradeRandomParameter()
    {
        int randomParameterNum = Random.Range(0, 5);

        switch (randomParameterNum)
        {
            case 0:
                shootDelay *= 0.95f;
                break;
            case 1:
                critChance += 0.05f;
                break;
            case 2:
                damage += 10f;
                break;
            case 3:
                bulletAngleSpread *= 0.9f;
                break;
            case 4:
                bulletsAtTheTime += 1;
                break;
            default:
                break;
        }
    }


    Vector3 mousePos;

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, gunTransform.position.z - Camera.main.transform.position.z));
        RotatePlayerWithMouse();
        RotateGunWithMouse();

        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(Shooting());
            Invoke("isShootingReset", shootDelay);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }

    void isShootingReset()
    {
        isShooting = false;
    }

    IEnumerator Shooting()
    {
        while (Input.GetMouseButton(0))
        {
            for (int i = 0; i < bulletsAtTheTime; i++) {
                Vector3 direction = (mousePos - gunTransform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + Random.Range(-bulletAngleSpread, bulletAngleSpread);

                Instantiate(bulletPrefab, bulletsSpawnPosition.position, Quaternion.AngleAxis(angle, Vector3.forward));
            }

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
