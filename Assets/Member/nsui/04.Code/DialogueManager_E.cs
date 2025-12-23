using System.Collections;
using UnityEngine;
using TMPro; // TMP 사용을 위해 필요
using UnityEngine.SceneManagement; // 씬 전환 시 필요

public class DialogueManager_E : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public TextMeshProUGUI dialogueText; // 텍스트 컴포넌트 연결
    public string[] sentences;           // 표시할 문장들
    public float typingSpeed = 0.05f;    // 글자가 써지는 속도

    private int index = 0;               // 현재 문장 번호
    private bool isTyping = false;       // 현재 타이핑 중인지 확인

    void Start()
    {
        // 첫 번째 문장 시작
        StartCoroutine(TypeSentence());
    }

    void Update()
    {
        // 클릭하거나 아무 키나 누를 때
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            if (isTyping)
            {
                // 1. 타이핑 중일 때 클릭하면 문장을 한 번에 완성
                StopAllCoroutines();
                dialogueText.text = sentences[index];
                isTyping = false;
            }
            else
            {
                // 2. 문장이 완성된 상태에서 클릭하면 다음 문장으로
                NextSentence();
            }
        }
    }

    IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogueText.text = "";

        // 현재 문장을 string 변수에 담음
        string currentSentence = sentences[index];
        int charIndex = 0;

        while (charIndex < currentSentence.Length)
        {
            // 만약 태그 '<'가 시작된다면 태그가 끝날 때까지 한 번에 추가
            if (currentSentence[charIndex] == '<')
            {
                int tagEnd = currentSentence.IndexOf('>', charIndex);
                dialogueText.text += currentSentence.Substring(charIndex, tagEnd - charIndex + 1);
                charIndex = tagEnd + 1;
            }
            else
            {
                // 일반 글자는 하나씩 출력
                dialogueText.text += currentSentence[charIndex];
                charIndex++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        isTyping = false;
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence());
        }
        else
        {
            // 모든 대화가 끝났을 때의 처리 (예: 다음 씬 이동)
            panel.SetActive(true);
        }
    }
}