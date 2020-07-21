using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject unitPrefab;
    [SerializeField] float spawnDelay;
    float curDelay;

    Transform unitStartPosition;
    Transform unitFinishPosition;

    [SerializeField] LayerMask pathwayLayer;

    #endregion

    #region Methods

    void Start() {
        InitializeFields();
        InitializeTimer();
    }

    void Update()
    {
        curDelay -= Time.deltaTime;
        if (curDelay <= 0)
        {
            // create a goblin at start position
            InstatiateUnit(unitStartPosition.position, unitStartPosition.rotation);
            InitializeTimer();
        }

        // let user spawn a unit with a click/touch
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, pathwayLayer)) {
                //Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
                InstatiateUnit(hit.point, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Fills all data required for the component to work
    /// </summary>
    void InitializeFields() {
        unitStartPosition = GameObject.FindGameObjectWithTag("Start").GetComponent<Transform>();
        unitFinishPosition = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
    }

    /// <summary>
    /// Restart the timer
    /// </summary>
    void InitializeTimer()
    {
        curDelay = spawnDelay;        
    }

    /// <summary>
    /// Spawn our unit with specified position & rotation
    /// </summary>
    void InstatiateUnit(Vector3 position, Quaternion rotation) {
        GameObject newUnit = Instantiate(unitPrefab, position, rotation);
        // initialize the unit here
        // 
        newUnit.GetComponent<Unit>().Initialize(unitFinishPosition);
    }

    #endregion
}
