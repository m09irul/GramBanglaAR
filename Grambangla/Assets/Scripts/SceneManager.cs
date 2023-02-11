using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] GameObject[] waypoints;
    public int current;
    public float speed;
    public float WPradius = 1;
    Animator animator;
    private void Start()
    {
        animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, character.transform.position) <= WPradius)
        {
            current++;
            if (current >= waypoints.Length)
                current = 0;
        }
        character.transform.position = Vector3.MoveTowards(character.transform.position, waypoints[current].transform.position, Time.deltaTime* speed);
        character.transform.LookAt(waypoints[current].transform);
    }
}
