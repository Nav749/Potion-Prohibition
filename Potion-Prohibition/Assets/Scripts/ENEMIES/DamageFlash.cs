using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;

    private SpriteRenderer spriteRenderers;
    private Material materials;

    private Coroutine damageFlashCoroutine;

    private void Start()
    {
        spriteRenderers = GetComponent<SpriteRenderer>();
        materials = spriteRenderers.material;
    }

    public void CallDamageFlash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColour();

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while ((elapsedTime < flashTime))
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColour()
    {
        materials.SetColor("_FkashColour", flashColor);
    }

    private void SetFlashAmount(float amount)
    {
        materials.SetFloat("_FlashAmount", amount);
    }
}
