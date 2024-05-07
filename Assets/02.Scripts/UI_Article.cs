using UnityEngine;
using UnityEngine.UI;
// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public Image ProfileImageUI;    // 프로필 이미지 
    public Text NameTextUI;         // 글쓴이
    public Text ContentTextUI;      // 글 내용    
    public Text LikeTextUI;         // 좋아요 개수    
    public Text WriteTimeUI;        // 글 쓴 날짜/시간

    public void Init(Article article)
    {
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = $"{article.WriteTime}";

    }
}
