using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillService : ServiceBase<SkillService>
{
    // Start is called before the first frame update
    void Start()
    {
        HideSkillUI();
    }

    void HideSkillUI()
    {
        this.gameObject.SetActive(false);
    }

    public void MakeSkillUIVisible()
    {
        this.gameObject.SetActive(true);
    }
}
