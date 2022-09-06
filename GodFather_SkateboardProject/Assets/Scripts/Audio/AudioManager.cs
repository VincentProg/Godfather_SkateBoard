using UnityEngine;


[System.Serializable]
public class Sound
{
    //RDV video Brackeys at end to check how to play sounds 
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float Volume = 0.5f;
    [Range(0.5f, 1.5f)] public float pitch = 1f;

    [Range(0f, 0.5f)] public float randVol = 0.1f;
    [Range(0f, 0.5f)] public float randPitch = 0.1f;

    private AudioSource source;

    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }
    public void Play()
    {
        source.volume = Volume * (1 + Random.Range(-randVol / 2f, randVol / 2f));
        source.pitch = pitch * (1 + Random.Range(-randPitch / 2f, randPitch / 2f));
        source.Play();

    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("AudioManager : Other AudioManager detected");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
        }

    public void Playsound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        //no sounds
        Debug.Log("AudioManager : Sound Not found in List (" + _name +")");
    }
}
