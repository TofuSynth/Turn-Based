using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadService : ServiceBase<LoadService>
{
    void Start()
    {
        HideLoadUI();
    }

    void HideLoadUI()
    {
        this.gameObject.SetActive(false);
    }

    public void MakeLoadUIVisible()
    {
        this.gameObject.SetActive(true);
    }
}
