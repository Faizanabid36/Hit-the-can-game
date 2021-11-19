using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float shootingForce = 300f;

    public GameObject ball;

    public static Ball currentBall { get; set; }

    public static Ball lastBall { get; set; }

    Plane plane = new Plane(Vector3.up, 0);

    public Transform target;

    void OnEnable()
    {
        currentBall = this;
    }
    private void Start()
    {
        ball.GetComponent<Rigidbody>().AddForce(new Vector3(200f, 0, 0), ForceMode.Acceleration);
    }

    private void Update()
    {
        if (currentBall.transform.position.y <= -2)
            currentBall.GameOver();
    }

    public void GameOver() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ShootBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var rb = ball.GetComponent<Rigidbody>();
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            target.localPosition = new Vector3(point.x, point.y, 0)+ rb.transform.forward;
        }

        Vector3 shootInDirection =  target.position - ball.transform.position;
        

        rb.AddForce(shootInDirection * shootingForce, ForceMode.Impulse);
        //lastBall = this;
    }

    private void OnTriggerEnter(Collider other) => Debug.Log(other.tag);
}
