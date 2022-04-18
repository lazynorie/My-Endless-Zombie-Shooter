using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public int currentZombieCount = 30;
    private PlayerHealthComponent _playerHealthComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerHealthComponent = FindObjectOfType<PlayerHealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentZombieCount<=0)
        {
            SceneManager.LoadScene("WinScene");
        }

        if (_playerHealthComponent.CurrentHealth<=0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
