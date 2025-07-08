using UnityEngine;

public class TapCollectble : MonoBehaviour
{
    

    public void TapToCollect()
    {
        PlayerManager.Instance.current_Currency += GameManager.Instance.TapMultiplier;
    }
}
