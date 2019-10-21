using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead) return;
        if (CombatMove()) return;
        if (InteractMove()) return;
        if (Move()) return;
    }

    bool CombatMove()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue;

            GameObject targetGO = target.gameObject;
            if (!GetComponent<Fighter>().CanAttack(targetGO)) continue;

            if (Input.GetMouseButton(1))
            {
                GetComponent<Fighter>().Attack(targetGO);
            }
            return true;
        }
        return false;
    }

    bool InteractMove()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.GetComponent<IInteraction>() == null) continue;

            GameObject targetGO = hit.transform.gameObject;
            if (targetGO == null) continue;

            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Interactor>().Interact(targetGO);
            }
            return true;
        }
        return false;
    }

    bool Move()
    {
        if (Input.GetMouseButton(1)) {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                GetComponent<Mover>().StartMoveAction(hit.point);
            }
            return true;
        }
        return false;
    }

    static Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
