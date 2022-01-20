using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAgitController : MonoBehaviour
{
     public ConversationSystem conver;
    // Start is called before the first frame update
    void Start()
    {
            FadeController.Instance.TitleFadeOut(conver.ActiveConversation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
