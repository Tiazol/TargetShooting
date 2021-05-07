using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int points = 10;

    public int Points => points;
}
