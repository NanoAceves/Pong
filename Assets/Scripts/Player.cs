using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed=10f;
    public enum Controles{P1, P2};
    public Controles PlayerControl = new Controles();
    public Collider2D TopWall, BottomWall;
    public AudioSource BallSound;

    private float VerticalMovement;
    private float minY, maxY;
    private float restrictY;
    private Collider2D PlayerCollider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCollider = GetComponent<Collider2D>();
        maxY = TopWall.bounds.center.y - TopWall.bounds.extents.y - PlayerCollider.bounds.extents.y - 0.10f;
        minY= BottomWall.bounds.center.y + BottomWall.bounds.extents.y + PlayerCollider.bounds.extents.y + 0.10f;

    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement=Input.GetAxisRaw(PlayerControl.ToString());
        transform.position += Vector3.up * VerticalMovement * Time.deltaTime*Speed;
        //Debug.Log(Input.GetAxisRaw(PlayerControl.ToString()));

        restrictY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, restrictY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ball")
        {
            BallSound.Play();
        }
    }
}
