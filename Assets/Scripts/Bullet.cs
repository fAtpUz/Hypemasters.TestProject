using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Fields

    [SerializeField] float speed;
    int damage;
    
    Transform target;

    #endregion

    #region Methods

    /// <summary>
    /// Set target and damage
    /// </summary>
    /// <param name="target">Transform</param>
    /// <param name="damage">integer</param>
    public void SetTarget(Transform target, int damage) {
        this.target = target;
        this.damage = damage;
        EventManager.AddTimeChangeListener(GoToTime);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Unit") {
            collision.gameObject.GetComponent<Unit>().ReceiveDamage(damage);
        }

        // if we collide with anything, disable the bullet object
        gameObject.SetActive(false);
    }

    /// <summary>
    /// changes object parameters to a value, recorded at a time
    /// </summary>
    /// <param name="time">playback time, float</param>
    void GoToTime(float time) {

    }

    #endregion
}
