using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentService : ServiceBase<EquipmentService>
{
    private void Start()
    {
        HideEquipmentUI();
    }

    private void HideEquipmentUI()
    {
        this.gameObject.SetActive(false);
    }
    public void MakeEquipmentUIVisible()
    {
        this.gameObject.SetActive(true);
    }
}
