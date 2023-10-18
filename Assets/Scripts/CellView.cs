using UnityEngine;

namespace SeaBattle
{
    public sealed class CellView : MonoBehaviour
    {
        public bool isPlaced;
        public bool isShip;
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color pressedColor;

        private void Start()
        {
            mesh.material.color = defaultColor;
        }

        private void OnMouseEnter()
        {
            if(mesh.material.color != pressedColor)
                mesh.material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            if(mesh.material.color != pressedColor)
                mesh.material.color = defaultColor;
        }

        private void OnMouseDown()
        {
            mesh.material.color = pressedColor;
        }

        public void Placed()
        {
            mesh.material.color = pressedColor;
        }

        public void Unplaced()
        {
            mesh.material.color = defaultColor;
        }
    }
}