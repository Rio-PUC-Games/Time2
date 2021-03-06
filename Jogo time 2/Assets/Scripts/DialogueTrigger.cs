using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
    [HideInInspector]
    public bool canActivate;
    [Tooltip("Isso ativa o dialogo sem que precise apertar uma tecla")]
    public bool Automatico;

    [Tooltip("Desaparece após o dialogo ter finalizado, serve para cutscenes")]
    public bool Cutscene;
    int index;
    [Tooltip("Coloque aqui o dialogo criado")]
    public DialogueBlock[] Dialogue;
    public int i;
    private ListaDialogos lista;
    private SceneControl scene;

     
    int saved;

    public bool girar;


    private void Start()
    {
        scene = FindObjectOfType<SceneControl>();
        lista = GameObject.Find("ListManager").GetComponent<ListaDialogos>();
        i = 0;
        saved = SaveSystem.GetInstance().playerinfo.saved;
        /*if (saved == SceneManager.GetActiveScene().buildIndex && (Cutscene == true))
        {
            Destroy(gameObject);
        }*/
    }
    public void Update()
    {

        index = FindObjectOfType<DialogueManager>().i;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("space"))
        {
            if (canActivate)
            {
                if (FindObjectOfType<DialogueManager>().DialogueOn == false)
                {
                     
                    TriggerDialogue();
                }
            }
        }
    }
    public void TriggerDialogue()
    {
      
        //dialogueCanvas.gameObject.SetActive(true);
        FindObjectOfType<DialogueManager>().DisplayDialogue(Dialogue[i]);

        if (!lista.Listadedialogos.Contains(Dialogue[i]))
        {
            lista.Listadedialogos.Add(Dialogue[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
             
            canActivate = true;
            if (Automatico == true)
            {
                TriggerDialogue();
                if (Cutscene == true)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (girar==true && Dialogue[i].Boss==false ){

             if (other.transform.position.x + other.transform.parent.position.x> transform.localPosition.x+ transform.parent.position.x ){
              
                
                if (transform.rotation.y==0)
                transform.rotation= new Quaternion(0, 180, 0, 0);
            } 
                else {
                if (transform.rotation.y==180)
                transform.rotation= new Quaternion(0, 0, 0, 0);
               
                
            }
            
            }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
            
        }
    }
}
