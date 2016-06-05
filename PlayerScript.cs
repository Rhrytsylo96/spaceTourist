using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerScript : MonoBehaviour {

	// Изменение скорости перемещения героя
	public float playerSpeed = 2.0f;
    public int livesPoint = 10;
    public GUIText livetxt;
	// Текущая скорость перемещения
	private float currentSpeed = 0.0f;
	
	// Создание переменных для кнопок
	public List<KeyCode> upButton;
	public List<KeyCode> downButton;
	public List<KeyCode> leftButton;
	public List<KeyCode> rightButton;
    //private Rectangle rect = Rect((Screen.width - Width) / 2, (Screen.height - Height) / 2, Width, Height);

    public Text healthtxt;
    public Text hsTxt;
    // Сохранение последнего перемещения
    private Vector3 lastMovement = new Vector3();

	// Переменная для лазера
	public Transform laser;
	
	// Как далеко от центра корабля будет появлятся лазер
	public float laserDistance = 0.2f;
	
	// Задержка между выстрелами (кулдаун)
	public float timeBetweenFires = 0.3f;
	
	// Счетчик задержки между выстрелами
	private float timeTilNextFire = 0.0f;
	
	// Кнопка, которая используется для выстрела
	public List<KeyCode> shootButton;
    private GameController gm;
	// Переменная для звука выстрела лазером
	public AudioClip shootSound;
    //public int getHScore()
    //{
    //    return gm.HighScore;
    //}

    void Start() {
        gm = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        hsTxt.GetComponent<MainManuScript>();
        healthtxt.text = "Health:" + livesPoint.ToString();
    }

	// Update is called once per frame
	void Update () {
		// Поворот героя к мышке
		Rotation();
		// Перемещение героя
		Movement();
	
		foreach (KeyCode element in shootButton)
		{
			if(Input.GetKey(element) && timeTilNextFire < 0)
			{
				timeTilNextFire = timeBetweenFires;
				ShootLaser();
				break;
			}
		}
		timeTilNextFire -= Time.deltaTime;
        //if (PlayerPrefs.HasKey("HighScore"))
        //{
        //    gm.highScore = PlayerPrefs.GetInt("HighScore");
        //}

        //{
        //    gm.HighScore = PlayerPrefs.GetInt("HighScore");
        //}

        // healthtxt.text += "Health:" + livesPoint.ToString();
    }

	// Поворот героя к мышке
	void Rotation() {
		// Показываем игроку, где мышка
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(worldPos);
		// Сохраняем координаты указателя мыши
		float dx = this.transform.position.x - worldPos.x;
		float dy = this.transform.position.y - worldPos.y;
		// Вычисляем угол между объектами «Корабль» и «Указатель»
		float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
		// Трансформируем угол в вектор
		Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
		// Изменяем поворот героя
		this.transform.rotation = rot;
	}

	// Движение героя к мышке
	void Movement() {
		// Необходимое движение
		Vector3 movement = new Vector3();
		// Проверка нажатых клавиш
		movement += MoveIfPressed(upButton, Vector3.up);
		movement += MoveIfPressed(downButton, Vector3.down);
		movement += MoveIfPressed(leftButton, Vector3.left);
		movement += MoveIfPressed(rightButton, Vector3.right);
		// Если нажато несколько кнопок, обрабатываем это
		movement.Normalize();
		// Проверка нажатия кнопки
		if(movement.magnitude > 0)
		{
			// После нажатия двигаемся в этом направлении
			currentSpeed = playerSpeed;
			this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
			lastMovement = movement;
		}
		else
		{
			// Если ничего не нажато
			this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
			// Замедление со временем
			currentSpeed *= 0.9f;
		}
	}
	
	// Возвращает движение, если нажата кнопка
	Vector3 MoveIfPressed(List<KeyCode> keyList, Vector3 Movement) {
		// Проверяем кнопки из списка
		foreach (KeyCode element in keyList)
		{
			if(Input.GetKey (element))
			{
				// Если нажато, покидаем функцию
				return Movement;
			}
		}
		// Если кнопки не нажаты, то не двигаемся
		return Vector3.zero;
	}
    public void OnCollisionEnter2D(Collision2D theCollision)

    {/*
	strata żyć przy kolizji playera z enemies
	\param theCollision obiekt dla kolizji
	*/
        Debug.Log("Oncollicionenter works"+ livesPoint);
        if (theCollision.gameObject.name.Contains("enemy"))
        {
			///Dla obserwacji prawidlowego dzialania na konsoli
            Debug.Log("detected collision");
            //livesPoint--;
           // livetxt.text=livesPoint.ToString();
            //Destroy(theCollision.gameObject);
           // healthScript hs = new healthScript();
		   //! zmniejszenie ilosci zyc przy kolizji znalezionej
            livesPoint -= 1;
            
            //youloseText.transform.position.Set(130, 130, 0);
            //youloseText.text = "You lose! ";
			///na ekran
            healthtxt.text = "Health:" + livesPoint.ToString();
            //Debug.Log(hs.health);
            if (livesPoint == 0)/*gdy nie ma życ wracamy sie do menu*/
            {
                //Debug.Log("Pora by uze vyjti!");
               // PlayerPrefs.SetInt("HighScore", gm.HighScore);
                //Debug.Log("setted highscore!=" + gm.HighScore);

                SceneManager.LoadScene("menu");
            }



        }
    }

   ///!tworzenie wylotu laseru z gracza
    void ShootLaser()
	{
		//!liczymy pozycje gracza
		float posX = this.transform.position.x +
			(Mathf.Cos ((transform.localEulerAngles.z - 90) *
			            Mathf.Deg2Rad) * -laserDistance);
		float posY = this.transform.position.y +
			(Mathf.Sin ((transform.localEulerAngles.z - 90) *
			            Mathf.Deg2Rad) * -laserDistance);
		///tworzymy w tej pozycji laser
		Instantiate(laser, new Vector3(posX, posY, 0), this.transform.rotation);
		///play sound
		GetComponent<AudioSource>().PlayOneShot(shootSound);

	}

}
