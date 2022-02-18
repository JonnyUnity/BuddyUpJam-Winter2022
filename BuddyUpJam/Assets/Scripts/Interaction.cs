using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Interaction", menuName = "Esther/New Interaction")]
public class Interaction : ScriptableObject
{
    public string InteractionText;
    public AudioClip AudioClip;
}
