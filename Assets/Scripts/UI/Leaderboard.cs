using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names; 
    [SerializeField] List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "bec031db7dd3fb4084f67d3e4e16c87a3bbc97bc224f93822feb5e72ed02e906";
    private void Start()
    {
        GetLeaderboard();
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
        int looplength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for( int i = 0; i< looplength; i++ )
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((_) =>
        {
            GetLeaderboard();

        }));
    }



}


