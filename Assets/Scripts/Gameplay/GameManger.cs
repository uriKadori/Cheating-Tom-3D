using GameData;
using System.Linq;
using UnityEngine;
namespace GamePlay
{
    public class GameManger : MonoBehaviour
    {
        //SO data level
        private GridData gridData;
        //gameplay grid
        [SerializeField] private Grid grid;
        [SerializeField] private Player player;
        [SerializeField] private Camera cam;
        [SerializeField] private int clickAbleLayerMask;
        private void Awake()
        {
            //TODO change to be nice from editor
            gridData = new GridData();
            //
            grid.Render(gridData);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, 1 << clickAbleLayerMask))
                {
                    if (hitInfo.collider.TryGetComponent<Cell>(out Cell target))
                    {
                        var path = gridData.GetPath(player.data, target.data);
                        var pathWithScale = path.Select(r => r * grid.SpaceSize).ToList();
                        player.MoveCharacter(path);
                    }
                }
            }
        }
    }
}