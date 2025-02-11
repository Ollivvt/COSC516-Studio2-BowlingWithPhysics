using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    [SerializeField] private BallController ball;
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;
    [SerializeField] private InputManager inputManager;

    private GameObject pinObjects; // Stores the current set of pins
    private FallTrigger[] fallTriggers; // Stores all pin fall triggers

    private void Start()
    {
        // Check if there are existing pins before spawning new ones
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsSortMode.None);
        
        if (fallTriggers.Length == 0) // If no pins exist, create new ones
        {
            Debug.LogWarning("No existing pins found. Spawning new pins.");
            SetPins();
        }
        else
        {
            Debug.Log("Existing pins found. Using them.");
        }

        // Subscribe to reset event
        inputManager.OnResetPressed.AddListener(HandleReset);
    }

    private void HandleReset(){
        Debug.Log("HandleReset() called in GameManager!");
        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
{
    Debug.Log("Resetting pins...");

    // **Stop all pin movement before deleting**
    FallTrigger[] existingPins = FindObjectsByType<FallTrigger>(FindObjectsSortMode.None);
    foreach (var pin in existingPins)
    {
        Rigidbody rb = pin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        Destroy(pin.gameObject); // Destroy every pin
    }

    if (pinObjects != null)
    {
        Destroy(pinObjects);
        pinObjects = null;
    }

    // **Create new pins at `pinAnchor`**
    pinObjects = Instantiate(pinCollection, pinAnchor.position, Quaternion.identity, transform);
    Debug.Log("New pins spawned at " + pinAnchor.position);

    // **Find all new pins and attach scoring event**
    fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    foreach (FallTrigger pin in fallTriggers)
    {
        pin.OnPinFall.AddListener(IncrementScore);
    }
}

    
    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
