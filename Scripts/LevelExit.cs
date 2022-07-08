using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float waitBeforeLoad = 0.5f;
    [SerializeField] float LevelExitSlowmoFactor = 0.2f;
    [SerializeField] GameObject winLabel;
    Animator myAnimator;

    void Start() 
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());

    }
    
    IEnumerator LoadNextLevel()
    {
        myAnimator.SetTrigger("PlayerArrives");
        yield return new WaitForSecondsRealtime(waitBeforeLoad);
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(waitBeforeLoad);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    
    

}

