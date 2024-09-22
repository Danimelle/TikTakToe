using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Collections;

public class WinChecker
{
    private TickTakToe tickTakToe;
    private ButtonRow[] buttonRows;

    private bool isWinning;


    public WinChecker(TickTakToe tickTakToe, ButtonRow[] buttonRows)
    {
        this.tickTakToe = tickTakToe;
        this.buttonRows = buttonRows;
    }

    public bool WinCheck()
    {
        isWinning = false;

        // check row matches
        for (int i = 0; i < buttonRows.Length; i++) // passes through all items in the first row (0-2)
        {
            TMP_Text firstButtonInRow = buttonRows[i].buttons[0].GetComponentInChildren<TMP_Text>(); //first buttons in row = the text componant of the first row items (0-2) i changes every time and 0 keeps it on the first row

            if (firstButtonInRow.text == "X" || firstButtonInRow.text == "O") // first check the first button in the row for an x or a o
            {
                for (int j = 1; j < buttonRows[i].buttons.Length; j++) // passes through the rest of the buttons, starting from 1 cause we checked 0
                {
                    TMP_Text restOfButtonsInRow = buttonRows[i].buttons[j].GetComponentInChildren<TMP_Text>(); // rest of buttons = text of rest of buttons

                    if (restOfButtonsInRow.text != firstButtonInRow.text) // if the text isn't x or o in the rest of the buttons, break
                    {
                        isWinning = false;
                        Debug.Log("no");
                        break;
                    }
                    else
                    {
                        isWinning = true;
                    }
                }

                if (isWinning)
                {
                    if (firstButtonInRow.text == "X") { tickTakToe.GameOver(1); }
                    if (firstButtonInRow.text == "O") { tickTakToe.GameOver(2); }
                    Debug.Log("win");
                    //tickTakToe.GameOver();
                    return true;
                }

            }

        }

        //check colum matches

        for (int i = 0; i < 3; i++) //runs 3 times for 3 colums
        {
            TMP_Text firstButtonInCol = buttonRows[0].buttons[i].GetComponentInChildren<TMP_Text>(); //stores the text of first buttons in the collun

            if (firstButtonInCol.text == "X" || firstButtonInCol.text == "O")
            {

                for (int j = 1; j < buttonRows.Length; j++)
                {
                    TMP_Text restOfButtonsInCol = buttonRows[j].buttons[i].GetComponentInChildren<TMP_Text>();

                    if (restOfButtonsInCol.text != firstButtonInCol.text)
                    {
                        isWinning = false;
                        break;
                    }
                    else
                    {
                        isWinning = true;
                    }
                }

                if (isWinning)
                {
                    if (firstButtonInCol.text == "X") { tickTakToe.GameOver(1); } //if the text is x then it sends the game over method a 1 that tells it what to print
                    if (firstButtonInCol.text == "O") { tickTakToe.GameOver(2); } //if the text is o then it sends the game over method a 2 that tells it what to print
                    Debug.Log("win");
                    return true;
                }

            }
        }


        // diagonal matches
        TMP_Text firstButtonLeft = buttonRows[0].buttons[0].GetComponentInChildren<TMP_Text>();
        if (firstButtonLeft.text == "X" || firstButtonLeft.text == "O")
        {
            isWinning = true;

            for (int i = 1; i < buttonRows.Length; i++)
            {
                TMP_Text restOfButtonDiagonal = buttonRows[i].buttons[i].GetComponentInChildren<TMP_Text>();

                if (restOfButtonDiagonal.text != firstButtonLeft.text)
                {
                    isWinning = false;
                    break;
                }
            }

            if (isWinning)
            {
                if (firstButtonLeft.text == "X") { tickTakToe.GameOver(1); }
                if (firstButtonLeft.text == "O") { tickTakToe.GameOver(2); }

                Debug.Log("win");
                //tickTakToe.GameOver();
                return true;
            }
        }

        TMP_Text firstButtonRight = buttonRows[0].buttons[2].GetComponentInChildren<TMP_Text>();
        if (firstButtonRight.text == "X" || firstButtonRight.text == "O")
        {
            isWinning = true;

            for (int i = 0; i < buttonRows.Length; i++)
            {
                TMP_Text restOfButtonDiagonal = buttonRows[i].buttons[2 - i].GetComponentInChildren<TMP_Text>();

                if (restOfButtonDiagonal.text != firstButtonRight.text)
                {
                    isWinning = false;
                    break;
                }
            }

            if (isWinning)
            {
                if (firstButtonRight.text == "X") { tickTakToe.GameOver(1); }
                if (firstButtonRight.text == "O") { tickTakToe.GameOver(2); }
                Debug.Log("win");
                //tickTakToe.GameOver();
                return true;
            }
        }

        //check for tie
        bool allFilled = true;

        for (int i = 0; i < buttonRows.Length; i++)
        {
            for (int j = 0; j < buttonRows[i].buttons.Length; j++)
            {
                TMP_Text buttonText = buttonRows[i].buttons[j].GetComponentInChildren<TMP_Text>();

                if (buttonText.text == " ")
                {
                    allFilled = false;
                    break;
                }
            }
            if (!allFilled) break; //if not all buttons are filled than break the loop
        }

        if (allFilled)
        {
            Debug.Log("It's a tie!");
            tickTakToe.GameOver(3);
        }


        return isWinning;
    }
}