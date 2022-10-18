using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    int health = 75;                // здоровье
    int addHealth = 5;              // + к здоровью
    float timeHealing = 0.5f;       // время одного курса лечения
    float maxTimeHealing = 3.0f;    // общее время курса лечения
    int maxHealth = 100;            // максимальное здоровье

    void Start()
    {
        StopAllCoroutines();
        ReceiveHealing(health);
    }

    public void ReceiveHealing(int health)
    {
        StartCoroutine(HealingCoroutine(health));
    }

    IEnumerator HealingCoroutine(int health)
    {
        float _allTimeHealing = 0;
        Debug.Log($"Before yield - {health}");
        // если время лечения еще не вышло + здоровье не выше максимума, то лечим
        while (_allTimeHealing <= maxTimeHealing && health + addHealth <= maxHealth)
        {
            health += addHealth;
            _allTimeHealing += timeHealing;
            yield return new WaitForSeconds(timeHealing);
            Debug.Log($"After yield {health}");
        }
    }
}
