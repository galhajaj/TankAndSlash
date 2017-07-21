using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePermanent : MonoBehaviour
{
    private static MakePermanent _instance;

    void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
