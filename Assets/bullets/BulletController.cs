using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public static int damage = 1;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {

        PhysicMaterial bullet = new PhysicMaterial("Bullet");

        bullet.bounciness = 1.0f;

        bullet.staticFriction = 0.2f;

        bullet.dynamicFriction = 0.3f;

        GetComponent<BoxCollider>().material = bullet;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Switch")
        {
            col.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        damage *= 2;
        if (col.gameObject.CompareTag("Wall"))
        {

            int myDirection = Random.Range(10, 20);
            Vector3 myDirectionVector = new Vector3();
            switch (myDirection)
            {
                case 0:

                    myDirectionVector = Vector3.forward;
                    break;
                case 1:

                    myDirectionVector = Vector3.back;
                    break;
                case 2:

                    myDirectionVector = Vector3.left;
                    break;
                default:

                    myDirectionVector = Vector3.right;
                    break;
            }
            GetComponent<Rigidbody>().AddForce(myDirectionVector * 10, ForceMode.VelocityChange);
        }


    }
    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 6);
    }
}
