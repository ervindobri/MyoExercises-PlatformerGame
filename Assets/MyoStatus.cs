using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyoStatus : MonoBehaviour
{
    // public ThalmicMyo thalmicMyo;
    private SpriteRenderer sprite;
    private SpriteRenderer childSprite;

    // Start is called before the first frame update
    private void Awake()
    {

        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        childSprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // sprite.color =  thalmicMyo.armSynced ? new Color(0,1,1,0.375f) : new Color(1,1,1,.5f);
        // childSprite.color = thalmicMyo.armSynced ? sprite.color : Color.white;
    }
}
