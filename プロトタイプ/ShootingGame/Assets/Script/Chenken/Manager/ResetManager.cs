using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;

public class ResetManager : MonoBehaviour
{
    void Start()
    {
        P1_PowerManager.Instance.ResetAllPowerUpgradeCount();
        P2_PowerManager.Instance.ResetAllPowerUpgradeCount();

        P1_PowerManager.Instance.RemoveAllCheckCallBack();
        P2_PowerManager.Instance.RemoveAllCheckCallBack();

        P1_PowerManager.Instance.RemoveAllUpdateCallBack();
        P2_PowerManager.Instance.RemoveAllUpdateCallBack();
    }
}
