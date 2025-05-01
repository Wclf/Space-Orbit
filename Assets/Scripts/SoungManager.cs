using UnityEngine;

public class SoungManager : MonoBehaviour
{
    public static SoungManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioSource _effectSource;

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
    
}
