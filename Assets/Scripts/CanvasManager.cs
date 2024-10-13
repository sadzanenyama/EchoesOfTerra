using UnityEngine;

[System.Serializable]
public class CanvasManager
{
    public GameObject[] canvas;
    public void SetCanvas(int id)
    {
        if(id == -1)
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].SetActive(false);
            }
        }

        for (int i = 0; i < canvas.Length; i++)
        {
            if (i != id)
            {
                if (canvas[i])
                    canvas[i].SetActive(false);
            }
            else
            {
                canvas[i].SetActive(true);
            }
        }
    }

    public void BackToBaseCanvas()
    {
        SetCanvas(0);
    }
}
