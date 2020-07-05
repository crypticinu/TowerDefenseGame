using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private TextMesh _textMesh;
    private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    void Start()
    {
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = _waypoint.GetGridSize();
        var waypointPos = _waypoint.GetGridPos();

        transform.position = new Vector3(
            waypointPos.x * gridSize,
            0f,
            waypointPos.y * gridSize);
    }

    private void UpdateLabel()
    {
        var labelText = $"{_waypoint.GetGridPos().x },{_waypoint.GetGridPos().y}";
        _textMesh = GetComponentInChildren<TextMesh>();

        if(_textMesh != null)
        {
            _textMesh.text = labelText;
        }

        gameObject.name = labelText;
    }
}
