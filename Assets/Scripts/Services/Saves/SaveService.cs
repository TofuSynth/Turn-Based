using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveService : ServiceBase<SaveService>
{
    void Start()
    {
        HideSaveUI();
    }

    void HideSaveUI()
    {
        this.gameObject.SetActive(false);
    }

    public void MakeSaveUIVisible()
    {
        this.gameObject.SetActive(true);
    }
}
