using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookUI : MonoBehaviour
{
    public GameObject SpellBookPanel;
    bool activeSpellBook = false;

    private void Start()
    {
        SpellBookPanel.SetActive(activeSpellBook);
    }
    
        
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            activeSpellBook = !activeSpellBook;
            SpellBookPanel.SetActive(activeSpellBook);
        }
     }


}