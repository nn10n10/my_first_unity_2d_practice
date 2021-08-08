using UnityEngine;

public class enterDialog : MonoBehaviour
{
    public GameObject EnterDialog;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            EnterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            EnterDialog.SetActive(false);
        }
    }

}
