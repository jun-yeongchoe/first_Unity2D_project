using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar : MonoBehaviour
{
    private RectTransform hpFront;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 1.5f, 0);

    private void Awake()
    {
        hpFront = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (!target) return;

        Vector3 screen = Camera.main.WorldToScreenPoint(target.position+worldOffset);
        hpFront.position = screen;
    }
}
