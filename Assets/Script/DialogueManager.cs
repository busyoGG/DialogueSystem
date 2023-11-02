using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager
{
    private static DialogueManager _instance = null;

    private List<DialogueTree> _dialogues = new List<DialogueTree>();

    private DialogueTree _curDialogue = null;

    private int _select;

    private List<string> _record = new List<string>();
    public static DialogueManager Instance()
    {
        if (_instance == null)
        {
            _instance = new DialogueManager();
        }
        return _instance;
    }
    private DialogueManager() { }

    public void Init()
    {
        DialogueTree d1 = new DialogueTree();
        d1.id = 0;
        d1.d_id = 0;
        d1.content = "第一条对话";
        d1.autoSpeed = 2;

        DialogueTree d2 = new DialogueTree();
        d2.id = 1;
        d2.d_id = 0;
        d2.content = "第二条对话";
        d2.autoSpeed = 2;
        d2.selection.Add("选项1");
        d2.selection.Add("选项2");

        DialogueTree d3 = new DialogueTree();
        d3.id = 2;
        d3.d_id = 0;
        d3.content = "第三条对话";
        d3.autoSpeed = 2;

        DialogueTree d4 = new DialogueTree();
        d4.id = 3;
        d4.d_id = 0;
        d4.content = "第四条对话";
        d4.autoSpeed = 2;

        d1.next.Add(d2);
        d2.next.Add(d3);
        d2.next.Add(d4);

        _dialogues.Add(d1);
    }

    /// <summary>
    /// 开始对话
    /// </summary>
    /// <param name="id"></param>
    public void StartDialogue(int id)
    {
        _curDialogue = _dialogues[id];
    }

    /// <summary>
    /// 获取对话内容
    /// </summary>
    /// <returns></returns>
    public string GetContent()
    {
        return _curDialogue?.content;
    }

    /// <summary>
    /// 获得选项
    /// </summary>
    /// <returns></returns>
    public List<string> GetSelection()
    {
        return _curDialogue?.selection;
    }

    /// <summary>
    /// 获得自动对话速度
    /// </summary>
    /// <returns></returns>
    public float GetAutoSpeed()
    {
        return _curDialogue.autoSpeed;
    }

    /// <summary>
    /// 设置选项
    /// </summary>
    /// <param name="select"></param>
    public void SetSelect(int select)
    {
        _select = select;
    }

    /// <summary>
    /// 下一对话
    /// </summary>
    public bool Next()
    {
        if (_curDialogue.selection.Count == 0)
        {
            if (_curDialogue.next.Count > 0)
            {

                _curDialogue = _curDialogue.next[0];
                return true;
            }
            else
            {
                _curDialogue = null;
                return false;
            }
        }
        else
        {
            _curDialogue = _curDialogue.next[_select];
            return true;
        }
    }

    /// <summary>
    /// 记录对话
    /// </summary>
    /// <param name="content"></param>
    public void RecordDialogue(string content)
    {
        _record.Add(content);
    }

    /// <summary>
    /// 获得对话记录
    /// </summary>
    /// <returns></returns>
    public List<string> GetRecordedDialogue()
    {
        return _record;
    }
}
