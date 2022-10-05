using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Runtime.InteropServices.ComTypes;

public class DialogManager : MonoBehaviour
{
    public string path;
    private int line_index;
    private Dialog dialog;
    [SerializeField] private TMP_Text textContainer;
    [SerializeField] private int nextScene;
    UnityWebRequest uwr;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WebRequest(Application.streamingAssetsPath + "/Dialogs/" + path)); 
    }

    private void ReadJSON()
    {
        var _text = uwr.downloadHandler.data;
        var jsonString = System.Text.Encoding.UTF8.GetString(_text, 3, _text.Length - 3);
        dialog = JsonUtility.FromJson<Dialog>(jsonString);
        foreach (Line line in dialog)
        {
            Debug.Log("italic: " + line.italic + ", sentence: " + line.sentence);
        }
        line_index = 0;
        textContainer.SetText(dialog.lines[line_index].sentence);
        if (dialog.lines[line_index].italic)
        {
            textContainer.fontStyle = FontStyles.Italic;
        }
    }

    private IEnumerator<UnityWebRequestAsyncOperation> WebRequest(string path)
    {
        uwr = UnityWebRequest.Get(path);
        yield return uwr.SendWebRequest();
        ReadJSON();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            line_index++;
            if(line_index >= dialog.lines.Length){
                textContainer.SetText("");
                print("yeaaah");
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                return;

            }
            textContainer.SetText(dialog.lines[line_index].sentence);
            if(dialog.lines[line_index].italic){
                textContainer.fontStyle = FontStyles.Italic;
            }
            else{
                textContainer.fontStyle = FontStyles.Normal;
            }
        }
    }

    void PrintDialog(string path)
    {
        textContainer.SetText(path);
    }
}


[System.Serializable]
public class Line{
    public bool italic;
    public string sentence;
}

[System.Serializable]

public class Dialog{
    public Line[] lines;

    public DialogEnumerator GetEnumerator(){
        return new DialogEnumerator(this);
    }

    public class DialogEnumerator{
        int index;
        Dialog lines;
        public DialogEnumerator(Dialog dLines){
            lines = dLines;
            index = -1;
        }
        public bool MoveNext(){
            index++;
            return (index<lines.lines.Length);
        }
        public Line Current => lines.lines[index];
    }
}
