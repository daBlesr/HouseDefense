using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public Rigidbody2D goblin;
    private Wave currentWave;
    [SerializeField] private int currentWaveNumber = 1;
    private float previousTimeWaveEnded = 0f;
    private float interWaveTime = 10.0f;
    private float goblinMultiplier = 1.2f;
    private float waveDurationMultiplier = 1.8f;

    void Start()
    {
        currentWave = new Wave(this, 2, 3);
    }

    void Update()
    {
        if (currentWave.shouldEnd())
        {
            Debug.Log("Wave " + currentWaveNumber + " ended");
            currentWave.end();
            previousTimeWaveEnded = Time.time;
        }
        else if (currentWave.hasEnded() && (Time.time - previousTimeWaveEnded > interWaveTime))
        {
            Debug.Log("Wave started " + currentWaveNumber);
            startNewWave();
        }
        else if (currentWave.shouldSpawnGoblin())
        {
            currentWave.spawnGoblin();
        }
    }

    private void startNewWave()
    {
        currentWave = new Wave(
            this,
            (int)Mathf.Ceil(currentWave.getNumberOfGoblins() * goblinMultiplier),
            (int)Mathf.Ceil(((float)currentWave.getSeconds()) * waveDurationMultiplier)
        );

        currentWaveNumber += 1;
    }

    public class Wave
    {
        private readonly WaveSystem ws;
        private readonly int goblins;
        private readonly int seconds;
        private Quaternion fakeRotation = new Quaternion();
        private float initializationTime;
        private float goblinSpawnTime;
        private int goblinsSpawned = 0;
        private float previousTimeGoblinSpawned = 0f;
        private bool ended = false;

        public Wave(WaveSystem ws, int goblins, int seconds)
        {
            this.ws = ws;
            this.goblins = goblins;
            this.seconds = seconds;

            initializationTime = Time.time;
            goblinSpawnTime = ((float)seconds) / goblins;
        }

        public void spawnGoblin()
        {
            goblinsSpawned += 1;
			Rigidbody2D newGoblin = Instantiate(this.ws.goblin, new Vector3(50, 2, 0), fakeRotation);
            previousTimeGoblinSpawned = Time.time;
        }

        public bool shouldEnd()
        {
            return !ended && (Time.time - initializationTime >= seconds);
        }

        public bool shouldSpawnGoblin()
        {
            return !ended && Time.time - previousTimeGoblinSpawned >= goblinSpawnTime;
        }

        public int getNumberOfGoblins()
        {
            return goblins;
        }

        public int getSeconds()
        {
            return seconds;
        }

        public void end()
        {
            ended = true;
        }

        public bool hasEnded()
        {
            return ended;
        }
    }
}
