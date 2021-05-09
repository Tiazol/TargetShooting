using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
