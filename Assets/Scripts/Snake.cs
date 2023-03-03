using TMPro;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 50;
    public Cube cube;
    public Food food;
    public Game game;

    public int Length = 5;

    public GameObject PointsText;

    public GameObject CubeDestoyed;
    public GameObject SnakeDestoyed;

    public AudioClip SnakeDeath;
    public AudioClip CubeDestory;

    private Camera mainCamera;
    private Rigidbody componentRigidbody;
    private SnakeTail componentSnakeTail;
    private Vector3 touchLastPos;
    private float sidewaysSpeed;
    private Cube _currentCube;
    private Cube _currentFood;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody>();
        componentSnakeTail = GetComponent<SnakeTail>();
        Length = int.Parse(PointsText.GetComponent<TextMesh>().text);

        for (int i = 0; i < Length; i++) componentSnakeTail.AddBodyElement();

        PointsText.GetComponent<TextMesh>().text = Length.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = (Vector3) mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

        PointsText.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);

       
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);

        sidewaysSpeed = 0;

        if (!(cube == null))
        {
            if (_currentCube != cube)
            {
                Transform transformText = cube.transform.GetChild(0);
                int countRemoveBodyElement = int.Parse(transformText.gameObject.GetComponent<TextMesh>().text);
                int bodyElementsAvailable = int.Parse(PointsText.GetComponent<TextMesh>().text);
                if (countRemoveBodyElement > bodyElementsAvailable)
                {
                    Die();
                    return;
                }
                for (int i = 0; i < countRemoveBodyElement; i++)
                {
                    Length--;
                    componentSnakeTail.RemoveElementBody();
                    PointsText.GetComponent<TextMesh>().text = Length.ToString();
                    transformText.gameObject.GetComponent<TextMesh>().text = (countRemoveBodyElement - 1).ToString();
                }
            }
            _audio.PlayOneShot(CubeDestory);
            CubeDestoyed.SetActive(true);
            CubeDestoyed.GetComponent<ParticleSystem>().Stop();
            GameObject gameObject = Instantiate(CubeDestoyed, new Vector3(cube.transform.position.x, cube.transform.position.y + 1, cube.transform.position.z), cube.transform.rotation);
            gameObject.transform.localScale = new Vector3(2.5f, 1, 2.5f);
            Destroy(cube.transform.parent.gameObject);
            _currentCube = cube;
        }
        if (food != null)
        {
            if(_currentFood != food)
            {
                Transform transformText = food.transform.GetChild(0);
                int countRemoveBodyElement = int.Parse(transformText.gameObject.GetComponent<TextMesh>().text);
                for (int i = 0; i < countRemoveBodyElement; i++)
                {
                    Length++;
                    componentSnakeTail.AddBodyElement();
                    PointsText.GetComponent<TextMesh>().text = Length.ToString();
                    transformText.gameObject.GetComponent<TextMesh>().text = (countRemoveBodyElement - 1).ToString();
                }
            }

            Destroy(food.transform.parent.gameObject);
            _currentFood = cube;
        }
    }
    private void Won()
    {
        game.OnPlayerReachedFinish();
    }
    public void ReachFinish()
    {
        Invoke("Won", 0f);
        componentRigidbody.velocity = Vector3.zero;
    }

    private void GameOver()
    {
        game.OnPlayerDied();
    }

    public void Die()
    {
        _audio.PlayOneShot(SnakeDeath);
        SnakeDestoyed.SetActive(true);
        SnakeDestoyed.GetComponent<ParticleSystem>().Stop();
        Instantiate(SnakeDestoyed, transform.position, transform.rotation);
        Invoke("GameOver", 0.05f);
        componentRigidbody.velocity = Vector3.zero;
    }
}
