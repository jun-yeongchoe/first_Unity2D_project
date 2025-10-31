using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Love : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disable", 2);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
