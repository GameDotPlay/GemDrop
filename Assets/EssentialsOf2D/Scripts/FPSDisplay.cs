namespace GemDrop
{
    using UnityEngine;
    using TMPro;

    public class FPSDisplay : MonoBehaviour
    {
        private TMP_Text fpsText;

        private void Awake()
        {
            this.fpsText = this.GetComponent<TMP_Text>();    
        }

        private void Update()
        {
            if (this.fpsText != null)
            {
                int fps;
                if (Time.deltaTime < Mathf.Epsilon)
                {
                    fps = 0;
                }
                else
                {
                    fps = (int)(1 / Time.deltaTime);
                }
                
                this.fpsText.text = $"FPS: {fps}";
            }
        }
    }
}