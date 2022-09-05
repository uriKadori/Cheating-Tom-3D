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
        public int Col  { get; private set; }
        public int Row  { get; private set; }

        public virtual void Render(Vector3 pos, float Scale)
        {
            Col = (int) pos.x;
            Row = (int) pos.z;
            transform.position = pos * Scale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCellClick?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnCellReleased?.Invoke();
        }

        internal void Render(Vector3Int vector3Int, object scale)
        {
            throw new NotImplementedException();
        }
    }
}
