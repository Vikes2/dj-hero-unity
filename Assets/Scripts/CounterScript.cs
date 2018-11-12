using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour {

    public TMP_Text counter;
    private int counterInt;

    public int Counter
    {
        get
        {
            return counterInt;
        }
        set
        {
            counterInt = value;
            counter.text = counterInt.ToString();
        }
    }

}
