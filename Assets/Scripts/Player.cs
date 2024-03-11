using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player objects")]
    private Rigidbody rb;
    private Animator anim;


    [Header("Gameplay values")]
    private float speed = 1;
    private float grabSpeed = 0.75f;
    private float horizontalMove, verticalMove;
    public List<GameObject> grabbedEnemies;


    [Header("UI Objects")]
    [SerializeField] Joystick joystick;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        CharacterMovement();
        PlayerAnimation();
    }


    void Update()
    {
        CharacterBalance();
    }


    void CharacterMovement()
    {
        horizontalMove = joystick.Direction.x * speed;
        verticalMove = joystick.Direction.y * speed;
        rb.velocity = new Vector3(horizontalMove, 0, verticalMove);

        Vector3 direction = new Vector3(horizontalMove, 0, verticalMove);
        transform.LookAt(transform.position + direction);

    }


    void PlayerAnimation()
    {
        if (rb.velocity.magnitude >= 0.15) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") //Beat a enemy
        {
            anim.SetTrigger("punching");
            StartCoroutine(Beat(other.gameObject));
        }

        if (other.tag == "Beated") //Check if the beated enemy is valid
        {
            if (!grabbedEnemies.Contains(other.gameObject) && grabbedEnemies.Count < GameManager.Instance.level)
            {
                GameManager.Instance.enemiesInGame -= 1;
                GameManager.Instance.beatedEnemies += 1;
                grabbedEnemies.Add(other.gameObject);
            }
        }

        if (other.tag == "Point")  //Delive a enemy
        {
            if (GameManager.Instance.beatedEnemies > 0)
            {
                GameManager.Instance.IncreaseMoney(grabbedEnemies.Count);
                DestroyEnemies();
            }

            else return;
        }
    }


    void CharacterBalance()
    {
        float i = 1;
        foreach (GameObject grabbed in grabbedEnemies)
        {
            if (grabbed != null)
            {
                grabbed.transform.rotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w * 2);

                grabbed.transform.GetComponent<Animator>().enabled = true;
                grabbed.transform.position = Vector3.Lerp(grabbed.transform.position, new Vector3(transform.position.x, transform.position.y + 0.2f + (0.15f * i), transform.position.z + 0.1f), grabSpeed / (i / 4) * Time.deltaTime);
                i++;
            }
        }
    }


    void DestroyEnemies()
    {
        foreach (GameObject grabbed in grabbedEnemies) Destroy(grabbed);
        grabbedEnemies.Clear();
    }


    IEnumerator Beat(GameObject enemy)
    {
        enemy.transform.GetChild(2).GetComponent<Rigidbody>().AddForce(Vector3.back * 0.5f, ForceMode.Impulse);
        enemy.transform.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(1);

        enemy.transform.GetChild(2).GetComponent<Rigidbody>().isKinematic = true;
        enemy.tag = "Beated";
    }
}
