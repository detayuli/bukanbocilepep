using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Player1Score, Player2Score;
    [SerializeField] float timer;
    public void AddScore(int playerNumber, int score)
    {
        if (playerNumber == 1)
        {
            Player1Score += score;
        }
        else if (playerNumber == 2)
        {
            Player2Score += score;
        }
    }
    public int GetScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            Debug.Log("Player 1 Score: " + Player1Score);
            return Player1Score;
        }
        else if (playerNumber == 2)
        {
            Debug.Log("Player 2 Score: " + Player2Score);
            return Player2Score;
        }
        else
        {
            return 0;
        }
    }
    public void SetTimer(float time)
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }
    //instance of the GameManager
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
