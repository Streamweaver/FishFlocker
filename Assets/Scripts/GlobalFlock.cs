using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public Transform fishSchool;
    public GameObject goal;
    public int TankSize = 7;
    public int NumFish;
    public GameObject[] AllFish { get; set; }
    public Vector3 goalPositon { get; set; }

    public float FishNeighborDistance = 3.0f;
    public float FishAvoidDistance = 1.5f;
    public float FishTurnSpeed = 2f;
    public float FishMinSpeed = 0.5f;
    public float FishMaxSpeed = 1.0f;

    void Awake()
    {
        goalPositon = goal.transform.position;
        AllFish = new GameObject[NumFish];
        for (int i = 0; i < NumFish; i++)
        {
            Spawn(i);
        }
    }

    void Update()
    {
        goalPositon = goal.transform.position;
    }

    private void Spawn(int i)
    {
        var pos = GetRandomPos();
        var prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        var fish = Instantiate(prefab, pos, Quaternion.identity);
        fish.transform.SetParent(fishSchool.transform, false);
        fish.GetComponent<Fish>().GlobalFlock = this;
        AllFish[i] = fish;
    }

    private Vector3 GetRandomPos()
    {
        var x = Random.Range(-TankSize, TankSize);
        var y = Random.Range(-TankSize, TankSize);
        var z = Random.Range(-TankSize, TankSize);
        return new Vector3(x, y, z);
    }
}
