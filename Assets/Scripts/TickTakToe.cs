using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;



[System.Serializable]
public class ButtonRow
{
    [SerializeField] public Button[] buttons; // Array of buttons for a single row
}


public class TickTakToe : MonoBehaviour
{


    [SerializeField] public ButtonRow[] buttonRows; // Array of ButtonRow 
    private bool isPlayerOne = true;
    private bool isPlayerTwo;

    [SerializeField] TMP_Text turnText;
    private bool isWinning = false;
    private bool computerMode = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            computerMode = true;
        }
    }

    public void OnButtonClick()
    {
        GameObject clickedButtonGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (clickedButtonGameObject != null && !isWinning)
        {
            Button clickedButton = clickedButtonGameObject.GetComponent<Button>();

            for (int i = 0; i < buttonRows.Length; i++)
            {
                for (int j = 0; j < buttonRows[i].buttons.Length; j++)
                {
                    if (buttonRows[i].buttons[j] == clickedButton)
                    {
                        TMP_Text buttonText = buttonRows[i].buttons[j].GetComponentInChildren<TMP_Text>();

                        if (buttonText != null && buttonText.text == " ")
                        {
                            if (isPlayerOne == true)
                            {
                                buttonText.text = "X";
                                WinCheck();

                                if (!isWinning)
                                {
                                    if (computerMode == true)
                                    {
                                        isPlayerTwo = false;
                                        ComputerMoveEasy();
                                    }
                                    else
                                    {
                                        PlayerTwo();
                                    }
                                }

                            }
                            else if (isPlayerTwo == true) // player 2 turn
                            {
                                buttonText.text = "O";
                                WinCheck();

                                if (!isWinning)
                                {
                                    PlayerOne();
                                }
                            }
                            return;
                        }
                    }
                }
            }
        }
    }



    public void PlayerOne()
    {
        isPlayerTwo = false;
        isPlayerOne = true;
        Debug.Log("player one turn");
        turnText.text = "player one turn";
        WinCheck();
    }

    public void PlayerTwo()
    {
        isPlayerOne = false;
        isPlayerTwo = true;
        Debug.Log("player two turn");
        turnText.text = "player two turn";
        WinCheck();
    }

    public void ComputerMoveEasy()
    {
        bool isValidMove = false;

        while (!isValidMove)
        {
            int row = Random.Range(0, buttonRows.Length);
            int col = Random.Range(0, buttonRows[row].buttons.Length);

            TMP_Text botText = buttonRows[row].buttons[col].GetComponentInChildren<TMP_Text>();

            if (botText.text == " ")
            {
                botText.text = "O";
                isValidMove = true;
            }
        }


        WinCheck();
        if (!isWinning)
        {
            PlayerOne();
        }


    }

    public void GameOver(int whoWon)
    {
        if (whoWon == 1)
        {
            turnText.text = "Player X won";
        }
        if (whoWon == 2)
        {
            turnText.text = "Player O won";
        }
        if (whoWon == 3)
        {
            turnText.text = "It's a tie";
        }
        //turnText.text = "game Over";

    }

    public void Restart(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void WinCheck()
    {
        WinChecker winChecker = new WinChecker(this, buttonRows);
        isWinning = winChecker.WinCheck();
        Debug.Log("checked for win");
    }
}
