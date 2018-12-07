using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability")]
public class Ability : ScriptableObject
{
    public string aName;
    public float aCooldown;
    public Sprite aIcon;
    public Effect[] effects;
    public KeyCode hotkey;

    public float speedMultiplier;
    public float healthMultiplier;

    // A cocher si l'effet de la capacité se déclenche automatique sur le joueur (Invulnérabilité et Knockback)
    public bool castOnSelf;

    // A cocher si le curseur doit être sur une unité ennemie pour que l'effet se déclenche (Normalement toujours false)
    public bool castOnEnnemy;
}
