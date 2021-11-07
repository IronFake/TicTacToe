using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    //[ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour
    {
        
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Linear Progress Bar")]
        public static void AddLinearProgressBar()
        {
           Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"), Selection.activeGameObject.transform);
        }
#endif
        
        [SerializeField] private float minimum;
        [SerializeField] private float maximum;
        [SerializeField] private float current;
        [SerializeField] private Image mask;
        [SerializeField] private Image fill;
        [SerializeField] private Color color;
        
        // private void Update()
        // {
        //     GetCurrentFill();
        // }

        private void GetCurrentFill()
        {
            float currentOffset = current - minimum;
            float maximumOffset = maximum - minimum;
            float fillAmount = currentOffset / maximumOffset;
            mask.fillAmount = fillAmount;
            fill.color = color;
        }

        public void ResetFill()
        {
            current = 0;
            GetCurrentFill();
        }
        
        public void UpdateProgress(float progress)
        {
            current = progress;
            GetCurrentFill();
        }
    }
}