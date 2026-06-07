using UnityEngine;

public class UiCanva : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance.MusicSource.clip != AudioManager.Instance.GetMusic("GameplayMusic"))
        {
            AudioManager.Instance.PlayMusic("GameplayMusic");
        }
    }
}
