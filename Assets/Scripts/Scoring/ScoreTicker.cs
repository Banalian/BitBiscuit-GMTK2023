using Scoring;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTicker : MonoBehaviour
{
    [Tooltip("Amount of points gained per tick")]
    [field:SerializeField]
    [field:Range(0,50)]
    public int PointsPerTick { get; private set; } = 1;
    
    [Tooltip("Time between ticks in seconds")]
    [field:SerializeField]
    [field:Range(.5f,5)]
    public float TimeBetweenTicks { get; private set; } = 1;

    [Tooltip("The current timer value")]
    [field:SerializeField]
    private float tickTimer;
    
    [Tooltip("The current tick the ticker is on, increases everytime it ticks")]
    [field:SerializeField]
    public int CurrentTick { get; private set; }

    [Tooltip("event fired on every tick")]
    public UnityAction OnTickEvent;

    public ScoreTicker(UnityAction onTickEvent)
    {
    }

    private void Awake()
    {
        this.tickTimer = .0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTick();
    }


    /// <summary>
    /// Updates the timer value, then checks if a tick needs to happen
    /// </summary>
    private void CheckTick()
    {
        this.tickTimer += Time.deltaTime;
        if (tickTimer >= TimeBetweenTicks)
        {
            Tick();
        }
        else
        {
            
        }
    }

    /// <summary>
    /// make a tick happen
    /// </summary>
    void Tick()
    {
        OnTickEvent?.Invoke();
        this.CurrentTick++;
        this.tickTimer = .0f;
        ScoreManager.Instance.AddScore(this.PointsPerTick);
    }
}
