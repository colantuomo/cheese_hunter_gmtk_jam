using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private Tile _editModeTile;
    [SerializeField]
    private Tilemap _editModeTilemap, _walkableTilemap;


    private void Start()
    {
        ClearWalkablePath();
    }

    void Update()
    {
        if (ClickedWithLeftSide())
        {
            RaycastHit2D hit = GetClickHit();
            if (hit.collider == null) return;
            if (hit.transform.TryGetComponent(out CanAddBlock _))
            {
                var tilemap = GetClickedTilemap(hit.transform);
                if (tilemap)
                {
                    var tilePosition = GetTilePosition(tilemap);
                    var tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);
                    EventsManager.Singleton.TryAddSelectedBlock(tileWorldPosition);
                }
            }
        }

        if (ClickedWithRightSide())
        {
            RaycastHit2D hit = GetClickHit();
            if (hit.collider != null)
            {
                if (!hit.transform.TryGetComponent(out IsEditableItem _))
                {
                    return;
                }
                if (hit.transform.TryGetComponent(out IsBlock block))
                {
                    EventsManager.Singleton.DeleteAItem(block.GetBlockType());
                    Destroy(block.gameObject);
                    return;
                }
                var tilemap = GetClickedTilemap(hit.transform);
                if (tilemap)
                {
                    var tilePosition = GetTilePosition(tilemap);
                    tilemap.SetTile(tilePosition, null);
                    _editModeTilemap.SetTile(tilePosition, _editModeTile);
                    EventsManager.Singleton.DeleteAItem(BlockTypes.Ground);
                }
            }
        }
    }

    private Tilemap GetClickedTilemap(Transform itemClicked)
    {
        if (itemClicked.TryGetComponent(out Tilemap tileMap))
        {
            return tileMap;
        }
        return null;
    }

    private TileBase GetClickedTileBase(Tilemap tilemap)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tpos = tilemap.WorldToCell(worldPoint);
        var tile = tilemap.GetTile(tpos);
        if (tile)
        {
            return tile;
        }
        return null;
    }

    private bool IsClickingThroughUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private Vector3Int GetTilePosition(Tilemap tilemap)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return tilemap.WorldToCell(worldPoint);
    }

    private RaycastHit2D GetClickHit()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(clickPosition, Vector2.zero);
    }

    private void ClearWalkablePath()
    {
        foreach (var pos in _walkableTilemap.cellBounds.allPositionsWithin)
        {
            if (_walkableTilemap.HasTile(pos))
            {
                _editModeTilemap.SetTile(pos, null);
            }
        }
    }

    private bool ClickedWithLeftSide()
    {
        if (IsClickingThroughUI()) return false;
        return Input.GetMouseButtonDown(0);
    }

    private bool ClickedWithRightSide()
    {
        if (IsClickingThroughUI()) return false;
        return Input.GetMouseButtonDown(1);
    }
}
