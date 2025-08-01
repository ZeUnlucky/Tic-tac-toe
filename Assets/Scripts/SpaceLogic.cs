using UnityEngine;

public class SpaceLogic : MonoBehaviour
{
    [HideInInspector] public Team TeamOccupying;
    [SerializeField] GamePrefabStore prefabs;
    [SerializeField] MVCController controller;
    private GameObject occupyingGO;
    public void Occupy(Team team, bool isRedo = false)
    {
        if (TeamOccupying == Team.None)
        {
            TeamOccupying = team;
            occupyingGO = Instantiate(TeamOccupying == Team.X ? prefabs.xObject : prefabs.oObject, transform.position, Quaternion.identity);  
            controller.OnLogicOccupy(this, !isRedo);
        }
        else if (team == Team.None)
            Undo();
    }

    private void Undo()
    {
        controller.Undo(this);
        if (occupyingGO != null)
            Destroy(occupyingGO);
    }

    private void OnMouseDown()
    {
        controller.OnInput(this);
    }
}
