using UnityEngine;

public class LinkManager : MonoBehaviour
{
    // 이동하고 싶은 구글 독스나 드라이브 주소를 여기에 넣으세요.
    public string tutorialURL = "https://docs.google.com/document/d/1VM-F8vvcNXjw3lfZI5qG3M8Q6xOX3tA9jTbDeIXA9U4/edit?usp=sharing";

    public void OpenTutorialLink()
    {
        // 브라우저를 실행해서 해당 URL을 엽니다.
        Application.OpenURL(tutorialURL);
    }
}