using System.Collections.Generic;

public class DialogueTree
{
    /// <summary>
    /// id，用来判断对话顺序
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 对话id，用来判断所属对话树
    /// </summary>
    public int d_id { get; set; }
    /// <summary>
    /// 对话人
    /// </summary>
    public string target { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string content { get; set; }
    /// <summary>
    /// 选项
    /// </summary>
    public List<string> selection { get; set; }
    /// <summary>
    /// 下一对话
    /// </summary>
    public List<DialogueTree> next {  get; set; }
    /// <summary>
    /// 特效 暂无用处
    /// </summary>
    public string effect { get; set; }
    /// <summary>
    /// 自动播放速度
    /// </summary>
    public float autoSpeed { get; set; }

    public DialogueTree()
    {
        next = new List<DialogueTree>();
        selection = new List<string>();
    }
}
