//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Wall : MonoBehaviour
//{
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.tag.Equals("bullet"))
//        {
//            Bullet b = collision.gameObject.GetComponent<Bullet>();

//            Vector3 income = b.movePos;
//            Vector3 normal = collision.contacts[0].normal;
//            b.movePos = Vector3.Reflect(income, normal).normalized;
//        }
//    }
//}
