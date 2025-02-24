using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int openTreasure = Animator.StringToHash("OpenTreasure");

    protected Animator animator;
    public float interactionDistance = 0f; //상자와 상호작용 거리
    private bool isOpen = false; //처음 상자 열렸는지 유무

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //플레이어의 위치를 가져오고, 플레이어가 상자에 다가가면 OpenTreasure메서드가 실행되도록 함
        //GameObject player = GameObject.FindWithTag("player");

        //if (player != null)
        //{
        //    float distance = Vector3.Distance(player.transform.position, transform.position);

        //    if (distance < interactionDistance && !isOpen)
        //        OpenTreasure();
           
        //}
    }

    public void OpenTreasure()
    {
        animator.SetBool(openTreasure, true);
        isOpen = true;
    }
}
