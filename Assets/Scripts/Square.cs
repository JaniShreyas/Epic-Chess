using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EC
{
    public class Square : MonoBehaviour
    {
        public int Position { get; set; }

        private void Start()
        {
            print(Position);
        }
    }
}