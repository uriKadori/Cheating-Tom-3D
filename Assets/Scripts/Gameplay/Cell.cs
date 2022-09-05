using GameData;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay
{
    public abstract class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Cell> OnCellClick;
        public event Action OnCellReleased;
        public abstract CellType CellType { get; }
        public virtual void Render(CellData cellData, Vector3 pos)
        {
            transform.position = pos;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCellClick?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnCellReleased?.Invoke();
        }
    }
}
