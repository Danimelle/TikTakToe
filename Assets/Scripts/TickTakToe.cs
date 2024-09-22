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


    [SerializeField] public ButtonRow[] buttonRows; // Array of ButtonRows 
    private bool isPlayerOne = true; //player 1 starts
    private bool isPlayerTwo;

    [SerializeField] TMP_Text turnText;
    private bool isWinning = false;
    private bool computerMode = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2) //if the scene index is 2 then it changes to computer vs player
        {
            computerMode = true;
        }
    }

    public void OnButtonClick()
    {
        GameObject clickedButtonGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (clickedButtonGameObject != null && !isWinning) //if button exists and no one won yet do this
        {
            Button clickedButton = clickedButtonGameObject.GetComponent<Button>();

            for (int i = 0; i < buttonRows.Length; i++)
            {
                for (int j = 0; j < buttonRows[i].buttons.Length; j++)
                {
                    if (buttonRows[i].buttons[j] == clickedButton)
                    {
                        TMP_Text buttonText = buttonRows[i].buttons[j].GetComponentInChildren<TMP_Text>(); //gets the text componant of selected button

                        if (buttonText != null && buttonText.text == " ") //if text exists and there's a space in the text component then  - 
                        {
                            if (isPlayerOne == true) //player 1 starts
                            {
                                buttonText.text = "X";
                                WinCheck();

                                if (!isWinning) // if player one didn't win then 
                                {
                                    if (computerMode == true) //check if comuter mode is on (if it's on the right scene)
                                    {
                                        isPlayerTwo = false;
                                        ComputerMoveEasy();
                                    }
                                    else //if not then start player 2 turn
                                    {
                                        PlayerTwo();
                                    }
                                }

                            }
                            else if (isPlayerTwo == true) // player 2 turn
                            {
                                buttonText.text = "O";
                                WinCheck();

                                if (!isWinning) // if player 2 didn't win then move on to player 1
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
        bool isValidMove = false; //valid move is false in the beginning and changes if the random button has a space in it
        int numberOfTries = 0;

        while (!isValidMove && numberOfTries < 5) //as long as valid move is false AND number of tries is less than 5, while will keep looping 
        {
            int row = Random.Range(0, buttonRows.Length); // picks a random number between 0 and the length of buttonrow
            int col = Random.Range(0, buttonRows[row].buttons.Length); //picks a random num between 0 and button lenght

            TMP_Text botText = buttonRows[row].buttons[col].GetComponentInChildren<TMP_Text>(); //uses random numbers to get the specific button text

            numberOfTries++;
            Debug.Log("number of tries: " + numberOfTries);
            if (botText.text == " ")
            {
                botText.text = "O";
                isValidMove = true;
            }
        }


        WinCheck();
        if (!isWinning) //if computer didn't win, move to player 1
        {
            PlayerOne();
        }


    }

    public void GameOver(int whoWon) //takes an int calue which tells it which text to switch to
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
