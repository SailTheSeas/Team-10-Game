using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private string CombatScene;
    void Start()
    {
        
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(CombatScene);
    }
}
