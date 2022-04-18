using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIScript : MonoBehaviour
{
    public TextMeshProUGUI currentEnemyText;
    public TextMeshProUGUI maxEnemyText;

    public ScoreManager _scoreManager;
    
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemyText.text = _scoreManager.currentZombieCount.ToString();
    }
}
