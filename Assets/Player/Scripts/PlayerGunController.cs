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
    [SerializeField] int bulletsPenetration = 1;

    int[] paramsLevels = new int[] { 1, 1, 1, 1, 1, 1 };

    public List<float> GetGunData()
    {
        List<float> dataList = new List<float>
        {
            shootDelay,
            critChance,
            damage,
            bulletAngleSpread,
            bulletsAtTheTime,
            bulletsPenetration // empty slot for future)
        };

        return dataList;
    } 

    public void UpgradeRandomParameter()
    {
        int randomDistNum = Random.Range(0, 100);
        int parameterNum;

        if(randomDistNum < 30) // 30% chance
        {
            parameterNum = 0; 
        } else if(randomDistNum < 60) // 30% chance
        {
            parameterNum = 1;
        } else if(randomDistNum < 80) // 20% chance
        {
            parameterNum = 2;
        } else if(randomDistNum < 90) // 10% chance
        {
            parameterNum = 3;
        } else if (randomDistNum < 95) // 5% chance
        {
            parameterNum = 4;
        }
        else // 5% chance
        {
            parameterNum = 5;
        } 
        


        switch (parameterNum)
        {
            case 0:
                shootDelay *= Mathf.Pow(0.8f, paramsLevels[parameterNum]);
                break;
            case 1:
                damage *= Mathf.Pow(1.85f, 1f / paramsLevels[parameterNum]);
                break;
            case 2:
                bulletAngleSpread = 10f * Mathf.Pow(0.95f, paramsLevels[parameterNum]);
                break;
            case 3:
                critChance *= Mathf.Pow(2f, 1f / paramsLevels[parameterNum]);
                break;
            case 4:
                bulletsAtTheTime += 1;
                damage *= 0.8f;
                bulletAngleSpread *= 1.2f;
                break;
            case 5:
                bulletsPenetration += 1;
                damage *= 0.9f;
                shootDelay *= 1.1f;
                break;
            default:
                break;
        }

        paramsLevels[parameterNum]++;
    }

    public void UpgradeParameter(int parameterNum)
    {
        switch (parameterNum)
        {
            case 0:
                shootDelay *= Mathf.Pow(0.8f, 1f / paramsLevels[parameterNum]);
                break;
            case 1:
                damage *= Mathf.Pow(2f, 1f / paramsLevels[parameterNum]);
                break;
            case 2:
                bulletAngleSpread *= Mathf.Pow(0.7f, 1f / paramsLevels[parameterNum]);
                break;
            case 3:
                critChance *= Mathf.Pow(2f, 1f / paramsLevels[parameterNum]);
                break;
            case 4:
                bulletsAtTheTime += 1;
                damage *= 0.8f;
                bulletAngleSpread *= 1.2f;
                break;
            case 5:
                bulletsPenetration += 1;
                damage *= 0.9f;
                shootDelay *= 1.2f;
                break;
            default:
                break;
        }

        paramsLevels[parameterNum]++;
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

                ProjectileScript bullet = Instantiate(bulletPrefab, bulletsSpawnPosition.position, Quaternion.AngleAxis(angle, Vector3.forward)).GetComponent<ProjectileScript>();
                bullet.SetParameters(damage, critChance, bulletsPenetration);
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
