using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//!  Classa dla menu głownego
/*!
wyswietlenie menu głownego
*/

public class MainManuScript : MonoBehaviour {
    ///menu kontekstowe yes/no
    public Canvas quitMenu;
    ///przycisk zacznij gre
    public Button startText;
    ///przycisk zamknij gre
    public Button exitText;
    ///najlepszy wynik
    private Text txtHighScore;
    ///zmienna dla komunikacji z player script klasą
    private PlayerScript ps;
	/// Use this for initialization
	void Start () {
        txtHighScore = GameObject.FindGameObjectWithTag("highscore").GetComponent<Text>();/*.FindObjectWithTag("highscore").GetComponent<Text>();*/


        quitMenu.enabled = false;///!ukryte menu kontekstowe
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        ///!inicjalizacja przycisków
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        
        txtHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
       // txtHighScore.text = ps.getHScore().ToString();
    
    }
    ///called once per frame
    void Update()
    {
        txtHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    //public void setTextToScore(int txt)
    //{
    //   // txtHighScore.text = txt.ToString();

    //}
    ///po wcisnięciu exit
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;

    }
    ///po wcisnieciu no
    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    ///po wcisnieciu start
    public void StartLevel()
    {
        SceneManager.LoadScene("Level_1");
        
    }/// po podtwierdzeniu wyjscia
    public void ExitGame()
    {
        Application.Quit();
    }

	
	// Update is called once per frame
	
}
