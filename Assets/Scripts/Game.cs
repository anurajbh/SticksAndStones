using UnityEngine;
using System.Collections;
 
[System.Serializable]
public class Game { 
 
    public static Game current;
    public Character Elly;
    public Character James;
    public Character Charlotte;
 
    public Game () {
        Elly = new Character();
        James = new Character();
        Charlotte = new Character();
    }
         
}