using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private Vector3? currentSpawnPoint = null; // Nullable Vector3 to represent the spawn point
    private string currentCheckpointID = null; // Unique ID of the current checkpoint
    private Checkpoint currentCheckpoint = null;
    private Teleport teleportScript;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            teleportScript = gameObject.GetComponent<Teleport>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method is called every time a new scene is loaded
        if (currentCheckpointID != null)
        {
            Checkpoint checkpoint = FindCheckpointByID(currentCheckpointID);
            if (checkpoint != null)
            {
                currentCheckpoint = checkpoint;
                currentCheckpoint.EnableCollider();
                Debug.Log("Checkpoint collider enabled for " + currentCheckpointID);
            }
            else
            {
                Debug.LogWarning("Checkpoint with ID " + currentCheckpointID + " not found in the scene.");
            }
        }
        else
        {
            Debug.Log("No checkpoint set. Collider was not activated.");
        }

        if(currentSpawnPoint.HasValue){
            // Spawn the player at the current spawn point
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                teleportScript.TeleportPlayer(player.transform,currentSpawnPoint.Value);
                Debug.Log("Player spawned at " + currentCheckpoint.checkpointTransform.position);
            }else{
                Debug.Log("No Spawnpoint set. Player will spawn at the default location.");
            }

        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
        currentSpawnPoint = checkpoint.checkpointTransform.position;
        currentCheckpointID = checkpoint.checkpointID; // Store the checkpoint ID
    }

    public void ClearCheckpoint()
    {
        currentCheckpoint = null;
        currentSpawnPoint = null;
        currentCheckpointID = null;
    }

    private Checkpoint FindCheckpointByID(string id)
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.checkpointID == id)
            {
                return checkpoint;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
