using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject bot;

    public void Reboot()
    {
        bot.transform.position = Vector3.up* 1.5f;
    }
}
