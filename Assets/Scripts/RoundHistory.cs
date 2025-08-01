using UnityEngine;

public class RoundHistory
{
    private Vector2 position;
    private Team team;
    public RoundHistory(Vector2 position, Team team)
    {
        this.position = position;
        this.team = team;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public Team GetTeam()
    {
        return team;
    }
}
