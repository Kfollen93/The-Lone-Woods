using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 5;
    private int currentHealth;
    public OnScreenQuestUi onScreenUi;
    private bool ratIsDead;
    private bool wolfIsDead;

    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            if (!ratIsDead && CompareTag("Rat") && onScreenUi.CurrentQuestUiIndex == 3)
            {
                ratIsDead = true;
                UiCounter.Instance.killCount++;
                UiCounter.Instance.UpdateUIKills();
            }

            if (!wolfIsDead &&CompareTag("Wolf") && onScreenUi.CurrentQuestUiIndex == 5)
            {
                wolfIsDead = true;
                UiCounter.Instance.killCount++;
                UiCounter.Instance.UpdateUIKills();
            }
            StartCoroutine(WaitForBloodAnim());
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator WaitForBloodAnim()
    {
        yield return new WaitForSeconds(0.2f);
        Dead();
    }
}
