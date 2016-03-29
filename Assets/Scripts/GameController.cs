using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MazeUtils;

public class GameController : MonoBehaviour {

	//Game components
	public Transform Player;
	public Transform Doors;
	public Camera mainCam;
	public GUIText gameOverText;

	public Object horizontalWall;
	public Object verticalWall;

	//Game controls
	public int startingTime;
	public int width, height;

	private  int level = 1;
	private int time;	
	private int counter = 0;
	private bool isGameOver = false;
	private GUIStyle guiStyle = new GUIStyle(); 
	
	// Constants
	private const int TIME_TICK = 60;
	private const int FONT_SIZE = 22;
	
	//Maze
	private Maze maze = new Maze();
 	private List<Object> walls = new List<Object>();

	//############# Public methods ##########

	void Start () {
		Debug.Log ("Game Start");
		gameOverText.enabled = false;
		maze.generateMaze (width, height);
		drawMaze ();
		setPlayerAndDoorPosition ();
		time = startingTime;
	}
	

	void Update () {
		//Camera Following
		float playerXpos = Player.position.x;
		float playerYpos = Player.position.y;
		mainCam.transform.position = new Vector3 (playerXpos, playerYpos, -10); 

		counter++;
		if (counter == TIME_TICK) 
		{
			time--;
			if (time <= 0)
				gameOver();
		
			counter = 0;
		}
	}


	void OnGUI()
	{
		if (!isGameOver) 
		{
			guiStyle.fontSize = FONT_SIZE;
			guiStyle.normal.textColor = Color.yellow;
			//GUI.color = Color.yellow;
			GUI.Label (new Rect (Screen.width / 2 - 150, 20, 1000, 100), "LEVEL: " + level, guiStyle);
			GUI.Label (new Rect (Screen.width / 2 + 150, 20, 100, 100), "TIME: " + time,  guiStyle);
		} 
		else
			GUI.Label (new Rect (Screen.width / 2 - 150, 20, 100, 100), "LAST LEVEL: " + level);

	}


	public void levelCompleted()
	{
		Debug.Log ("Level Completed");

		level++;
		if (isGameOver) 
		{
			level = 1;
			isGameOver = false;
		}

		clearMaze ();
		maze.generateMaze (width, height);
		drawMaze ();
		setPlayerAndDoorPosition ();
		gameOverText.enabled = false;
		int nextTime = startingTime - (level-1) * 10;
		if (nextTime < 20)
						time = 20;
		else
			time = nextTime;
	}


	public void gameOver()
	{
		Debug.Log ("Game Over");
		clearMaze ();
		isGameOver = true;
		gameOverText.enabled = true;
	}


	//############# Private methods ##########

	private void setPlayerAndDoorPosition()
	{
		int startingPoint = Random.Range (0, 12);
		Debug.Log ("startign point:" + startingPoint);
		Player.position = new Vector3 (28f, -64f - startingPoint*96f, 0);
		startingPoint = Random.Range (0, 12);
		Debug.Log ("startign point:" + startingPoint);
		Doors.position = new Vector3 (2054f, -33f - startingPoint*96f, 0);
	}
	

	private void drawMaze()
	{
		Debug.Log ("Draw Maze");

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
					if (maze.getCell(i,j).isTop()) {
						if (j != 0)
								walls.Add(Instantiate (horizontalWall, new Vector3 (transformX (i), -(transformY (j)), 0), new Quaternion ()));

					}
					if (maze.getCell(i,j).isLeft()) {
						if (i != 0)
								walls.Add(Instantiate (verticalWall, new Vector3 (transformX (i), -(transformY (j)), 0), new Quaternion ()));
					}
			}
		}
	}


	private void clearMaze()
	{
		
		foreach (Object o in walls)
		{
			Destroy (o, 1.0f);
		}
	}


	private float transformX (float x)
	{
		return x * 96;
	}


	private float transformY (float y)
	{
		return y * 96;
	}
}
