using UnityEngine;

namespace TicTacToe.UI
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] private ViewType type;

        public ViewType ViewType => type;
        
        public virtual void Hide() => gameObject.SetActive(false);
        
        public virtual void Show() => gameObject.SetActive(true);
    }
}