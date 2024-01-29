using UnityEngine;

public class EnemyMutation : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] normalSprites;
    [SerializeField] Sprite[] mutatedSprites;

    private bool CheckMutation(float chance)
    {
        return Random.Range(0f, 1f) <= chance;
    }

    public void ApplyMutationChance(float mutationChance)
    {
        for (int i = 0; i < mutatedSprites.Length; i++)
        {
            if(CheckMutation(mutationChance))
            {
                normalSprites[i].sprite = mutatedSprites[i];
            }
        }
    }
}
