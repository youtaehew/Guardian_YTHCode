using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfEnemyAnimationManager : MonoBehaviour
{
    ElfEnemy elfEnemy;

    private void Awake()
    {
        elfEnemy = transform.root.GetComponent<ElfEnemy>();
    }
    private void FireSlash(int type)
    {
        elfEnemy.FireSlash(type);
    }
    private void FireLine()
    {
        elfEnemy.FireLine();
    }
    private void Prick()
    {
        elfEnemy.Prick();
    }
}
