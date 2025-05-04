using UnityEngine;

public class Character_Base_Behaviour : MonoBehaviour
{
    public static Character_Base_Behaviour Instance { get; private set; }
    public bool ActiveCharacterIori;
    private void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion

        //ActiveCharacterIori = true;
    }
}
