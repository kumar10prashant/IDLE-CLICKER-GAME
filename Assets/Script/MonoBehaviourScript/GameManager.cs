using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int AutoMultiplier;
    public int TapMultiplier;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }
}
