using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    None,
    X,
    O
}

[Serializable]
public class Column
{
    public SpaceLogic[] rows;
}

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Team CurrentTeam;
    [HideInInspector] public bool IsPlaying;
    [SerializeField] private TextMeshProUGUI teamText;
    [SerializeField] private Column[] columns;
    [SerializeField] Button[] buttonsToDisable;
    public HistoryManager History;

    public static GameManager Instance;

    // singleton implementation
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // team X starts defaultly
    private void Start()
    {
        Instance.CurrentTeam = Team.X;
        Instance.IsPlaying = true;
        History = new HistoryManager();
    }

    // gets the space logic from a position
    public SpaceLogic GetLogicFromPosition(Vector2 position)
    {
        return columns[(int)position.x].rows[(int)position.y];
    }

    // executes team turn, adds to history, changes current team, checks for win
    public void DoTeamTurn(SpaceLogic logic, bool addToHistory = true)
    {
        if (IsPlaying)
        {
            Vector2 pos = GetPositionFromLogic(logic);
            if (addToHistory)
                History.AddHistory(new RoundHistory(pos, CurrentTeam));
            if (DidWin(pos))
            {
                IsPlaying = false;
                teamText.text = CurrentTeam == Team.X ? "X WON!" : "O WON!";
                foreach (var button in buttonsToDisable)
                {
                    button.interactable = false;
                }
            }
            else
            {
                if (CurrentTeam == Team.X)
                    CurrentTeam = Team.O;
                else
                    CurrentTeam = Team.X;
                teamText.text = CurrentTeam == Team.X ? "X" : "O";
            }
            
        }
    }

    // gets logic's position
    private Vector2 GetPositionFromLogic(SpaceLogic logic)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (columns[i].rows[j] == logic)
                    return new Vector2(i, j);
            }
        }
        return Vector2.zero;
    }

    //checks for win by checking row, then column, then diagonals
    private bool DidWin(Vector2 pos)
    {

        if (columns[2].rows[(int)pos.y].TeamOccupying == columns[0].rows[(int)pos.y].TeamOccupying && columns[1].rows[(int)pos.y].TeamOccupying == columns[0].rows[(int)pos.y].TeamOccupying)
            return true;
      
        if (columns[(int)pos.x].rows[2].TeamOccupying == columns[(int)pos.x].rows[0].TeamOccupying && columns[(int)pos.x].rows[1].TeamOccupying == columns[(int)pos.x].rows[0].TeamOccupying)
            return true;

        if (IsCornerOrCenter(pos))
            return AreDiagonalsEqual();

        return false;

    }

    // checks if the position is a corner or center with math
    private bool IsCornerOrCenter(Vector2 pos)
    {
        return pos.x + pos.y == 2 || pos.x == pos.y;
    }

    // checks  for diagonals
    private bool AreDiagonalsEqual()
    {
        if (columns[1].rows[1].TeamOccupying != Team.None)
            return (columns[0].rows[0].TeamOccupying == columns[1].rows[1].TeamOccupying && columns[1].rows[1].TeamOccupying == columns[2].rows[2].TeamOccupying) ||
                (columns[0].rows[2].TeamOccupying == columns[1].rows[1].TeamOccupying && columns[1].rows[1].TeamOccupying == columns[2].rows[0].TeamOccupying);
        else
            return false;
    }
}
