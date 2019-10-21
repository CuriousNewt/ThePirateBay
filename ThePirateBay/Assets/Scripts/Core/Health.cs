using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public bool isDead = false;


    public void TakeDamage(float damage) {
        health = Mathf.Max(health - damage, 0);
        if (health == 0) {
            Die();
        }
    }

    void Die() {
        if (isDead) return;

        isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }
}
