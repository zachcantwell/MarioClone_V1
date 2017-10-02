using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private Transform m_playerTransform;
    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private Text m_livesText; 

    public static LevelManager Instance
    {
        get; private set;
    } 

    private int m_hitPoint = 3;
    private int m_score = 0;
    private GameObject m_spawnLocation;

	void Awake () {

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_spawnLocation = GameObject.FindGameObjectWithTag("SpawnLocation");
        SetScoreText();
        SetLivesText();
    }
	
	void Update () {
        if(m_playerTransform.position.y < -100)
        {
            m_playerTransform.position = m_spawnLocation.transform.position;
            m_hitPoint--;
            SetLivesText();
            if(m_hitPoint <= 0)
            {
                Debug.Log("GameOver");
                
            }
        }	
	}

    public void Win()
    {
        Debug.Log("Winner");
    }

    public void SetScore(int value)
    {
        m_score += value;
        SetScoreText();
    }

    public void SetScoreText()
    {
        m_scoreText.text = "Score: " + m_score.ToString();
    }

    public void SetLivesText()
    {
        m_livesText.text = "Lives: " + m_hitPoint.ToString();
    }
}
