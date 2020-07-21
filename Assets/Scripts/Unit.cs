using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    #region Fields

    [SerializeField] int healthPoints;
    [SerializeField] float speed;

    [SerializeField] float rotSpeed;

    Transform unitFinishPosition;

    UnitReachedFinish reachedFinish;
    UnitDied deathEvent;

    bool enabled = false;

    List<UnitAction> recorderActions = new List<UnitAction>();

    #endregion

    #region Methods

    /// <summary>
    /// Set the goal position and start
    /// Initialize unit
    /// </summary>
    /// <param name="unitFinishPosition">Transform</param>
    public void Initialize(Transform unitFinishPosition) {
        this.unitFinishPosition = unitFinishPosition;

        InitializeEvents();

        enabled = true;
    }

    /// <summary>
    /// Initialization for all event-related things
    /// </summary>
    void InitializeEvents() {
        reachedFinish = new UnitReachedFinish();
        deathEvent = new UnitDied();

        EventManager.AddTimeChangeListener(GoToTime);
        EventManager.AddReachedFinishInvoker(this);
        EventManager.AddUnitDeadInvoker(this);
    }

    void Update()
    {
        if (enabled)
        {
            float distance = Vector3.Distance(transform.position, unitFinishPosition.position);
            transform.position = Vector3.Lerp(transform.position, unitFinishPosition.position, (Time.deltaTime * speed) / distance);
            RotateTowardsGoal();
        }
        else {

        }
    }

    /// <summary>
    /// Set rotation Y to face the object we are going to (unitFinishPosition)
    /// </summary>
    void RotateTowardsGoal() {
        float differenceZ = unitFinishPosition.position.z - transform.position.z;
        float differenceX = unitFinishPosition.position.x - transform.position.x;

        float atan = Mathf.Atan2(differenceX, differenceZ) * Mathf.Rad2Deg;
        //Debug.Log(atan);

        transform.rotation = Quaternion.Euler(0, atan, 0);
    }

    /// <summary>
    /// Deals damage and checks if HP is less than 0
    /// </summary>
    /// <param name="damageAmount">integer</param>
    public void ReceiveDamage(int damageAmount) {
        healthPoints -= damageAmount;
        if (healthPoints <= 0)
        {
            deathEvent.Invoke(transform);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Finish")  {
            reachedFinish.Invoke(transform);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Add listener to the event of reaching the castle / finish obj
    /// </summary>
    /// <param name="listener"></param>
    public void AddReachedFinishListener(UnityAction<Transform> listener) {
        reachedFinish.AddListener(listener);
    }

    /// <summary>
    /// Add listener to the event of death
    /// so that towers will know the unit is no more
    /// </summary>
    /// <param name="listener"></param>
    public void AddUnitDeadListener(UnityAction<Transform> listener)
    {
        deathEvent.AddListener(listener);
    }

    /// <summary>
    /// Like SetActive(false)
    /// </summary>
    void DisableObject() {

    }

    /// <summary>
    /// changes object parameters to a value, recorded at a time
    /// </summary>
    /// <param name="time">playback time, float</param>
    void GoToTime(float time)
    {
        Debug.Log(time);
    }

    #endregion
}
