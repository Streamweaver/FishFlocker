using UnityEngine;

public class Fish : MonoBehaviour
{
    public GlobalFlock GlobalFlock { get; set; }
    public float speed;
    Vector3 averageHeading;
    Vector3 averagePosition;
    private GameObject[] _allFish;
    private Vector3 myPosition;
    private int myInstanceID;

    void Start()
    {
        _allFish = GlobalFlock.AllFish;
        speed = GetRandomSpeed();
        myInstanceID = this.GetInstanceID();
    }

    void Update()
    {
        myPosition = this.transform.position;
        var doTurning = ApplyTankBoundary();

        if (doTurning)
        {
            DoTurning();
        }
        else if (Random.Range(0, 5) < 1)
        {
            ApplyRules();
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void DoTurning()
    {
        Vector3 direction = Vector3.zero - myPosition;
        var a = transform.rotation;
        var b = Quaternion.LookRotation(direction);
        var t = TurnSpeed() * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(a, b, t);
        speed = GetRandomSpeed();
    }

    private float GetRandomSpeed()
    {
        return Random.Range(GlobalFlock.FishMinSpeed, GlobalFlock.FishMaxSpeed);
    }

    private bool ApplyTankBoundary()
    {
        var dist = Vector3.Distance(myPosition, GetGoalPos());
        var tankSize = GlobalFlock.TankSize;
        return dist < tankSize;
    }

    private void ApplyRules()
    {

        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0;

        Vector3 goalPos = GetGoalPos();

        float dist;
        int groupSize = 0;

        for (var i = 0; i < _allFish.Length; i++)
        {
            GameObject fish = _allFish[i];

            if (fish.GetInstanceID() == myInstanceID)
                continue;

            var otherFishPos = fish.transform.position;
            dist = Vector3.Distance(otherFishPos, myPosition);
            if (dist <= GlobalFlock.FishNeighborDistance)
            {
                vCenter += otherFishPos;
                groupSize++;

                if (dist <= GlobalFlock.FishAvoidDistance)
                {
                    vAvoid += myPosition - otherFishPos;
                }

                Fish anotherFish = fish.GetComponent<Fish>();
                gSpeed += anotherFish.speed;
            }
        }

        Vector3 diff;
        if (groupSize > 0)
        {
            diff = vCenter / groupSize + (goalPos - myPosition);
            speed = gSpeed / groupSize;
        }
        else
        {
            diff = goalPos - myPosition;
            speed = GetRandomSpeed();
        }

        Vector3 direction = (diff + vAvoid) - myPosition;

        if (direction == Vector3.zero)
            return;

        var a = transform.rotation;
        var b = Quaternion.LookRotation(direction);
        var t = TurnSpeed() * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(a, b, t);

    }

    private Vector3 GetGoalPos()
    {
        return GlobalFlock.goalPositon;
    }

    private float TurnSpeed()
    {
        return Random.Range(0.2f, 0.6f) * GlobalFlock.FishTurnSpeed;
    }

}
