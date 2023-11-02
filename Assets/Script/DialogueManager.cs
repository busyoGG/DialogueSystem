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
        d1.content = "��һ���Ի�";
        d1.autoSpeed = 2;

        DialogueTree d2 = new DialogueTree();
        d2.id = 1;
        d2.d_id = 0;
        d2.content = "�ڶ����Ի�";
        d2.autoSpeed = 2;
        d2.selection.Add("ѡ��1");
        d2.selection.Add("ѡ��2");

        DialogueTree d3 = new DialogueTree();
        d3.id = 2;
        d3.d_id = 0;
        d3.content = "�������Ի�";
        d3.autoSpeed = 2;

        DialogueTree d4 = new DialogueTree();
        d4.id = 3;
        d4.d_id = 0;
        d4.content = "�������Ի�";
        d4.autoSpeed = 2;

        d1.next.Add(d2);
        d2.next.Add(d3);
        d2.next.Add(d4);

        _dialogues.Add(d1);
    }

    /// <summary>
    /// ��ʼ�Ի�
    /// </summary>
    /// <param name="id"></param>
    public void StartDialogue(int id)
    {
        _curDialogue = _dialogues[id];
    }

    /// <summary>
    /// ��ȡ�Ի�����
    /// </summary>
    /// <returns></returns>
    public string GetContent()
    {
        return _curDialogue?.content;
    }

    /// <summary>
    /// ���ѡ��
    /// </summary>
    /// <returns></returns>
    public List<string> GetSelection()
    {
        return _curDialogue?.selection;
    }

    /// <summary>
    /// ����Զ��Ի��ٶ�
    /// </summary>
    /// <returns></returns>
    public float GetAutoSpeed()
    {
        return _curDialogue.autoSpeed;
    }

    /// <summary>
    /// ����ѡ��
    /// </summary>
    /// <param name="select"></param>
    public void SetSelect(int select)
    {
        _select = select;
    }

    /// <summary>
    /// ��һ�Ի�
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
    /// ��¼�Ի�
    /// </summary>
    /// <param name="content"></param>
    public void RecordDialogue(string content)
    {
        _record.Add(content);
    }

    /// <summary>
    /// ��öԻ���¼
    /// </summary>
    /// <returns></returns>
    public List<string> GetRecordedDialogue()
    {
        return _record;
    }
}
