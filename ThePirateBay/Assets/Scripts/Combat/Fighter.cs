using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IAction
{
    public float weaponRange = 2f;
    public float timeBetweenAttacks = 2f;
    public float weaponDamage = 5f;

    Health target;
    float timeSinceLastAttack = Mathf.Infinity;

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (target == null) return;
        if (target.isDead) return;

        if (!IsInRange())
        {
            GetComponent<Mover>().MoveTo(target.transform.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            MakeAttack();
        }
    }

    void MakeAttack() {
        if (timeSinceLastAttack >= timeBetweenAttacks) {
            transform.LookAt(target.transform);
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
            timeSinceLastAttack = 0;
        }
    }

    bool IsInRange() {
        return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
    }

    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
    }

    public void Cancel() {
        GetComponent<Animator>().ResetTrigger("Attack");
        GetComponent<Animator>().SetTrigger("StopAttack");
        target = null;
    }

    void Hit() {
        if (target == null) return;
        target.TakeDamage(weaponDamage);
    }

    public bool CanAttack(GameObject combatTarget) {
        if (combatTarget == null) return false;
        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.isDead;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }
}
