using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	/// tworzenie zmiennej przeciwnik
	public Transform enemy;

	/// roznica w odcinkach czasowych
	public float timeBeforeSpawning = 1.5f;
    ///czas pomiedzy przeciwnik
	public float timeBetweenEnemies = 0.25f;
    ///czas miedzyfazowy
	public float timeBeforeWaves = 2.0f;
    ///ilosc przeciwnikow na fale
	public int enemiesPerWave = 10;
    ///ile teraz jest enemies
	private int currentNumberOfEnemies = 0;
    ///punkty zyciowe
    public int livesPoint = 10;

	/// Zmienne dla wypisywania na ekran
	private int score = 0;
	private int waveNumber = 0;
    
    public int waveMaxNumber=2;

    
	/// !references to the txt objects
	public GUIText scoreText;
	public GUIText waveText;
   // public GUIText liveText;
    public GUIText youloseText;
    public int highScore=0;
    
    ///public int HighScore
    //{
    //    get { return highScore;
    //    }
    //    set { highScore = value; }
    //}

	/// Przy inializacji
	void Start () {
        if (PlayerPrefs.HasKey("Score"))
        {
            if(SceneManager.GetSceneByName("Level_1").isLoaded)/*sprawdzenie aktualnej sceny*/
            {
                PlayerPrefs.DeleteKey("Score");
                score = 0;
            }
            else
            {
                score = PlayerPrefs.GetInt("Score");

            }
        }
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (SceneManager.GetSceneByName("Level_1").isLoaded)
            {
              
            }
            else
            {
                highScore = PlayerPrefs.GetInt("HighScore");

            }
        }

        // highScore = PlayerPrefs.GetInt("HighScore");
        //if (PlayerPrefs.HasKey("HighScore"))
        //   {
        //    highScore = PlayerPrefs.GetInt("HighScore");

        //}
        //healthtxt.
        StartCoroutine (SpawnEnemies());
	}
	
	/// Update is called once per frame
	void Update () {
        
        
        if (SceneManager.GetSceneByName("Level_1").isLoaded)
        {
           // clearScore();
            if (waveNumber > waveMaxNumber)
            {
                saveScore();
                SceneManager.LoadScene(2);
            }
        }
        else if (SceneManager.GetSceneByName("Level_2").isLoaded)
        {
            //loadScore();
            saveScore();

        }
        

    }
    //public bool WavesNum(int number)
    //{
    //    if (waveNumber > number)
    //    {
    //        return true;
    //    }
    //    else
    //        return false;

    //}

	/// Pojawienie szeregow enemies
	IEnumerator SpawnEnemies()
	{
		/// poczatkowa zatrzymka przed pojawieniem enemies
		yield return new WaitForSeconds (timeBeforeSpawning);
		/// wykonujemy:
		while(true)
		{
			// Nie tworzyc nowych dopuki nie zniszczylem starych
			if (currentNumberOfEnemies <= 0)
			{
                
				waveNumber++;
				waveText.text = "Wave: " + waveNumber;
                waveText.color = Color.green;
                livesPoint++;
                Debug.Log(livesPoint);
                
            
                float randDirection;
				float randDistance;
				/// tworzymy 10 enemies poza ekranew w dowolnych miejscach
				for (int i = 0; i < enemiesPerWave; i++)
				{
					/* random distances and positions*/
					randDistance = Random.Range (10, 25);
					randDirection = Random.Range (0, 360);
					/// Koordynaty pojawienia przeciwnikow
					float posX = this. transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this. transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					///Tworzenie wróga na wskazanych pozycjach
					Instantiate (enemy, new Vector3 (posX, posY, 0), this.transform.rotation);
					currentNumberOfEnemies++;
                    livesPoint += 2;
					yield return new WaitForSeconds (timeBetweenEnemies);
				}
			}
			///Czekamy do kolejnego sprawdzianu
			yield return new WaitForSeconds (timeBeforeWaves);
		}
	}
   

    ///Zmniejszenie ilosci wrógów
    public void KilledEnemy()
	{
		currentNumberOfEnemies--;
        saveScore();
	}
///zwienkszenie ilosci punktow
	public void IncreaseScore(int increase)
	{
        /*!
        \param increase pierwszy argument, poczatkowe punkty
        */
		score += increase;
		scoreText.text = "Points: " + score;
	}
    ///zapamietywanie ilosci punktów
    public void saveScore()
    {
        /*!
        \zapamiętaj punkty przez PlayerPrefs z kluczem score i highscore
        */
        PlayerPrefs.SetInt("Score", score);
        if (score>PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
      
    }
    public void loadScore()
    {
       if( PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            PlayerPrefs.DeleteKey("Score");
        }
    }
    public void clearScore()
    {
        PlayerPrefs.DeleteKey("Score");
        score = 0;
    }
}
