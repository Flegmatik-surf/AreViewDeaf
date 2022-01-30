using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public string path;
    private string JSONString;
    private int line_index;
    private Dialog dialog;
    [SerializeField] private TMP_Text textContainer;

    // Start is called before the first frame update
    void Start()
    {
        JSONString = File.ReadAllText(Application.dataPath + "\\Resources\\Dialogs\\" + path);
        print(JSONString);
        dialog = JsonUtility.FromJson<Dialog>(JSONString);
        foreach (Line line in dialog)
        {
            Debug.Log("italic: "+line.italic + ", sentence: " + line.sentence);
        }
        line_index = 0;
        textContainer.SetText(dialog.lines[line_index].sentence);
        if(dialog.lines[line_index].italic){
            textContainer.fontStyle = FontStyles.Italic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            line_index++;
            if(line_index >= dialog.lines.Length){
                textContainer.SetText("");
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
