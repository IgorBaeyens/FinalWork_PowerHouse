using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "character")]
public class Character : ScriptableObject
{

    public new string name;
    public string health;

    public GameObject character;

    public Sprite iconCharacter;
    public Sprite iconPrimary;
    public Sprite iconSecondary;
    public Sprite iconUltimate;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
