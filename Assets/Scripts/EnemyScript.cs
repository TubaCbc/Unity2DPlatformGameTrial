using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject enemySaw;
    public Ease myEase;

    void Start()
    {
        enemySaw.GetComponent<Transform>().DOLocalMove(new Vector3(24.19f, -6.37f, 0f), 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        enemySaw.GetComponent<Transform>().DORotate(new Vector3(0f, 0f, 180f), 1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

   
}
