using TMPro;
using UnityEngine;

public class DeathMeter : MonoBehaviour
{
    TextMeshPro TextMesh;

    void Start()
    {
        TextMesh = gameObject.GetComponent<TextMeshPro>();
        TextMesh.text = "1週目";
    }

    public void Dead(int Death)
    {
        TextMesh.text = Death.ToString() + "週目";
    }
}
