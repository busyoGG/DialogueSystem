using System.Collections.Generic;

public class DialogueTree
{
    /// <summary>
    /// id�������ж϶Ի�˳��
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// �Ի�id�������ж������Ի���
    /// </summary>
    public int d_id { get; set; }
    /// <summary>
    /// �Ի���
    /// </summary>
    public string target { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    public string content { get; set; }
    /// <summary>
    /// ѡ��
    /// </summary>
    public List<string> selection { get; set; }
    /// <summary>
    /// ��һ�Ի�
    /// </summary>
    public List<DialogueTree> next {  get; set; }
    /// <summary>
    /// ��Ч �����ô�
    /// </summary>
    public string effect { get; set; }
    /// <summary>
    /// �Զ������ٶ�
    /// </summary>
    public float autoSpeed { get; set; }

    public DialogueTree()
    {
        next = new List<DialogueTree>();
        selection = new List<string>();
    }
}
