using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Ball.currentBall != null)
                Ball.currentBall.ShootBall();
        }
    }
}
