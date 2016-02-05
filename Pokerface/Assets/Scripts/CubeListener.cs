using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeListener : MonoBehaviour
{
    public Text textSelected;
    public void MyPointerEnter () 
	{
		GetComponent<Renderer>().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
	}

	public void MyPointerLeave ()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
 

    public void MyClick(GameObject obj)
    {
        Text text = obj.GetComponentInChildren<Text>();
        //textSelected.text = "You selected " + (text != null ? text.text : obj.name);
        //Debug.Log(textSelected.text);


        // Find input field
        InputField[] ins = GameObject.FindObjectsOfType<InputField>();
        foreach (InputField i in ins)
        {
            Debug.Log("in: " + i.name);
            if (i.isFocused)
            {//i.Select();   // I also tried to use this EventSystem.current.SetSelectedGameObject(go);
                i.ActivateInputField();
                i.Select();
                i.MoveTextEnd(false);
                i.ProcessEvent(Event.KeyboardEvent("b"));
                //				inputField.ProcessEvent(Event.KeyboardEvent("a"));
                //				i.text += "a";
                //				i.textComponent.text += "a";
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                InputField i = go.GetComponent<InputField>();
                if (i != null)
                {
                    i.ProcessEvent(Event.KeyboardEvent("l"));
                }
            }
        }
    }
}
