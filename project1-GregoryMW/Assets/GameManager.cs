using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] VRPlayer player;
    [SerializeField] StartButton startButton;
    [SerializeField] FinishedButton finishedButton;
    [SerializeField] GameObject door;
    [SerializeField] List<Button> buttons;
    [SerializeField] AudioClip victorySound;
    public enum GAME_STATE { IDLE, STARTED, FINISHED }
    public GAME_STATE gameState = GAME_STATE.IDLE;
    // Start is called before the first frame update
    void Start()
    {
        startButton.buttonPressed += startPressed;
        finishedButton.buttonPressed += finishPressed;
    }

    void startPressed(VRHand hand)
    {
        if (gameState == GAME_STATE.IDLE)
        {
            startGame();
        }
    }

    void finishPressed(VRHand hand)
    {
        if (gameState == GAME_STATE.STARTED)
        {
            bool gameFinished = true;
            foreach (Button b in buttons)
            {
                if (!b.isActive)
                {
                    gameFinished = false;
                    break;
                }
            }
            if (gameFinished)
            {
                endGame();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        gameState = GAME_STATE.STARTED;
        door.GetComponent<OpenDoor>().OperateDoor();
    }

    public void endGame()
    {
        SceneManager.LoadScene(0);
    }
}
