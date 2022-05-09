using UnityEngine;

[CreateAssetMenu(fileName = "New Achivement", menuName = "Achivement")]
public class Achivement : ScriptableObject
{
    public string achivementInfo;
    public Sprite achivementSprite;


    [HideInInspector]
    public bool isActive;

}
