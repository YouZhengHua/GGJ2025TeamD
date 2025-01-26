using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    private BGMManager manager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>();
        manager.PlaySound(BGMType.START);
    }

    public void PlayCriditBGM()
    {
        manager.PlaySound(BGMType.CRIDIT);
    }

    public void PlayStartBGM()
    {
        manager.PlaySound(BGMType.START);
    }

    public void PlayPlayBGM()
    {
        manager.PlaySound(BGMType.GAMEON);
    }
}
