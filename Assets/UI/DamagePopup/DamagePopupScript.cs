using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DamagePopupScript : MonoBehaviour
{
    private TMP_Text textMesh;
    private Color textColor;
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] float destroySpeed = 3f;
    private float destroyTimer;
    [SerializeField] Vector3 moveVector;

    [SerializeField] Color standartColor;
    [SerializeField] Color criticalColor;
    [SerializeField] Color scoreColor;

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 3f * Time.deltaTime;

        if(destroyTimer > destroyDelay * 0.2f)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        } else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }

        destroyTimer -= Time.deltaTime;
        if(destroyTimer < 0)
        {
            textColor.a -= destroySpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a <= 0 )
            {
                Destroy(gameObject);
            }
        }
    }

    public static DamagePopupScript Create(GameObject damagePopupPrefab, Vector3 position, string damage, bool isCriticalHit, bool isScorePopup)
    {
        Vector3 spawnPosition = new Vector3(position.x + Random.Range(-0.3f, 0.3f), position.y + Random.Range(0.1f, 0.5f), 0);

        DamagePopupScript damagePopupRef = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity).GetComponent<DamagePopupScript>();
        damagePopupRef.Setup(damage, isCriticalHit, isScorePopup);

        return damagePopupRef;
    }

    public void Setup(string damage, bool isCriticalHit, bool isScorePopup)
    {
        textMesh.SetText(damage);

        if(isScorePopup)
        {
            textMesh.fontSize = Mathf.FloorToInt(textMesh.fontSize * 1.6f);
            textColor = scoreColor;
        } else if(isCriticalHit)
        {
            textMesh.fontSize = Mathf.FloorToInt(textMesh.fontSize * 1.2f);
            textColor = criticalColor;
        } else
        {
            textColor = standartColor;
        }

        textMesh.color = textColor;
        destroyTimer = destroyDelay;
    }
}
