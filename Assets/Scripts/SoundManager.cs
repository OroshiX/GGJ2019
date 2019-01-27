using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSource;

    void Start() {

    }
    public void playMusic() {
        //int index, bool loop) {
        audioSource.Play();
        //        audioSource.loop = loop;
    }
    //
    //    void OnEnable() {
    //        SceneManager.sceneLoaded += onLoadCallback;
    //    }
    //
    //    private void onLoadCallback(Scene scene, LoadSceneMode loadSceneMode) {
    //        playMusic();
    //    }
    //
    //    void OnDisable() {
    //        SceneManager.sceneLoaded -= onLoadCallback;
    //    }
}
