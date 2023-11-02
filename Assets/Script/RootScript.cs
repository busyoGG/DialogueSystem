using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RootScript : MonoBehaviour
{   
    //UI控件 -----start
    public TextMeshProUGUI _content;

    public List<Button> _buttons;

    public Button _auto;

    public Button _start;

    public Button _panel;

    public Button _record;

    public TextMeshProUGUI _target;

    public GameObject _dialoguePanel;
    //UI控件 -----end

    /// <summary>
    /// 是否自动对话
    /// </summary>
    private bool _isAuto = false;
    /// <summary>
    /// 自动对话协程
    /// </summary>
    private Coroutine _dialogCo;

    void Start()
    {
        DialogueManager.Instance().Init();

        _auto.onClick.AddListener(OnAutoClick);
        _start.onClick.AddListener(OnStartClick);
        _panel.onClick.AddListener(OnNextClick);
        _record.onClick.AddListener(OnRecordClick);

        for (int i = 0, len = _buttons.Count; i < len; i++)
        {
            Button button = _buttons[i];
            int index = i;
            button.onClick.AddListener(() =>
            {
                OnSelectClick(index);
            });
        }
    }
    /// <summary>
    /// UI展示
    /// </summary>
    private void ShowDialogue()
    {
        _content.text = DialogueManager.Instance().GetContent();
        DialogueManager.Instance().RecordDialogue(_content.text);
        _target.text = DialogueManager.Instance().GetTarget();

        List<string> selections = DialogueManager.Instance().GetSelection();
        if (selections.Count > 0)
        {
            for (int i = 0, len = selections.Count; i < len; i++)
            {
                string selection = selections[i];
                Button button = _buttons[i];
                button.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selection;
                button.gameObject.SetActive(true);
            }
        }
    }
    /// <summary>
    /// 开始对话点击事件
    /// </summary>
    private void OnStartClick()
    {
        _start.gameObject.SetActive(false);
        _dialoguePanel.SetActive(true);

        DialogueManager.Instance().StartDialogue(0);
        ShowDialogue();

        if (_isAuto)
        {
            _dialogCo = StartCoroutine(AutoDialogue());
        }
    }
    /// <summary>
    /// 自动对话点击事件
    /// </summary>
    private void OnAutoClick()
    {
        _isAuto = !_isAuto;
        if (_isAuto)
        {
            _dialogCo = StartCoroutine(AutoDialogue());
            _auto.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Auto On";
        }
        else
        {
            StopCoroutine(_dialogCo);
            _auto.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Auto";
        }
    }
    /// <summary>
    /// 下一对话点击事件
    /// </summary>
    private void OnNextClick()
    {
        if (!_buttons[0].gameObject.activeInHierarchy)
        {
            bool next = DialogueManager.Instance().Next();
            if (next)
            {
                ShowDialogue();
            }
            else
            {
                _start.gameObject.SetActive(true);
                _dialoguePanel.SetActive(false);
                DialogueManager.Instance().ClearRecordedDialogue();
            }

            if (_isAuto)
            {
                StopCoroutine(_dialogCo);
                _dialogCo = StartCoroutine(AutoDialogue());
            }
        }
    }
    /// <summary>
    /// 选项点击事件
    /// </summary>
    /// <param name="index"></param>
    private void OnSelectClick(int index)
    {
        DialogueManager.Instance().SetSelect(index);
        DialogueManager.Instance().RecordDialogue(DialogueManager.Instance().GetSelection()[index]);
        DialogueManager.Instance().Next();
        for (int i = 0; i < _buttons.Count; i++)
        {
            Button button = _buttons[i];
            button.gameObject.SetActive(false);
        }
        ShowDialogue();
        if (_isAuto)
        {
            _dialogCo = StartCoroutine(AutoDialogue());
        }
    }
    /// <summary>
    /// 对话回放点击事件
    /// </summary>
    private void OnRecordClick() {
        List<string> record = DialogueManager.Instance().GetRecordedDialogue();
        for(int i = 0,len =  record.Count; i < len; i++)
        {
            string rec = record[i];
            Debug.Log(rec);
        }
    }
    /// <summary>
    /// 自动对话协程
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoDialogue()
    {
        float speed = DialogueManager.Instance().GetAutoSpeed();
        yield return new WaitForSeconds(speed);
        bool next = DialogueManager.Instance().Next();
        if (next)
        {
            bool nextSelection = DialogueManager.Instance().GetSelection().Count == 0;
            if (nextSelection)
            {
                StartCoroutine(AutoDialogue());
            }
            ShowDialogue();
        }
        else
        {
            _start.gameObject.SetActive(true);
            _dialoguePanel.SetActive(false);
            DialogueManager.Instance().ClearRecordedDialogue();
        }
    }
}
