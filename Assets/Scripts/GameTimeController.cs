using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    private float _currentTimeScale = 1;
    
    public void ChangeGamePauseState()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = _currentTimeScale;
    }

    public void ChangeTimeFastState()
    {
        if (Time.timeScale == 0) return;
        
        if (Time.timeScale == 1)
        {
            Time.timeScale = 1.5f;
        }
        else if (Time.timeScale == 1.5f)
        {
            Time.timeScale = 2f;
        }
        else Time.timeScale = 1;

        _currentTimeScale = Time.timeScale;
    }
} 