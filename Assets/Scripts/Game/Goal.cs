using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string next;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("goal enter");
        SceneManager.LoadScene(next);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("goal");

            SceneManager.LoadScene(next);
        }
    }
}
